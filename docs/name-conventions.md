# Naming Conventions

This doc is the single source of truth for how things are named across the backend: folders, classes, methods, endpoints, and entities. It exists because the codebase accumulated inconsistencies (a dependent entity named without its parent prefix, a `Commands` folder that stayed singular, a query named for the wrong cardinality, a controller pluralized despite exposing no collection) that had to be found and fixed one at a time. The rules below are what those fixes converged on — follow them so the same drift doesn't happen again.

All examples use placeholder names:

- **`Entity`** — an independent, top-level domain aggregate.
- **`Item`** — a resource that is meaningless without a parent `Entity` (it can't be created, queried, or exist independently of one). Because of this, an item is never referred to by its bare name anywhere in the backend — see [Entities](#entities) below. Its full, correct name is **`EntityItem`** (the parent's name concatenated with its own), and that is the name used everywhere from this point on.

## Core principle

Two rules drive almost every naming decision in this codebase:

1. **Plural names group many things of a kind; singular names describe one thing, or a concept that isn't a collection at all.** A folder full of per-entity feature files is plural (`Entities/`). A folder representing a mass noun like "everything related to persistence" is singular (`Persistence/`) — "Persistences" isn't a word, and pluralizing it wouldn't describe more than one of anything.
2. **A dependent object always carries its parent's name.** If a class, table, folder, or route can't be understood without knowing which parent entity it belongs to, that parent name is part of its own name — not just implied by the folder it happens to sit in.

Everything else in this doc is these two rules applied to a specific layer.

## Entities

- **`Entity`** — an independent aggregate root. PascalCase class name, `tb_entity` snake_case table name (singular, `tb_` prefix), `Id` primary key column.
- **`EntityItem`** — a dependent aggregate: it always belongs to exactly one `Entity` and cannot exist without it (enforced by a required foreign key and cascade delete). Its name is the compound `<Parent><Item>`, applied consistently in **every** layer:

  | Layer | Bare name (❌ wrong) | Parent-prefixed (✅ correct) |
  | --- | --- | --- |
  | Domain class | `Item` | `EntityItem` |
  | Table | `tb_item` | `tb_entity_item` |
  | FK column | `owner_id` | `entity_id` |
  | Response DTO | `ItemResponse` | `EntityItemResponse` |
  | Request DTO | `AddItemRequest` | `AddEntityItemRequest` |
  | Feature folder | `Features/Items/` | `Features/EntityItems/` |
  | Mapping profile entry | `CreateMap<Item, ItemResponse>()` | `CreateMap<EntityItem, EntityItemResponse>()` |

  **Why the prefix matters even though it feels redundant inside `Features/EntityItems/`:** an item's identity *is* its relationship to its parent — dropping the prefix anywhere makes that relationship invisible at the point of use (a bare `AddItemRequest` reads as if items exist on their own), and it collides the moment a second, unrelated top-level `Item` entity is introduced. The folder already being named `EntityItems` is not a license to shorten the class inside it.

- Collection navigation properties on the parent (`Entity.Items`) may drop the prefix — the receiver (`Entity`) already supplies the missing context, so `Entity.Items` is exactly as unambiguous as `Entity.EntityItems` would be, just shorter. This is the one place the parent-prefix rule relaxes; see [Methods](#methods) for the same relaxation applied to method names.

## Folders

Every folder falls into one of three categories. Identify which one before naming it.

| Category | Rule | Examples |
| --- | --- | --- |
| **Feature / model folders** — grouped by entity, hold many files about that entity | Plural, matching the entity's own plural form | `Features/Entities/`, `Features/EntityItems/`, `Models/Request/Entities/`, `Models/Response/EntityItems/` |
| **Architectural / category folders** — grouped by *kind of file*, not by entity | Plural (there are many repositories, many services, many contracts...) | `Repositories/`, `Services/`, `Contracts/`, `Mappings/`, `Behaviors/`, `Specifications/`, `Enums/`, `Events/` |
| **Mass-noun concept folders** — name a single non-countable domain concept, not a collection of items | Singular | `Identity/`, `Persistence/`, `Scheduling/`, `Security/`, `Middleware/`, `Common/` |

Within a feature folder, every subfolder that groups multiple files of the same kind is *also* plural — this applies recursively, no matter how deep:

```text
Features/EntityItems/
├── Commands/              ✅ plural — holds multiple commands
│   ├── Create/
│   └── Delete/
Features/EntityItems/
├── Command/                ❌ wrong — singular, but holds multiple command files
│   ├── Create/
│   └── Delete/
```

```text
Models/Response/
├── EntityItems/            ✅ plural — matches every sibling under Response/
│   └── EntityItemResponse.cs
Models/Response/
├── EntityItem/              ❌ wrong — the only singular folder among plural siblings
│   └── EntityItemResponse.cs
```

A category folder is not exempt just because its *contents* happen to be pattern types rather than domain entities — `Specifications/` still holds many specification files, so it's plural like its sibling category folders (`Repositories/`, `Services/`), not singular:

```text
Specification/               ❌ wrong — singular top-level folder, but holds
├── Entities/                   multiple plural subfolders underneath it
└── Photos/

Specifications/               ✅ correct
├── Entities/
└── Photos/
```

**Litmus test:** if you can put a real number in front of the folder's name and it reads naturally ("three Entities", "two Commands", "several Specifications"), it's plural. If the phrase is nonsensical ("three Identities" doesn't mean multiple auth systems; "two Persistences" isn't English), it's a mass-noun concept and stays singular.

## Classes

### DTOs (Request / Response)

DTO class names are always **singular**, representing one instance of the entity — even when the DTO is returned inside a `List<T>` or `PagedResponse<T>`. Plurality belongs to the wrapper, never the item type:

```csharp
List<EntityItemResponse> Items { get; set; }        // ✅ singular item type, plural collection
PagedResponse<EntityResponse> Results { get; set; }   // ✅
List<EntityItemsResponse> Items { get; set; }        // ❌ never pluralize the DTO itself
```

Naming pattern: `<Verb><Entity|EntityItem>Request` for requests (`CreateEntityRequest`, `UpdateEntityRequest`, `AddEntityItemRequest`), `<Entity|EntityItem>Response` for responses (`EntityResponse`, `EntityItemResponse`). A verb-less request (just `EntityRequest`) is acceptable only when the DTO is a pure field bag with no dedicated action verb of its own.

### CQRS triplets

Every command or query ships as a triplet in the same feature folder, sharing one base name:

```text
<Verb><Entity>Command
<Verb><Entity>CommandHandler
<Verb><Entity>CommandValidator
```

(same shape for `Query` / `QueryHandler` / `QueryValidator`.) The `Handler` and `Validator` suffixes are appended, never inserted or reordered.

**Cardinality rule:** the entity name inside a command/query is singular or plural depending on *what the operation returns or acts on* — not on which entity it happens to touch. A query that fetches one row by id returns a single DTO, so it takes the singular form even though its entity has a plural feature folder:

```csharp
// Returns ApiResponse<EntityResponse> — ONE entity
GetEntityByIdQuery              ✅ singular — matches the single-item return type
GetEntitiesByIdQuery            ❌ plural name, singular return — mismatched

// Returns ApiResponse<PagedResponse<EntityResponse>> — MANY entities
GetPagedEntitiesQuery           ✅ plural — matches the collection return type
```

Read the return type before naming the query — don't default to the entity's own plural form out of habit.

### Domain entity classes

PascalCase, singular, matching the [Entities](#entities) rules above (`Entity`, `EntityItem` — never a bare dependent name).

### Controllers

`<Entity>Controller` is plural **only when the controller exposes at least one true collection endpoint** — a route that lists many resources (`GET /entities`) or creates into a collection you can enumerate. If every action on the controller is scoped to exactly one resource (keyed by an id or by "the current caller's own resource," with no route that returns or lists many), the controller is a singleton-resource controller and stays **singular**:

```csharp
[Route("api/[controller]")]
public class EntitiesController          // ✅ plural — GET /entities lists many
{
    [HttpGet] public Task<...> GetPaged() // lists many entities
    [HttpGet("{id}")] public Task<...> GetById(string id)
    [HttpPost] public Task<...> Create(...)
}

[Route("api/[controller]")]
public class AccountController           // ✅ singular — every action is scoped
{                                          //    to "the current caller", never a list
    [HttpGet("user-info")] public Task<...> GetUser()
    [HttpPost("register")] public Task<...> RegisterUser(...)
}

[Route("api/[controller]")]
public class EntityOwnedResourcesController  // ❌ plural name, but every action takes
{                                              //    an id and returns/affects one item —
    [HttpGet("{ownerId}")] public Task<...> Get(string ownerId)   // no list-many route exists
    [HttpPut("edit")] public Task<...> Edit(...)
}
```

Don't pluralize a controller just because the entity it wraps has a plural feature folder — check the actual routes.

## Methods

- **Domain aggregate methods** (instance methods on `Entity` that mutate its own state or child collections) use a verb + singular noun, with **no entity prefix** — the receiver already supplies that context, so `entity.AddItem(...)` is unambiguous without becoming `entity.AddEntityItem(...)`. This is the method-level counterpart to the `Entity.Items` property relaxation noted in [Entities](#entities): once you're already inside a call on `Entity`, restating its name is noise, not clarity.
- **Repository / data-access methods** encode their own return cardinality in the verb, independent of the entity's plurality:
  - `GetAllAsync()`, `GetAsync(predicate)` → return a collection (`IReadOnlyList<T>`).
  - `GetFirstAsync(predicate)` → returns a single nullable item (`T?`).
  - `AddEntity(entity)`, `UpdateEntity(entity)`, `DeleteEntity(entity)` → operate on exactly one item at a time; the `Entity` in the name here is the generic type parameter placeholder, not a literal domain name.
  - Every async method carries the `Async` suffix; synchronous mutation methods (`AddEntity`/`UpdateEntity`/`DeleteEntity`, which only stage a change without hitting the database) do not.
- **Handler methods** are always named `Handle(...)` — this is fixed by the mediator pattern, not a per-feature choice.
- **Collection properties** (navigation properties, in-memory lists) are always plural nouns: `Entity.Items`, never `Entity.ItemList` or `Entity.ItemCollection`.

## Endpoints

- **Route base path is plural** when the controller is plural (see [Controllers](#controllers)): `GET /api/entities`, `GET /api/entities/{id}`, `POST /api/entities`. A singleton-resource controller keeps whatever singular/verb-based paths make sense for its scoped actions (`GET /api/account/user-info`), since there's no collection to name in the plural.
- **Nested dependent-resource routes** are prefixed by the owning controller, matching the `EntityItem` naming from [Entities](#entities) — e.g. a dedicated `EntityItemsController` for items that have enough of their own behavior to warrant it, or an action nested under the parent's controller (`POST /api/entities/{entityId}/items`) when the item's lifecycle is simple. Pick one shape per relationship and stay consistent within it.
- **Route parameter names** disambiguate whose id is whose: `{id}` refers to the resource the current controller/action is primarily about; `{entityId}` (or similarly qualified) is used the moment an action needs a *different* resource's id, typically the parent in a nested route.
- **HTTP verb mapping**: `GET` reads (one or many, per the cardinality of the route), `POST` creates, `PUT` performs a full/idempotent update, `PATCH` performs a partial update or a named state transition (`PATCH /entities/{id}/cancel`), `DELETE` removes.

## Cheat sheet

| Question | Answer |
| --- | --- |
| Does this folder hold many files of one kind? | Plural |
| Does this folder name a single non-countable concept? | Singular |
| Does this class/table/DTO/route represent a resource that can't exist without a parent? | Prefix it with the parent's name — always, everywhere except the parent's own collection property/method |
| Does this DTO represent one item, even inside a `List<T>`? | Singular class name |
| Does this command/query act on or return one item, or many? | Name matches that cardinality, not the entity's default plurality |
| Does this controller have a route that lists/creates into a collection? | Plural controller name and plural base route. If not, singular. |
| Is this a domain method called directly on the entity instance? | Drop the entity prefix — the receiver already provides it |

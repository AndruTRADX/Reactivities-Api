# Refactor Task List: Rename "Command" Convention to "Action"

## 1. Rename `Commands` folder to `Actions` (Profiles feature)

For each file below: rename the file, rename the type inside it, and update the namespace.

### AddPhoto

- [ ] `AddPhotoProfileCommand.cs` → `AddPhotoProfileAction.cs`
- [ ] `AddPhotoProfileCommandHandler.cs` → `AddPhotoProfileActionHandler.cs`

### RemovePhoto

- [ ] `RemovePhotoProfileCommand.cs` → `RemovePhotoProfileAction.cs`
- [ ] `RemovePhotoProfileCommandHandler.cs` → `RemovePhotoProfileActionHandler.cs`
- [ ] `RemovePhotoProfileCommandValidator.cs` → `RemovePhotoProfileActionValidator.cs`

### SetMainPhoto

- [ ] `SetMainPhotoProfileCommand.cs` → `SetMainPhotoProfileAction.cs`
- [ ] `SetMainPhotoProfileCommandHandler.cs` → `SetMainPhotoProfileActionHandler.cs`
- [ ] `SetMainPhotoProfileCommandValidator.cs` → `SetMainPhotoProfileActionValidator.cs`

### Folder & namespace

- [ ] Rename folder `Features/Profiles/Commands` → `Features/Profiles/Actions`
- [ ] Update the namespace in every file above:
  `Reactivities.Application.Features.Profiles.Commands.*` → `Reactivities.Application.Features.Profiles.Actions.*`

## 2. Fix folder name in Activities feature

- [ ] Rename folder `Features/Activities/Action` → `Features/Activities/Actions`
  *(singular → plural, for consistency with the new naming convention)*
- [ ] Update the namespace in every file inside this folder:
  `Reactivities.Application.Features.Activities.Action` → `Reactivities.Application.Features.Activities.Actions`

## 3. Follow-up items (implied by the rename, easy to miss)

- [ ] Rename the **type** inside each file to match (e.g. `class AddPhotoProfileCommand` → `class AddPhotoProfileAction`) — in C# the convention is one type per file matching the file name
- [ ] Update every place that references the old type names: controllers/endpoints (`mediator.Send(new AddPhotoProfileCommand(...))` → `...Action(...)`), any explicit DI registrations, and existing tests
- [ ] Grep the touched folders for the literal word `Command` after the rename, to catch anything missed (XML doc comments, log messages, exception strings, etc.)

## 4. Follow-up items (implied by the renames, easy to miss)

- [ ] Rename the **type** inside each `Command` file to match its new file name (e.g. `class AddPhotoProfileCommand` → `class AddPhotoProfileAction`) — in C# the convention is one type per file matching the file name
- [ ] Update every place that references the old type names: controllers/endpoints (`mediator.Send(new AddPhotoProfileCommand(...))` → `...Action(...)`), any explicit DI registrations, and existing tests
- [ ] Grep the touched folders for the literal word `Command` and `queries` (lowercase) after the renames, to catch anything missed (XML doc comments, log messages, exception strings, etc.)

## 5. Rename `UserResponse` to `UserProfile` in non-self-query endpoints

- [ ] Identify all endpoints that return data about **other** users' accounts (viewing someone else's profile, listing attendees, viewing a photo owner, etc.) — as opposed to endpoints that return the **caller's own** account data (e.g. `GetCurrentUser` / `/me`-style endpoints)
- [ ] In those non-self endpoints, rename the response type `UserResponse` → `UserProfile`
- [ ] Update the corresponding file name(s) if they follow the type name (e.g. `UserResponse.cs` → `UserProfile.cs`, if a dedicated copy is needed instead of reusing the same class both endpoint types return)
- [ ] Update AutoMapper profiles/mapping configs referencing `UserResponse` for these cases
- [ ] Update namespaces/usings and any DI or serialization config that references the old type name
- [ ] Leave self-query endpoints (returning the caller's own data) using `UserResponse` unchanged

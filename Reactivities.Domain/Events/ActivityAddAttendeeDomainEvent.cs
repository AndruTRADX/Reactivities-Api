using Reactivities.Domain.Common;

namespace Reactivities.Domain.Events;

public record ActivityAddAttendeeDomainEvent(string ActivityId, DateTime Date) : IDomainEvent;

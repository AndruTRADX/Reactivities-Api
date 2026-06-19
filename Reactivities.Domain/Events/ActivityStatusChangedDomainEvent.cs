using Reactivities.Domain.Common;
using Reactivities.Domain.Enums;

namespace Reactivities.Domain.Events;

public record ActivityStatusChangedDomainEvent(string ActivityId, ActivityEventType EventType, DateTime Date) : IDomainEvent;
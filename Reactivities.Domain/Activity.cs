using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;
using Reactivities.Domain.Enums;
using Reactivities.Domain.Events;

namespace Reactivities.Domain;

[Table("tb_activity")]
public class Activity : BaseDomainModel
{
    [Column("id")]
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("category")]
    public string Category { get; set; } = string.Empty;

    [Column("city")]
    public string City { get; set; } = string.Empty;

    [Column("venue")]
    public string Venue { get; set; } = string.Empty;

    [Column("latitude")]
    public double Latitude { get; set; }

    [Column("longitude")]
    public double Longitude { get; set; }

    [Column("current_status", TypeName = "varchar(20)")]
    public ActivityEventType CurrentStatus { get; set; } = ActivityEventType.Created;

    public List<ActivityAttendee> Attendees { get; set; } = [];
    public List<ActivityEvent> Events { get; set; } = [];

    public void Cancel(string userId, string reason)
    {
        EnsureUserIsHost(userId);

        if (CurrentStatus == ActivityEventType.Completed)
            throw new DomainException("Cannot cancel an activity that has been completed");

        if (CurrentStatus == ActivityEventType.Cancelled)
            throw new DomainException("Cannot cancel an activity that has been cancelled already");

        CurrentStatus = ActivityEventType.Cancelled;
        RaiseEvent(ActivityEventType.Cancelled, userId, reason);
    }

    public void Complete()
    {
        var canBeCompleted = CurrentStatus is ActivityEventType.Created or ActivityEventType.Reactivated;
        if (!canBeCompleted) return;

        CurrentStatus = ActivityEventType.Completed;
        RaiseEvent(ActivityEventType.Completed, null, "The activity was successfully carried out.");
    }

    public void Initialize(string hostUserId)
    {
        Attendees.Add(new ActivityAttendee
        {
            ActivityId = Id,
            UserId = hostUserId,
            IsHost = true,
        });

        RaiseEvent(ActivityEventType.Created, hostUserId, string.Empty);
    }

    public void Update(string userId)
    {
        EnsureUserIsHost(userId);

        if (CurrentStatus == ActivityEventType.Completed)
            throw new DomainException("Cannot cancel an activity that has been completed");

        if (CurrentStatus == ActivityEventType.Cancelled)
            throw new DomainException("Cannot cancel an activity that has been already cancelled");
    }

    public void AddAttendee(string userId)
    {
        if (CurrentStatus == ActivityEventType.Completed)
            throw new DomainException("Cannot attend an activity that has been completed");

        if (CurrentStatus == ActivityEventType.Cancelled)
            throw new DomainException("Cannot attend an activity that has been cancelled");

        var alreadyAttending = Attendees.Any(x => x.UserId == userId);
        if (alreadyAttending)
            throw new DomainException("You are already attending the activity");

        Attendees.Add(new ActivityAttendee
        {
            ActivityId = Id,
            UserId = userId,
        });

        AddDomainEvent(new ActivityAddAttendeeDomainEvent(Id, Date));
    }

    public void RemoveAttendee(string userId)
    {
        if (CurrentStatus == ActivityEventType.Completed)
            throw new DomainException("Cannot leave a completed activity.");

        if (CurrentStatus == ActivityEventType.Cancelled)
            throw new DomainException("Cannot leave a cancelled activity.");

        var attendee = Attendees.Find(x => x.UserId == userId) 
            ?? throw new DomainException("You are not attending this activity.");
;
        if (attendee.IsHost)
            throw new DomainException("The host cannot leave the activity.");

        Attendees.Remove(attendee);
    }

    private void EnsureUserIsHost(string userId)
    {
        var host = Attendees.FirstOrDefault(a => a.IsHost);
        if (host is null || host.UserId != userId)
            throw new DomainException("Only the host can perform this action");
    }

    private void RaiseEvent(ActivityEventType type, string? triggeredByUserId, string reason)
    {
        Events.Add(new ActivityEvent
        {
            ActivityId = Id,
            EventType = type,
            TriggeredByUserId = triggeredByUserId,
            Reason = reason,
            OccurredAt = DateTime.UtcNow,
        });

        AddDomainEvent(new ActivityStatusChangedDomainEvent(Id, type, Date));
    }
}
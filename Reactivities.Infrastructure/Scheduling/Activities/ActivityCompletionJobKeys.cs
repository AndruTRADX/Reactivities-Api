using Quartz;

namespace Reactivities.Infrastructure.Scheduling.Activities;

/// <summary>
/// Centraliza la construcción de claves de Job/Trigger para evitar strings mágicos duplicados.
/// </summary>
internal static class ActivityCompletionJobKeys
{
    private const string GroupName = "activity-completion";

    public static JobKey BuildJobKey(string activityId) => new($"complete-activity-{activityId}", GroupName);

    public static TriggerKey BuildTriggerKey(string activityId) => new($"complete-activity-{activityId}-trigger", GroupName);
}

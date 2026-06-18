using Quartz;
using Reactivities.Application.Contracts.Scheduling;

namespace Reactivities.Infrastructure.Scheduling.Activities;

public class QuartzActivitySchedulerService(ISchedulerFactory schedulerFactory) : IActivitySchedulerService
{
    public async Task ScheduleActivityCompletionAsync(string activityId, DateTime activityDate, CancellationToken cancellationToken = default)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);

        var jobKey = ActivityCompletionJobKeys.BuildJobKey(activityId);
        var triggerKey = ActivityCompletionJobKeys.BuildTriggerKey(activityId);

        var fireTimeUtc = new DateTimeOffset(DateTime.SpecifyKind(activityDate, DateTimeKind.Utc));

        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerKey)
            .ForJob(jobKey)
            .StartAt(fireTimeUtc)
            .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionFireNow())
            .Build();

        if (await scheduler.CheckExists(jobKey, cancellationToken))
        {
            await scheduler.RescheduleJob(triggerKey, trigger, cancellationToken);
            return;
        }

        var job = JobBuilder.Create<CompleteActivityJob>()
            .WithIdentity(jobKey)
            .UsingJobData(CompleteActivityJob.ActivityIdDataKey, activityId)
            .Build();

        await scheduler.ScheduleJob(job, trigger, cancellationToken);
    }

    public async Task CancelScheduledCompletionAsync(string activityId, CancellationToken cancellationToken = default)
    {
        var scheduler = await schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = ActivityCompletionJobKeys.BuildJobKey(activityId);

        await scheduler.DeleteJob(jobKey, cancellationToken);
    }
}
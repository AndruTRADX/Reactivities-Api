using MediatR;
using Quartz;
using Reactivities.Application.Features.Activities.Actions.CompleteActivity;

namespace Reactivities.Infrastructure.Scheduling.Activities;

/// <summary>
/// Job técnico: solo traduce la ejecución de Quartz en un comando de MediatR.
/// Toda la regla de negocio vive en CompleteActivityActionHandler (Application).
/// </summary>
[DisallowConcurrentExecution]
public class CompleteActivityJob(ISender sender) : IJob
{
    public const string ActivityIdDataKey = "activityId";

    public async Task Execute(IJobExecutionContext context)
    {
        var activityId = context.MergedJobDataMap.GetString(ActivityIdDataKey);

        if (string.IsNullOrWhiteSpace(activityId))
        {
            throw new JobExecutionException($"El JobDataMap no contiene la clave requerida '{ActivityIdDataKey}'.");
        }

        await sender.Send(new CompleteActivityAction { ActivityId = activityId }, context.CancellationToken);
    }
}

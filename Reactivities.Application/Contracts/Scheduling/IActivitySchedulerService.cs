namespace Reactivities.Application.Contracts.Scheduling;

/// <summary>
/// Puerto hacia el mecanismo de programación de tareas (implementado con Quartz en Infrastructure).
/// </summary>
public interface IActivitySchedulerService
{
    /// <summary>
    /// Programa (o re programa, si ya existía) el job que marcará la actividad como completada
    /// en la fecha indicada.
    /// </summary>
    Task ScheduleActivityCompletionAsync(string activityId, DateTime activityDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina el job de finalización programado para la actividad, si existe.
    /// </summary>
    Task CancelScheduledCompletionAsync(string activityId, CancellationToken cancellationToken = default);
}

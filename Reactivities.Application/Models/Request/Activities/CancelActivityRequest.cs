namespace Reactivities.Application.Models.Request.Activities;

public class CancelActivityRequest
{
    public string Id { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}

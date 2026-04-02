namespace Reactivities.Application.Models.Response;

public class ApiResponse<T>
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse()
    {

    }

    public ApiResponse(T data, string message = "") : this()
    {
        Success = true;
        Data = data;
        Message = message ?? "Operation successful";
    }

    public ApiResponse(string message, List<string> errors) : this()
    {
        Success = false;
        Message = message ?? "An error has occurred";
        Errors = errors ?? [];
    }
}
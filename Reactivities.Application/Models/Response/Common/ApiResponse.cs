namespace Reactivities.Application.Models.Response.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse()
    {
        Success = true;
        Message = "Operation successful";
    }

    public ApiResponse(T data) : this()
    {
        Success = true;
        Data = data;
        Message = "Operation successful";
    }

    public ApiResponse(T data, string message) : this()
    {
        Success = true;
        Data = data;
        Message = message;
    }

    public ApiResponse(string message, List<string> errors) : this()
    {
        Success = false;
        Message = message ?? "An error has occurred";
        Errors = errors ?? [];
    }
}
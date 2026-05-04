namespace Reactivities.Application.Models.Response.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; } = false;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }

    public ApiResponse()
    {
        Success = true;
        Title = "Success";
        Message = "Operation successful";
    }

    public ApiResponse(T data) : this()
    {
        Success = true;
        Data = data;
        Title = "Success";
        Message = "Operation successful";
    }

    public ApiResponse(T data, string message) : this()
    {
        Success = true;
        Data = data;
        Title = "Success";
        Message = message;
    }

    public ApiResponse(string title, string message, Dictionary<string, string[]> errors) : this()
    {
        Success = false;
        Title = title;
        Message = message ?? "An error has occurred";
        Errors = errors ?? [];
    }
}
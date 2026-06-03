namespace MyMauiApp.Models;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; }

    public static ServiceResponse<T> Fail(string message) => new() { Success = false, Message = message };
    public static ServiceResponse<T> Ok(T data) => new() { Data = data, Success = true };
}
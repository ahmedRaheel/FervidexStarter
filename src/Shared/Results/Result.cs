namespace StarterKit.Api.Shared.Results;
public sealed record Result<T>(bool Success, string Message, T? Data)
{
    public static Result<T> Ok(T data, string message="Success") => new(true,message,data);
    public static Result<T> Fail(string message) => new(false,message,default);
}

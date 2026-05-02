namespace FactoryManagment.Api.Common;

public class ApiResponse<T>
{
    public bool      Success { get; init; }
    public T?        Data    { get; init; }
    public string[]? Error   { get; init; }

    private ApiResponse() { }

    /// <summary>
    /// Wraping a successful response with the given data payload.
    /// </summary>
    public static ApiResponse<T> Ok(T data) => new()
    {
        Success = true,
        Data = data,
        Error = null
    };

    /// <summary>
    /// Wraping a failed response with one or more error messages.
    /// </summary>
    public static ApiResponse<T> Fail(IEnumerable<string> error) => new()
    {
        Success = false,
        Data = default,
        Error = error.ToArray()
    };

}

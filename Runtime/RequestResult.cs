public readonly struct RequestResult<T>
{
    public readonly T Data;
    public readonly bool IsSuccessful;

    private RequestResult(T data, bool isSuccessful)
    {
        Data = data;
        IsSuccessful = isSuccessful;
    }

    public static RequestResult<T> Success(T data) => new RequestResult<T>(data, true);
    public static RequestResult<T> Failure(T data) => new RequestResult<T>(data, false);
}

public readonly struct RequestResult
{
    public readonly bool IsSuccessful;

    private RequestResult(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public static RequestResult Success { get; } = new RequestResult(true);
    public static RequestResult Failure { get; } = new RequestResult(false);
}

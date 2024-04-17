

public class Result
{
    private Result(bool isSuccess, Error error, object? data = null, object? trace = null)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Data = data;
        Trace = trace;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public object? Data {get;set;}
    public object? Trace { get; set; }

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Success(object data,object trace) => new(true, Error.None, data,trace);
    public static Result Failure(Error error) => new(false, error);
}
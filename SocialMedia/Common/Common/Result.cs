namespace SocialMedia.Common.Common;


public enum ResultMode : byte
{
    Exception,
    Message,
    Success
}

public class Result
{
    public Result(ResultMode resultMode, dynamic? value, string? message)
    {
        ResultMode = resultMode;
        Value = value;
        Message = message;
    }

    public ResultMode ResultMode { get; }
    public dynamic? Value { get; }
    public string? Message { get; }

    public static Result WithSuccess() => new(ResultMode.Success, null, null);
    public static Result WithSuccess(dynamic value) => new(ResultMode.Success, value, null);
    public static Result WithSuccess(dynamic value, string message) => new(ResultMode.Success, value, message);
    public static Result WithSuccess(string message) => new(ResultMode.Success, null, message);
    public static Result WithMessage(string message) => new(ResultMode.Message, null, message);
    public static Result WithException() => new(ResultMode.Exception, null, null);
    public static Result WithException(string message) => new(ResultMode.Exception, null, message);
    public static Result WithException(Exception exception) => new(ResultMode.Exception, exception, exception.Message);
}

public class Statement
{
    public const string Success = "Mission accomplished";
    public const string UnAuthorized = "User authentication was associated with an error";
    public const string Failure = "An error occurred while executing the operation";
}
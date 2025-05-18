using Core.Domain.Response;

namespace Core.External.Models.Response;

public class ExternalResponse : BaseResponse
{
    // TRUE
    public static ExternalResponse Ok()
    {
        return new ExternalResponse { Success = true, StatusCode = 200 };
    }
    public static ExternalResponse Created()
    {
        return new ExternalResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static ExternalResponse BadRequest(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static ExternalResponse Unauthorized(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static ExternalResponse Forbidden(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static ExternalResponse NotFound(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static ExternalResponse AlreadyExists(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static ExternalResponse Error(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static ExternalResponse BadGateway(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static ExternalResponse ServiceUnavailable(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 504, Message = message };
    }

}
public class ExternalResponse<T> : BaseResponse<T> where T : class
{
    // TRUE
    public static ExternalResponse<T> Ok(T? content)
    {
        return new ExternalResponse<T> { Success = true, StatusCode = 200, Content = content };
    }

    // FALSE
    public static ExternalResponse<T> BadRequest(string? message, T? content)
    {
        return new ExternalResponse<T> { Success = false, StatusCode = 400, Message = message, Content = content };
    }
    public static ExternalResponse<T> NotFound(string? message, T? content)
    {
        return new ExternalResponse<T> { Success = false, StatusCode = 404, Message = message, Content = content };
    }
    public static ExternalResponse<T> AlreadyExists(string? message, T? content)
    {
        return new ExternalResponse<T> { Success = false, StatusCode = 409, Message = message, Content = content };
    }
    public static ExternalResponse<T> Error(string? message, T? content)
    {
        return new ExternalResponse<T> { Success = false, StatusCode = 500, Message = message, Content = content };
    }
}

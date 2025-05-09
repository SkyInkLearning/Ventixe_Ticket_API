namespace Core.Domain.Response;

public class ServiceResponse : BaseResponse
{
    // TRUE
    public static ServiceResponse Ok()
    {
        return new ServiceResponse { Success = true, StatusCode = 200 };
    }
    public static ServiceResponse Created()
    {
        return new ServiceResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static ServiceResponse BadRequest(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static ServiceResponse Unauthorized(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static ServiceResponse Forbidden(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static ServiceResponse NotFound(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static ServiceResponse AlreadyExists(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static ServiceResponse Error(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static ServiceResponse BadGateway(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static ServiceResponse ServiceUnavailable(string? message)
    {
        return new ServiceResponse { Success = false, StatusCode = 504, Message = message };
    }

}
public class ServiceResponse<T> : BaseResponse<T> where T : class
{
    // TRUE
    public static ServiceResponse<T> Ok(T? content)
    {
        return new ServiceResponse<T> { Success = true, StatusCode = 200, Content = content };
    }

    // FALSE
    public static ServiceResponse<T> BadRequest(string? message, T? content)
    {
        return new ServiceResponse<T> { Success = false, StatusCode = 400, Message = message, Content = content };
    }
    public static ServiceResponse<T> NotFound(string? message, T? content)
    {
        return new ServiceResponse<T> { Success = false, StatusCode = 404, Message = message, Content = content };
    }
    public static ServiceResponse<T> AlreadyExists(string? message, T? content)
    {
        return new ServiceResponse<T> { Success = false, StatusCode = 409, Message = message, Content = content };
    }
    public static ServiceResponse<T> Error(string? message, T? content)
    {
        return new ServiceResponse<T> { Success = false, StatusCode = 500, Message = message, Content = content };
    }
}
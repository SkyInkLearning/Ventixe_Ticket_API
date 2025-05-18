namespace Core.Domain.Response;

public class BusResponse : BaseResponse
{
    // TRUE
    public static BusResponse Ok()
    {
        return new BusResponse { Success = true, StatusCode = 200 };
    }
    public static BusResponse Created()
    {
        return new BusResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static BusResponse BadRequest(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static BusResponse Unauthorized(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static BusResponse Forbidden(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static BusResponse NotFound(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static BusResponse AlreadyExists(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static BusResponse Error(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static BusResponse BadGateway(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static BusResponse ServiceUnavailable(string? message)
    {
        return new BusResponse { Success = false, StatusCode = 504, Message = message };
    }

}
public class BusResponse<T> : BaseResponse<T> where T : class
{
    // TRUE
    public static BusResponse<T> Ok(T? content)
    {
        return new BusResponse<T> { Success = true, StatusCode = 200, Content = content };
    }

    // FALSE
    public static BusResponse<T> BadRequest(string? message, T? content)
    {
        return new BusResponse<T> { Success = false, StatusCode = 400, Message = message, Content = content };
    }
    public static BusResponse<T> NotFound(string? message, T? content)
    {
        return new BusResponse<T> { Success = false, StatusCode = 404, Message = message, Content = content };
    }
    public static BusResponse<T> AlreadyExists(string? message, T? content)
    {
        return new BusResponse<T> { Success = false, StatusCode = 409, Message = message, Content = content };
    }
    public static BusResponse<T> Error(string? message, T? content)
    {
        return new BusResponse<T> { Success = false, StatusCode = 500, Message = message, Content = content };
    }
}
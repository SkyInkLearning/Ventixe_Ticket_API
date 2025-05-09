namespace Core.Domain.Response;

public class RepositoryResponse : BaseResponse
{
    // TRUE
    public static RepositoryResponse Ok()
    {
        return new RepositoryResponse { Success = true, StatusCode = 200 };
    }
    public static RepositoryResponse Created()
    {
        return new RepositoryResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static RepositoryResponse BadRequest(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static RepositoryResponse Unauthorized(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static RepositoryResponse Forbidden(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static RepositoryResponse NotFound(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static RepositoryResponse AlreadyExists(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static RepositoryResponse Error(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static RepositoryResponse BadGateway(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static RepositoryResponse ServiceUnavailable(string? message)
    {
        return new RepositoryResponse { Success = false, StatusCode = 504, Message = message };
    }

}
public class RepositoryResponse<T> : BaseResponse<T> where T : class
{
    // TRUE
    public static RepositoryResponse<T> Ok(T? content)
    {
        return new RepositoryResponse<T> { Success = true, StatusCode = 200, Content = content };
    }

    // FALSE
    public static RepositoryResponse<T> BadRequest(string? message, T? content)
    {
        return new RepositoryResponse<T> { Success = false, StatusCode = 400, Message = message, Content = content };
    }
    public static RepositoryResponse<T> NotFound(string? message, T? content)
    {
        return new RepositoryResponse<T> { Success = false, StatusCode = 404, Message = message, Content = content };
    }
    public static RepositoryResponse<T> AlreadyExists(string? message, T? content)
    {
        return new RepositoryResponse<T> { Success = false, StatusCode = 409, Message = message, Content = content };
    }
    public static RepositoryResponse<T> Error(string? message, T? content)
    {
        return new RepositoryResponse<T> { Success = false, StatusCode = 500, Message = message, Content = content };
    }
}
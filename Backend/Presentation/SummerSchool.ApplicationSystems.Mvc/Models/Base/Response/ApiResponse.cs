namespace SummerSchool.ApplicationSystems.Mvc.Models.Base.Response;

public abstract class ApiResponseBase
{
    public int StatusCode { get; set; }

    public bool IsSuccessful { get; set; }

    public string Message { get; set; }
}

[Serializable]
public class ApiResponse : ApiResponseBase
{
    public static ApiResponse SetSuccess(string message = null)
    {
        return new ApiResponse
        {
            StatusCode = StatusCodes.Status200OK,
            IsSuccessful = true,
            Message = message
        };
    }

    public static ApiResponse SetFail(int statusCode = StatusCodes.Status400BadRequest, string message = null)
    {
        return new ApiResponse
        {
            StatusCode = statusCode,
            IsSuccessful = false,
            Message = message
        };
    }
}

[Serializable]
public class ApiResponse<T> : ApiResponseBase
{
    public T Result { get; set; }

    public static ApiResponse<T> SetSuccess(T result, string message = null)
    {
        return new ApiResponse<T>
        {
            Result = result,
            StatusCode = StatusCodes.Status200OK,
            IsSuccessful = true,
            Message = message
        };
    }

    public static ApiResponse<T> SetFail(T result = default(T), int statusCode = StatusCodes.Status400BadRequest, string message = null)
    {
        return new ApiResponse<T>
        {
            Result = result,
            StatusCode = statusCode,
            IsSuccessful = false,
            Message = message
        };
    }
}

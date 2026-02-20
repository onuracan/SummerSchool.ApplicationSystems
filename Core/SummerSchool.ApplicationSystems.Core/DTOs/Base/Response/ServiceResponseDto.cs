namespace SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;

public abstract class ServiceResponseBaseDto
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}

[Serializable]
public class ServiceResponseDto : ServiceResponseBaseDto
{
    public static ServiceResponseDto SetSuccess(string message = null)
    {
        return new ServiceResponseDto
        {
            StatusCode = 200,
            IsSuccessful = true,
            Message = message
        };
    }

    public static ServiceResponseDto SetFail(int statusCode = 400, string message = null)
    {
        return new ServiceResponseDto
        {
            StatusCode = statusCode,
            IsSuccessful = false,
            Message = message
        };
    }

}

[Serializable]
public class ServiceResponseDto<T> : ServiceResponseBaseDto
{
    public T Result { get; set; }

    public static ServiceResponseDto<T> SetSuccess(T result, string message = null)
    {
        return new ServiceResponseDto<T>
        {
            Result = result,
            StatusCode = 200,
            IsSuccessful = true,
            Message = message
        };
    }

    public static ServiceResponseDto<T> SetFail(T result = default(T), int statusCode = 400, string message = null)
    {
        return new ServiceResponseDto<T>
        {
            Result = result,
            StatusCode = statusCode,
            IsSuccessful = false,
            Message = message
        };
    }
}
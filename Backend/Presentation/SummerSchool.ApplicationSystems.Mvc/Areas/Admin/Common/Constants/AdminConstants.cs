namespace SummerSchool.ApplicationSystems.Mvc.Areas.Admin.Common.Constants;

/// <summary>
/// Admin area için cookie authentication constant'ları
/// </summary>
public static class AdminCookieConstants
{
    public const string SCHEME = "AdminScheme";
    public const string COOKIE_NAME = "AdminAuthCookie";
    public const string COOKIE_PATH = "/Admin";
}

/// <summary>
/// Admin area için route constant'ları
/// </summary>
public static class AdminRouteConstants
{
    public const string INDEX = "/Admin/Home/Index";
    public const string LOGIN = "/Admin/Auth/Login";
    public const string LOGOUT = "/Admin/Auth/Logout";
    public const string ACCESS_DENIED = "/Admin/AccessDenied";
    public const string ERROR = "/Admin/Error/{statusCode}";
    
    // Application Management
    public const string APP_INDEX = "/Admin/Application/Index";
    public const string UPDATE_APPLICATION_STATUS = "/Admin/Application/UpdateApplicationStatus/{id}";
    
    // Course Management
    public const string GET_COURSES = "/Admin/Course/GetCourses";
    
    // Course Application Management
    public const string GET_APPLICATIONS = "/Admin/CourseApplication/GetApplicationByCourseId";
}

/// <summary>
/// Admin area için API endpoint constant'ları
/// </summary>
public static class AdminApiEndpoints
{
    public const string AUTH_LOGIN = "api/auth/admin-login";
    
    public const string APPLICATIONS_BY_COURSE = "api/courses/{0}/applications";
    public const string UPDATE_APPLICATION_STATUS = "api/course-applications/{0}/status";
}

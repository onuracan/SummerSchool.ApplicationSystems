using SummerSchool.ApplicationSystems.Core.DTOs.Base;

namespace SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Response;

public class CourseApplicationListResponseDto : BaseDto
{
    public string StudentInfo { get; set; }
    public string CourseInfo { get; set; }
    public string ApplicationStatusInfo { get; set; }
    public string ApplicationStatusDescription { get; set; }
    public string UpdatedUser { get; set; }
    public DateTime UpdatedDate { get; set; }
}

using SummerSchool.ApplicationSystems.Core.DTOs.Base;

namespace SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;

public class UpdateCourseApplicationStatusRequestDto : BaseDto
{
    public int ApplicationStatus { get; set; }
    public string ApplicationStatusDescription { get; set; }
}

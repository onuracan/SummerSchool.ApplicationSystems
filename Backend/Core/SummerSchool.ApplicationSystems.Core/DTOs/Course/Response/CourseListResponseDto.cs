using SummerSchool.ApplicationSystems.Core.DTOs.Base;

namespace SummerSchool.ApplicationSystems.Core.DTOs.Course.Response;

public class CourseListResponseDto : BaseDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Faculty { get; set; }
    public int Quota { get; set; }
    public int ApplicationCount { get; set; }
    public bool CanBeApply { get; set; }
}

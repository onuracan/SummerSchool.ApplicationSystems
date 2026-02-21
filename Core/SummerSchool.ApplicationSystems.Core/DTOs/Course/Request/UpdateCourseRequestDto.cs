namespace SummerSchool.ApplicationSystems.Core.DTOs.Course.Request;

public class UpdateCourseRequestDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Faculty { get; set; }
    public int Quota { get; set; }
}

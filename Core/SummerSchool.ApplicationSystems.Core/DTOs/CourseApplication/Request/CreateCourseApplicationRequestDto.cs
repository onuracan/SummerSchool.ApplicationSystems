namespace SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;

public class CreateCourseApplicationRequestDto
{
    public Guid? StudentId { get; set; }
    public Guid? CourseId { get; set; }
    public int? ApplicationStatus { get; set; }
}

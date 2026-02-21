namespace SummerSchool.ApplicationSystems.Mvc.Models.Course.Response;

public class CourseListResponseViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Faculty { get; set; }
    public int Quota { get; set; }
    public int ApplicationCount { get; set; }
    public bool CanBeApply { get; set; }
}

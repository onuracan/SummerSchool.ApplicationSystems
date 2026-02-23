namespace SummerSchool.ApplicationSystems.Mvc.Models.CourseApplication.Response;

public class CourseApplicationListResponseViewModel
{
    public Guid Id { get; set; }
    public string StudentInfo { get; set; }
    public string CourseInfo { get; set; }
    public int ApplicationStatus { get; set; }
    public string ApplicationStatusInfo { get; set; }
    public string UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

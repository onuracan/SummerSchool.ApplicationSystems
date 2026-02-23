namespace SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;

public class UpdateStudentRequestDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public string SchoolNumber { get; set; }
    public string Department { get; set; }
    public string Faculty { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }
    public string CountryCode { get; set; }
}

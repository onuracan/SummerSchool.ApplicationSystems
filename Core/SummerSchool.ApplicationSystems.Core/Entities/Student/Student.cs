using SummerSchool.ApplicationSystems.Core.Entities.Base;

namespace SummerSchool.ApplicationSystems.Core.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public string SchoolNumber { get; set; }
    public string Department { get; set; }
    public string Faculty { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }
    public int CountryId { get; set; }

    public virtual ICollection<CourseApplication> CourseApplications { get; set; }
}

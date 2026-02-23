using SummerSchool.ApplicationSystems.Core.Entities.Base;

namespace SummerSchool.ApplicationSystems.Core.Entities;

public class Course : BaseEntity, IInsertedAuditing, IUpdatedAuditing
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Faculty { get; set; }
    public int Quota { get; set; }
    public string InsertedUser { get; set; }
    public DateTime InsertedDate { get; set; }
    public string UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<CourseApplication> CourseApplications { get; set; }
}

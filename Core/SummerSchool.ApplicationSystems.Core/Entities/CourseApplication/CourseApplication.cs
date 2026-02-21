using SummerSchool.ApplicationSystems.Core.Entities.Base;

namespace SummerSchool.ApplicationSystems.Core.Entities;

public class CourseApplication : BaseEntity, IUpdatedAuditing
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public int ApplicationStatus { get; set; }
    public string ApplicationStatusDescription { get; set; }
    public string UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public virtual Student Student { get; set; }
    public virtual Course Course { get; set; }
}

namespace SummerSchool.ApplicationSystems.Core.Entities.Base;

public interface IUpdatedAuditing
{
    public string UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

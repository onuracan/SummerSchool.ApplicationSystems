namespace SummerSchool.ApplicationSystems.Core.Entities.Base;

public interface IInsertedAuditing
{
    public string InsertedUser { get; set; }
    public DateTime InsertedDate { get; set; }
}

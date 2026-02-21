using SummerSchool.ApplicationSystems.Core.Entities.Base;

namespace SummerSchool.ApplicationSystems.Core.Entities;

public class OtpVerification : BaseEntity
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
    public DateTime InsertedDate { get; set; }
}

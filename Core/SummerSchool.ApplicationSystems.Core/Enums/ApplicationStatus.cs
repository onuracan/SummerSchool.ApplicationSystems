using System.ComponentModel;

namespace SummerSchool.ApplicationSystems.Core.Enums;

public enum ApplicationStatus
{
    [Description("Başvuruldu")]
    Application = 1,
    [Description("Kabul Edildi")]
    Acceptance = 2,
    [Description("Reddedildi")]
    Rejection = 3
}

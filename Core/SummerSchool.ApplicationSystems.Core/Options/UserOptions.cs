using SummerSchool.ApplicationSystems.Shared.Enums;

namespace SummerSchool.ApplicationSystems.Core.Options;

public class UserOptions
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public UserType UserType { get; set; }
}

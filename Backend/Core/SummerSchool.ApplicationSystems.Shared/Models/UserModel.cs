using SummerSchool.ApplicationSystems.Shared.Enums;

namespace SummerSchool.ApplicationSystems.Shared.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string NameAndSurname { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public UserType UserType { get; set; }
}

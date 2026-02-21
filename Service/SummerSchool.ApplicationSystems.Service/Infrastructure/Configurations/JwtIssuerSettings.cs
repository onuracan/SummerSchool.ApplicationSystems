namespace SummerSchool.ApplicationSystems.Service.Infrastructure.Configurations;

public class JwtIssuerSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpireTime { get; set; }
}

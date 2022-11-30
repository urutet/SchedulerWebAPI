namespace Scheduler.Common.Options;

public class JwtOptions
{
    public const string SectionName = "JWT";
    
    public string SecretKey { get; set; }

    public int Lifespan { get; set; }
}
namespace Messaging.Api;

public static class Env
{
    public static string DatabaseConnection => String("DB_CS");
    public static string JWTIssuer => String("JWT_ISS");
    public static string JWTAudience => String("JWT_AUD");
    public static string JWTPrivateKey => String("JWT_KEY");

    public static string String(string key)
    {
        return Environment.GetEnvironmentVariable(key) ?? "";
    }
}
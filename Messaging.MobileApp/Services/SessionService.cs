namespace Messaging.MobileApp.Services;

public static class SessionService
{
    public static string AuthToken 
    {
        get => Preferences.Get("kAuthToken", "");
        set => Preferences.Set("kAuthToken", value);
    }

    public static string APIUrl
    {
        get => Preferences.Get("kAPIURL", "");
        set => Preferences.Set("kAPIURL", value);
    }

    public static string Username
    {
        get => Preferences.Get("kUsername", "");
        set => Preferences.Set("kUsername", value);
    }
}

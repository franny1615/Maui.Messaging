using System.Text.Json.Serialization;

namespace Messaging.MobileApp.Models;

public class Auth
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}

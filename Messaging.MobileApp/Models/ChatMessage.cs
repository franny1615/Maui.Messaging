using System.Text.Json.Serialization;
using Messaging.MobileApp.Services;
using Microsoft.Maui.Controls.Shapes;

namespace Messaging.MobileApp.Models;

public class ChatMessage
{
    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("sentOn")]
    public DateTime? SentOn { get; set; }

    public string SentOnStr => $"{SentOn:F}";
    public Thickness Margin => User == SessionService.Username ? 
        new Thickness(64, 0, 0, 0) : 
        new Thickness(0, 0, 64, 0);
    public RoundRectangle BubbleShape => User == SessionService.Username ? 
        new RoundRectangle() { CornerRadius = new CornerRadius(5, 5, 5, 0) }:
        new RoundRectangle() { CornerRadius = new CornerRadius(5, 5, 0, 5) };
}
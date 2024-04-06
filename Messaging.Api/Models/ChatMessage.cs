namespace Messaging.Api.Models;

public class ChatMessage
{
    public string User { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime? SentOn { get; set; }
}

public static class Messages
{
    public static List<ChatMessage> Current = new();
}

using Messaging.Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace Messaging.Api.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message, int channel)
    {
        var newChat = new ChatMessage
        {
            User = user,
            Message = message,
            SentOn = DateTime.Now,
            Channel = channel
        };
        Messages.Current.Add(newChat);
        await Clients.All.SendAsync("ReceiveMessage", newChat.User, newChat.Message, newChat.SentOn, newChat.Channel);
    }
}

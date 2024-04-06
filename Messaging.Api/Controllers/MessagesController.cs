using Messaging.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Api.Controllers;

[ApiController]
[Route("messages")]
public class MessagesController
{
    [Route("all")]
    [HttpGet]
    [Authorize]
    public List<ChatMessage> GetAllMessages()
    {
        var result = Messages.Current.OrderBy((msg) => msg.SentOn).ToList();
        return result;
    }

    [Route("clear")]
    [HttpPost]
    [Authorize]
    public bool ClearAllCurrentMessages()
    {
        Messages.Current.Clear();
        return Messages.Current.Count == 0;
    }
}

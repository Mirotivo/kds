using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class WebRtcHub : Hub
{
    private readonly ILogger<UserController> _logger;
    private readonly Dictionary<string, string> _users;

    public WebRtcHub(ILogger<UserController> logger, Dictionary<string, string> users)
    {
        _logger = logger;
        _users = users;
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("userJoined", Context.ConnectionId);
    }




    public async Task sendReady(object message)
    {
        await Clients.Others.SendAsync("ReceiveReady", message);
    }

    public async Task sendBye(object message)
    {
        await Clients.Others.SendAsync("ReceiveBye", message);
    }

    public async Task SendIceCandidate(object message)
    {
        await Clients.Others.SendAsync("ReceiveIceCandidate", message);
    }

    public async Task SendSdpOffer(object message)
    {
        await Clients.Others.SendAsync("ReceiveSdpOffer", message);
    }
    
    public async Task SendSdpAnswer(object message)
    {
        await Clients.Others.SendAsync("ReceiveSdpAnswer", message);
    }


    

    
    public async Task Login(string userName)
    {
        // Add user to the active user list
        // Clients.All.SendAsync("userLoggedIn", userName);

        if (!_users.ContainsKey(userName))
        {
            _users.Add(userName, Context.ConnectionId);
        }
        else
        {
            _users[userName] = Context.ConnectionId;
        }
        await Clients.All.SendAsync("userLoggedIn", userName);
    }

    public void Logout(string userName)
    {
        // Remove user from the active user list
        Clients.All.SendAsync("userLoggedOut", userName);
    }

    // [MyCustom]
    public override async Task OnConnectedAsync()
    {
        // Retrieve custom value from the HttpContext
        // var customValue = Context.GetHttpContext().Items["URL"] as Microsoft.AspNetCore.Http.PathString;
        // var customValue = (Microsoft.AspNetCore.Http.PathString)Context.GetHttpContext().Items["URL"];
        // if (customValue.HasValue)
        // {
        //     _logger.LogInformation($"URL: {customValue.Value}");
        // }
        // // Use custom value in SignalR hub
        _logger.LogInformation($"Connecting : .....");
        try
        {
            // var userName = Context.User.Identity.Name;
            // if (userName == null)
            // {
            //     userName = (Context.GetHttpContext().Items["User"] as ClaimsPrincipal).Identity.Name;
            //     _logger.LogInformation($"userName: {userName}");
            // }
            // if (userName != null)
            // {
            //     var displayName = GetUserDisplayName(userName);
            //     _users[userName] = displayName;
            //     await Clients.All.SendAsync("UserConnected", displayName);
            // }
            // _users.Add(Context.ConnectionId);
            // _users.Add(Context.ConnectionId, Context.ConnectionId);
            // await Clients.All.SendAsync("userLoggedIn", Context.ConnectionId);
        }
        catch (System.Exception)
        {
            
        }
        finally {
            await base.OnConnectedAsync();
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // var userName = Context.User.Identity.Name;
        // var displayName = GetUserDisplayName(userName);
        // _users.Remove(userName);
        // await Clients.All.SendAsync("UserDisconnected", displayName);
        // await base.OnDisconnectedAsync(exception);

        try
        {
            // ConnectedUsers.Remove(Context.ConnectionId);
            // _users.Remove(Context.ConnectionId);
            // await Clients.All.SendAsync("userLoggedOut", Context.ConnectionId);
        }
        catch (System.Exception)
        {

        }
        finally
        {

        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task GetConnectedUsers()
    {
        var otherUsers = _users.Keys.Where(value => value != Context.ConnectionId);
        await Clients.All.SendAsync("ConnectedUsers", otherUsers);
    }

    private string GetUserDisplayName(string userName)
    {
        // TODO: implement your logic to get the user's display name
        // For example, you could retrieve it from a database or compute it from the user name
        return userName;
    }

}

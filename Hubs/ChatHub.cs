using Fxst.ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace Fxst.ChatService.Hubs
{
    public class ChatHub : Hub {
        public async Task JoinChat(UserConnection userConnection) {
            // Hub gives us access to the Clients property, which allows us to send messages to all connected clients
            await Clients.All // Send to all connected clients
                .SendAsync("ReceiveMessage", "System", $"{userConnection.UserName} has joined the chat.");
        }
        
        public async Task JoinSpecificChatRoom(UserConnection userConnection) {
            // Hub gives us access to the Groups property, which allows us to send messages to all clients in a specific group
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);
            await Clients.Group(userConnection.ChatRoom) // Send to all clients in the specified group
                .SendAsync("ReceiveMessage", "System", $"{userConnection.UserName} has joined {userConnection.ChatRoom}.");
        }
    }
}
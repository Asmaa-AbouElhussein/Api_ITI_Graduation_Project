using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using courses_site_api.DTOs;
using courses_site_api.models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace courses_site_api.SignalR
{
    
    public class ChatHub:Hub
    {
        private readonly courses_entitiy _context;
        private readonly static ConnectionMapping<string> connections= new ConnectionMapping<string>();
        public ChatHub(courses_entitiy crs)
        {
            _context = crs;
        }
        public async Task sendMess(chatDTO chatmess)
        {
            
            
           var senderObj= _context.registrations.FirstOrDefault(r => r.username == chatmess.Sender);
            if (senderObj is not null)
            {
                chat ch = new chat { Sender = chatmess.Sender, message = chatmess.message, Registrationid = senderObj.id,Receiver=chatmess.Receiver };
                 
                await _context.chats.AddAsync(ch);
               await _context.SaveChangesAsync();

                string name = Context.GetHttpContext().Request.Query["Username"];
                if (name != "Admin22")
                {
                    foreach (var connection in connections.Getconnections("Admin22"))
                    {

                      await Clients.Client(connection).SendAsync("serversend",chatmess);
                    }
                }
                else
                {
                    foreach (var connection in connections.Getconnections(chatmess.Receiver))
                    {
                      
                        await Clients.Client(connection).SendAsync("serversend", chatmess);
                    }
                }
               // var users = new string[] { chatmess.Sender, chatmess.Receiver };
            }
            //Clients.Caller.hello("sent");
            //Clients.Others.hello("New message");
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public  override Task OnConnectedAsync()
        {
            string name=Context.GetHttpContext().Request.Query["Username"];
            connections.Add(name, Context.ConnectionId);
           
              foreach (var username in connections.GetKeys())
                {
                   if (username != "Admin22")
                   {
                    foreach (var connection in connections.Getconnections("Admin22"))
                    {

                        Clients.Client(connection).SendAsync("onlineUsers", username);
                    }
                     }
                }
            

            return base.OnConnectedAsync();
        }      
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.GetHttpContext().Request.Query["Username"];

            
                
                    foreach (var connection in connections.Getconnections("Admin22"))
                    {

                        Clients.Client(connection).SendAsync("RemoveonlineUsers", name);
                    }
               
            
            connections.Remove(name, Context.ConnectionId);
            
            return base.OnDisconnectedAsync(exception);


        }
    }
}

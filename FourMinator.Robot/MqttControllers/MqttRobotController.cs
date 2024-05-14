using FourMinator.Persistence.Domain;
using FourMinator.RobotServices.Events;
using FourMinator.RobotServices.Hubs;
using Microsoft.AspNetCore.SignalR;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.RobotServices
{
    public class MqttRobotController 
    {

      
        private IRobotService _robotService;

        public MqttRobotController(IRobotService robotService)
        {
            _robotService = robotService;
        }
       


        public Task OnClientConnected(ClientConnectedEventArgs eventArgs)
        {
                    
                      

            Console.WriteLine($"Client test test test '{eventArgs.ClientId}' connected.");
            
            _robotService.UpdateRobotStatus(eventArgs.ClientId, RobotStatus.Online).Wait();

            return Task.CompletedTask;
        }

        public Task ValidateConnection(ValidatingConnectionEventArgs eventArgs)
        {
            Console.WriteLine($"Client '{eventArgs.ClientId}' wants to connect. Accepting!");
            return Task.CompletedTask;
        }

        public Task OnClientDisconnected(ClientDisconnectedEventArgs eventArgs)
        {
            Console.WriteLine($"Client '{eventArgs.ClientId}' disconnected.");
            _robotService.UpdateRobotStatus(eventArgs.ClientId, RobotStatus.Offline).Wait();
            
            return Task.CompletedTask;
        }


       
    }
}

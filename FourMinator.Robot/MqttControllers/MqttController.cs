using FourMinator.Persistence.Domain;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.RobotServices.MqttControllers
{
    public class MqttController
    {

        private readonly RobotService _robotService;

        public MqttController(RobotService robotService)
        {
            _robotService = robotService;
        }

        public Task OnClientConnected(ClientConnectedEventArgs eventArgs)
        {
            Console.WriteLine($"Client test test test '{eventArgs.ClientId}' connected.");
            _robotService.UpdateRobotStatus(eventArgs.ClientId, RobotStatus.Online);
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
            _robotService.UpdateRobotStatus(eventArgs.ClientId, RobotStatus.Offline);
            return Task.CompletedTask;
        }
    }
}

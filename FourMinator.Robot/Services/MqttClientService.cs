using HiveMQtt.Client;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.RobotServices.Services
{
    public class MqttClientService
    {
        
        private readonly IHiveMQClient _hiveMQClient;

        public MqttClientService(IHiveMQClient hiveMQClient)
        {
            _hiveMQClient = hiveMQClient;
        }


        public async Task ConnectAsync()
        {
            await _hiveMQClient.ConnectAsync();
        }


        public async Task PublishAsync(string topic, string payload)
        {
            await _hiveMQClient.PublishAsync(topic, payload);
        }

        public async Task SubscribeAsync(string topic)
        {
            await _hiveMQClient.SubscribeAsync(topic);
        }

        public bool IsConnected()
        {
            return _hiveMQClient.IsConnected();
        }




       


    }
}

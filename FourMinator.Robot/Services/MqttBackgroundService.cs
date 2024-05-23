using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.RobotServices.Services
{
    public class MqttBackgroundService : BackgroundService
    {
        private readonly MqttClientService _clientService;
        public MqttBackgroundService(MqttClientService clientService) { 
        
            _clientService = clientService;
        
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _clientService.ConnectAsync();

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (!_clientService.IsConnected())
                    {
                        Console.WriteLine("Attempting to reconnect...");
                        await _clientService.ConnectAsync();
                    }
                    await _clientService.PublishAsync("alive", "4MINATORSVC01");
                    await Task.Delay(1000, stoppingToken); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in MQTT background service: {ex.Message}");
            }
        }
    }
}

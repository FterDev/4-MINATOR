using Microsoft.AspNetCore.SignalR;

namespace FourMinator.RobotServices.Hubs
{
    public class RobotsHub : Hub
    {

        private readonly RobotService _robotService;

        public RobotsHub(RobotService robotService) { 
            _robotService = robotService;
        }
        public async Task GetRobots()
        { 
            var robots = await _robotService.GetAllRobots();
            await Clients.All.SendAsync("ReceiveRobots", robots);
        }
    }
}

using FourMinator.Persistence.Domain;
using Microsoft.AspNetCore.SignalR;

namespace FourMinator.RobotServices.Hubs
{
    public class RobotsHub : Hub
    {

        private readonly IRobotService _robotService;

        public RobotsHub(IRobotService robotService) { 
            _robotService = robotService;
        }
        public async Task GetRobots()
        { 
            var robots = await _robotService.GetAllRobots();
            await Clients.All.SendAsync("ReceiveRobots", robots);
        }

        public async Task UpdateRobotStatus(string robotName, RobotStatus status)
        {
            await _robotService.UpdateRobotStatus(robotName, status);
            await Clients.All.SendAsync("ReceiveRobots", await _robotService.GetAllRobots());   
        }

        public async Task CreateRobot(string robotName, string password, string nickname)
        {
            var res = await _robotService.CreateRobot(robotName, nickname, password);

            if(res)
            {
                await Clients.Caller.SendAsync("CreateSuccess");
                await GetRobots();
                return;
            }
            await Clients.Caller.SendAsync("CreateError");

        }
    }
}

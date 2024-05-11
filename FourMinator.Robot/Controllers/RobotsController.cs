
using FourMinator.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourMinator.RobotServices
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RobotsController
    {
        private readonly RobotService _robotService;

        public RobotsController(RobotService robotService)
        {
            _robotService = robotService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRobots()
        {
            var robots = await _robotService.GetAllRobots();
            return new OkObjectResult(robots);
        }   
    }
}

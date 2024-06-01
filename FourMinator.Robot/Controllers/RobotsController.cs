
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FourMinator.Auth;
using FourMinator.AuthServices.Middleware;
using FourMinator.RobotServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace FourMinator.RobotServices
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class RobotsController
    {
        private readonly IRobotService _robotService;
        private readonly FireAuth _fireAuth;
        private readonly MqttClientService _mqttClientService;

        public RobotsController(IRobotService robotService, FireAuth fireAuth, MqttClientService mqttClientService)
        {
            _robotService = robotService;
            _fireAuth = fireAuth;
            _mqttClientService = mqttClientService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRobots([FromHeader(Name = "Authorization")] string token)
        {
            
            token = token.Replace("Bearer ", "");

            try
            {                 
                var res = await _fireAuth.Authorize(token);
            }
            catch (Exception e)
            {
                return new UnauthorizedResult();
            }

            var robots = await _robotService.GetAllRobots();
            foreach (var robot in robots)
            {
                
                await _mqttClientService.PublishAsync($"robots/{robot.Name}/rand", "1");
            }
            return new OkObjectResult(robots);
        }   
    }
}

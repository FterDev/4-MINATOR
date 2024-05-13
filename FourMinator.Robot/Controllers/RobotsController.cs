
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FourMinator.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourMinator.RobotServices
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class RobotsController
    {
        private readonly RobotService _robotService;
        private readonly FirebaseApp _firebaseApp;

        public RobotsController(RobotService robotService, FirebaseApp firebaseApp)
        {
            _robotService = robotService;
            _firebaseApp = firebaseApp;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRobots()
        {
            var auth = FirebaseAuth.GetAuth(_firebaseApp);

            FirebaseToken res = await auth.VerifyIdTokenAsync("");

            Console.WriteLine(res.Uid);

            var robots = await _robotService.GetAllRobots();
            return new OkObjectResult(robots);
        }   
    }
}

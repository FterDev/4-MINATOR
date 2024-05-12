
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

            FirebaseToken res = await auth.VerifyIdTokenAsync("eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc2MDI3MTI2ODJkZjk5Y2ZiODkxYWEwMzdkNzNiY2M2YTM5NzAwODQiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vZm91cm1pbmF0b3ItemJ3IiwiYXVkIjoiZm91cm1pbmF0b3ItemJ3IiwiYXV0aF90aW1lIjoxNzE1NTM0NDgxLCJ1c2VyX2lkIjoiTU5kek9ScUFsOVFzenNMR1hZN3JLTFc5Qm13MSIsInN1YiI6Ik1OZHpPUnFBbDlRc3pzTEdYWTdyS0xXOUJtdzEiLCJpYXQiOjE3MTU1MzQ0ODEsImV4cCI6MTcxNTUzODA4MSwiZW1haWwiOiJmZWRvckB0ZXJla2hvdi5jaCIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJlbWFpbCI6WyJmZWRvckB0ZXJla2hvdi5jaCJdfSwic2lnbl9pbl9wcm92aWRlciI6InBhc3N3b3JkIn19.VzqDPPL0m1RAG2qNbN4_M1BEyYN8QYYbK8jF7pHyU8P3CRLMfgc5QruHVYYtcQAo6Pmd3gdw8EnV2ZSJLQf3TnGD1QyentsBtjbdq-0F4d4zcNV0zdmHDJid04QwjKcwsHFw5LfF11oZ62gU4Pv3erCVJ3jDa7RlKRysS6fU7PZjH7lGx02tbgUXi_DPVluX0R02ZYdLulTsD0PItJXYWM6pAhFc4NrxgLFZ6QMvIRdvYhkP0CIkkiA__6xnIf9WGg5BRo5QIjMm6T5hRKQLazl9b7ZHRjWDXMY2oSZg4umeCoe5xaUbWTVy6_0Y-_9txQvfNxGEqyzOYQR0989QSQ");

            Console.WriteLine(res.Uid);

            var robots = await _robotService.GetAllRobots();
            return new OkObjectResult(robots);
        }   
    }
}

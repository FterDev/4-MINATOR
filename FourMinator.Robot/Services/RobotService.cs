using FourMinator.Auth;
using FourMinator.Persistence;
using FourMinator.Persistence.Domain;
using FourMinator.RobotServices.Hubs;
using Microsoft.AspNetCore.SignalR;
using RandomString4Net;
using System.Text;
using XSystem.Security.Cryptography;

namespace FourMinator.RobotServices
{
    public class RobotService : IRobotService
    {
        private readonly IRobotRepository _robotRepository;
        private readonly IUserRepository _userRepository;
    


        private IHubContext<RobotsHub> HubContext { get; set; }

        public RobotService(FourminatorContext context, IHubContext<RobotsHub> robotsHub)
        {
            _robotRepository = new RobotRepository(context);
            _userRepository = new UserRepository(context);
            HubContext = robotsHub;
        }

        public ICollection<string> Errors { get; } = new List<string>();

        public async Task<bool> CreateRobot(string name, string userNickname,  string password)
        {

            var user = await GetUserByNickname(userNickname);

            var salt = RandomString.GetString(Types.ALPHANUMERIC_MIXEDCASE, 16);

            Robot robot = new Robot()
            {
                Name = name,
                Password = CreatePwHash(password, salt),
                Salt = salt,
                PublicKey = "",
                CreatedBy = user.Id,
                IsSuperUser = false,
                Status = (Int16)RobotStatus.Offline
            };
            

            if(await ValidateRobot(robot))
            {
                await _robotRepository.CreateRobot(robot);
                return true;
            }

            return false;
            
        }


        public async Task<bool> UpdateRobot(Robot robot)
        {
            if(await ValidateRobot(robot))
            {
                await _robotRepository.UpdateRobot(robot);
                return true;
            }
           
            return false;
        }


        public async Task UpdateRobotStatus(string name, RobotStatus status)
        {
            await _robotRepository.SetStatusByName(name, status);
            await this.HubContext.Clients.All.SendAsync("ReceiveRobots",  await this.GetAllRobots());
        }


        public async Task<IEnumerable<Robot>> GetAllRobots()
        {
            return await _robotRepository.GetAllRobots();
        }

        
        private async Task<bool> ValidateRobot(Robot robot)
        {
            RobotValidator validator = new RobotValidator();
            var res = await validator.ValidateAsync(robot);
            
            if(!res.IsValid)
            {
                foreach(var error in res.Errors)
                {
                    Errors.Add(error.ErrorMessage);
                }
                return false;
            }
            Errors.Clear();
            return true;
        }


        private async Task<User?> GetUserByNickname(string nickname)
        {
            return await _userRepository.GetUserByNickname(nickname);
        }

        private string CreatePwHash(string password, string salt)
        {
            
            byte[] bytes = Encoding.UTF8.GetBytes(password+salt);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string res = string.Empty;

            foreach(byte  b in hash)
            {
                res += String.Format("{0:x2}", b);
            }
            return res;
        }

       

  
    }
}

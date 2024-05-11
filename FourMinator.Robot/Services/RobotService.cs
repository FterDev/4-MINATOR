using FourMinator.Auth;
using FourMinator.Persistence.Domain;

namespace FourMinator.RobotService.Services
{
    internal class RobotService
    {
        private readonly IRobotRepository _robotRepository;
        private readonly IUserRepository _userRepository;
        public RobotService(IRobotRepository robotRepository, IUserRepository userRepository)
        {
            _robotRepository = robotRepository;
            _userRepository = userRepository;
        }

        public ICollection<string> Errors { get; } = new List<string>();

        public async Task<bool> CreateRobot( string name, string userEmail,  string password, string thumbprint, string publicKey)
        {

            Robot robot = new Robot()
            {
                Name = name,
                Password = password,
                Thumbprint = thumbprint,
                PublicKey = publicKey, 
                CreatedBy = await GetUserByEmail(userEmail),
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


        private async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        
    }
}

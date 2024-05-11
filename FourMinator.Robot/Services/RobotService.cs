using FourMinator.Persistence.Domain;


namespace FourMinator.RobotService.Services
{
    internal class RobotService
    {
        private IRobotRepository _robotRepository;
        public RobotService(IRobotRepository robotRepository)
        {
            _robotRepository = robotRepository;
        }

        public async Task CreateRobot(Robot robot)
        {
            await _robotRepository.CreateRobot(robot);
        }

        public async Task DeleteRobot(Robot robot)
        {
            await _robotRepository.DeleteRobot(robot);
        }

        public async Task<ICollection<Robot>> GetAllRobots()
        {
            return await _robotRepository.GetAllRobots();
        }

        public async Task<Robot?> GetRobotById(uint id)
        {
            return await _robotRepository.GetRobotById(id);
        }

        public async Task UpdateRobot(Robot robot)
        {
            await _robotRepository.UpdateRobot(robot);
        }

        
    }
}

using FourMinator.Persistence.Domain;


namespace FourMinator.RobotServices
{
    public interface IRobotRepository
    {
        Task CreateRobot(Robot robot);
        Task<ICollection<Robot>> GetAllRobots();
        Task<Robot?> GetRobotById(uint id);
        Task UpdateRobot(Robot robot);
        Task DeleteRobot(Robot robot);
    }
}

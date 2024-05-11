using FourMinator.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.RobotService
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

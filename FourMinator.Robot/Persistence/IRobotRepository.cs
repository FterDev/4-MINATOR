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
        Task CreateRobot(string name, string password, string thumbprint, string publicKey, User createdBy);
        Task<Robot?> GetRobotById(uint id);
        Task UpdateRobot(Robot robot);
        Task DeleteRobot(Robot robot);
    }
}

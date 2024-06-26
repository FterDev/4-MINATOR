using FourMinator.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.RobotServices
{
    public interface IRobotService
    {
        public ICollection<string> Errors { get; }
        public Task<bool> CreateRobot(string name, string userNickname, string password);
        public Task<bool> UpdateRobot(Robot robot);
        public Task UpdateRobotStatus(string name, RobotStatus status);

        public Task<IEnumerable<Robot>> GetAllRobots();

    }
}

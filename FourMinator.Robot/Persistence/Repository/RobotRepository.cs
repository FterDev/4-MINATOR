

using FourMinator.Persistence.Domain;
using FourMinator.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FourMinator.RobotServices
{
    public class RobotRepository : IRobotRepository
    {
        private FourminatorContext _context;
        public RobotRepository(FourminatorContext context)
        {
            _context = context;
        }
        public async Task CreateRobot(Robot robot)
        {
            await _context.Robots.AddAsync(robot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRobot(Robot robot)
        {
            _context.Robots.Remove(robot);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Robot>> GetAllRobots()
        {
            var res = await _context.Robots.ToListAsync();
            return res;
        }

        public async Task<Robot?> GetRobotById(uint id)
        {
            var robot = await _context.Robots.FindAsync(id);
            return robot;
        }

        public async Task UpdateRobot(Robot robot)
        {
           var res =  _context.Robots.Update(robot);
           await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace FourMinator.Auth
{
    public class UserController
    {
        private IUserRepository _userRepository;
        
        public UserController(DbContext context)
        {
            _userRepository = new UserRepository(context);
        }





    }
}

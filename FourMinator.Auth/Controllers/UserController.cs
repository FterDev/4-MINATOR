using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




namespace FourMinator.Auth
{
    [Route("api/[controller]")]
    public class UserController
    {
        private IUserRepository _userRepository;
        private IIdentityProviderAuthenticator _identityProviderAuthenticator;
        
        public UserController(AuthContext context, IIdentityProviderAuthenticator ipAuth)
        {
            _userRepository = new UserRepository(context);
            _identityProviderAuthenticator = ipAuth;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromHeader(Name = "Auth-Key")] string authKey, [FromBody] User user)
        {
            if(_identityProviderAuthenticator.ValidateAuthKey(authKey))
            {
                var userCheck = await _userRepository.GetUserByEmail(user.Email);
                if(userCheck == null)
                {
                    await _userRepository.CreateUser(user.Nickname, user.Email);
                    return new OkResult();
                }
                else
                {
                    return new BadRequestObjectResult("User already exists");
                }
            }
            else
            {
                return new UnauthorizedResult();
            }
        }



    }
}

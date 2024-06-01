
using FourMinator.Persistence.Domain;
using FourMinator.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FourMinator.GameServices.Persistence.Repository;
using FourMinator.GameServices.Persistence.Contracts;
using FourMinator.Persistence.Domain.Game;





namespace FourMinator.Auth
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private IUserRepository _userRepository;
        private IPlayerRepository _playerRepository;
        private IIdentityProviderAuthenticator _identityProviderAuthenticator;
        
        public UserController(FourminatorContext context, IIdentityProviderAuthenticator ipAuth)
        {
            _userRepository = new UserRepository(context);
            _playerRepository = new PlayerRepository(context);
            _identityProviderAuthenticator = ipAuth;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromHeader(Name = "Auth-Key")] string authKey, string externalId, string nickname)
        {
            if(_identityProviderAuthenticator.ValidateAuthKey(authKey))
            {
                var user = await _userRepository.CreateUser(nickname, externalId);
                await _playerRepository.CreatePlayer(new Player { UserId = user.Id, IsBot = false, State = -1 });
                return new OkResult();
            }
            else
            {
                return new UnauthorizedResult();
            }
        }



    }
}

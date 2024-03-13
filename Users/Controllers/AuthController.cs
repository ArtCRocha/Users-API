using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Users.Commands;
using Users.Exceptions;
using Users.Models;
using Users.Repositories;
using Users.Results;
using Users.Utils;

namespace Users.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepostory;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepostory, ITokenService tokenService, IMapper mapper)
        {
            _userRepostory = userRepostory;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<TokenResult> Login(LoginCommand command)
        {
            var user = await _userRepostory.GetUserByEmail(command.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            {
                throw new NotFoundException("Usuário já existe");
            }

            var token = _tokenService.GenerateToken(user);

            return new TokenResult(token);
        }

        [HttpPost("register")]
        public async Task<TokenResult> Register( RegisterCommand command)
        {

            var existingUser = await _userRepostory.GetUserByEmail(command.Email);

            if(existingUser != null)
            {
                throw new NotFoundException("Usuário já existe");
            }

            var passowrd = BCrypt.Net.BCrypt.HashPassword(command.Password);
            var user = new User(command.Name, command.Email, passowrd, null);
            await _userRepostory.Create(user);

            var token = _tokenService.GenerateToken(user);

            return new TokenResult(token);
        }

        [HttpGet("userId")]
        public Guid GetUserId()
        {
            var userId =  User.Identity?.Name;
            return userId == null ? Guid.Empty : Guid.Parse(userId);
        }


        [HttpGet("me"), Authorize]
        public async Task<UserResult> GetMe()
        {
            var userId = GetUserId();

            var user = await _userRepostory.GetUserById(userId);

            if (user == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            return _mapper.Map<UserResult>(user);
        }
    }
}

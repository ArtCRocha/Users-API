using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.DTO;
using Users.Models;
using Users.Repositories;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _context;

        public UserController(IUserRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task Create([FromForm] UserDto userDto)
        {
            var filePath = Path.Combine("Storage", userDto.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            userDto.Photo.CopyTo(fileStream);

            var user = new User(userDto.Name, userDto.Email,  userDto.Password, filePath);
            await _context.Create(user);
        }

        [HttpGet]
        [Route("{id}/photo")]
        public async Task<FileContentResult> GetUserPhoto(Guid id)
        {
            var user = await _context.GetUserById(id);
            var dataBytes = System.IO.File.ReadAllBytes(user.Photo);
            return File(dataBytes, "image/png");
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await _context.GetAll();
        }
    }
}

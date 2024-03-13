using AutoMapper;
using Users.Models;
using Users.Results;

namespace Users.Mappers
{
    public class UserMapper : Profile
    {
       public UserMapper() {
            CreateMap<User, UserResult>();
       }
    }
}

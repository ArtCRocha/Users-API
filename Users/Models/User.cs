using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Models
{
    [Table("users")]
    public class User
    {
        public Guid Id { get; init; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Photo {  get; set; }
       

        public User(string name, string email, string password, string photo)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            Photo = photo;
           
        }
    }
}

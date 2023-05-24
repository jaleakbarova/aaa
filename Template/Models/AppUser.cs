
using Microsoft.AspNetCore.Identity;
namespace Template.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
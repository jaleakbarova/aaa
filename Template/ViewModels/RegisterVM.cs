using System.ComponentModel.DataAnnotations;

namespace Template.View_Models
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        [MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }

    }
}


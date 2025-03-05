using System.ComponentModel.DataAnnotations;

namespace NEWZEAL_LAND_WORK_API.UsersLoging
{
    public class RegisterUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string password { get; set; }

        public string[] Roles { get; set; }

       
    }
}

using System.ComponentModel.DataAnnotations;

namespace NEWZEAL_LAND_WORK_API.UsersLoging
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace JagraTaskManager.Shared.Dto
{
    public class UserForRegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime TimeOfRegister { get; set; }

        public UserForRegisterDto()
        {
            TimeOfRegister = DateTime.Now;
        }
    }
}

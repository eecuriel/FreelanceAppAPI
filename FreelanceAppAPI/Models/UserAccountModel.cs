using System.ComponentModel.DataAnnotations;

namespace FreelanceAppAPI.Models
{
    public class UserAccountModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

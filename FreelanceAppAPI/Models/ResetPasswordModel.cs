using System.ComponentModel.DataAnnotations;

namespace FreelanceAppAPI.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="El password a confirmar no es igual")]
        public string ConfirmNewPassword { get; set; }
        
    }
}
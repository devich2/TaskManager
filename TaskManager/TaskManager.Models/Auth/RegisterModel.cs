using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Auth
{
    public class RegisterModel
    {		
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required] [EmailAddress] public string Email { get; set; }
        
        [Required] 
        [MinLength(6)] 
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
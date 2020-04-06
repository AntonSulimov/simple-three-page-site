using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}

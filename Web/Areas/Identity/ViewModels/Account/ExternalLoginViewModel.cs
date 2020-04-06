using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Identity.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }
    }
}

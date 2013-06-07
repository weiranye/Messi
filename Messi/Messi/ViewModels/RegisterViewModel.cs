using System.ComponentModel.DataAnnotations;

namespace Messi.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
    }
}
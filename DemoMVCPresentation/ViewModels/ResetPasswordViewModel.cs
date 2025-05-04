using System.ComponentModel.DataAnnotations;

namespace DemoMVCPresentation.ViewModels
{
    public class ResetPasswordViewModel
    {

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}

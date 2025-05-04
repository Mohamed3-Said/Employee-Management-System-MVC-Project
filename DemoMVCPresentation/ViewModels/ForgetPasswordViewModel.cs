using System.ComponentModel.DataAnnotations;

namespace DemoMVCPresentation.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; } = null!;
    }
}

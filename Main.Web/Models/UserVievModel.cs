using RR.AttributeService_V1;
using RR.Common_V1;
using System.ComponentModel.DataAnnotations;

namespace Main.Web.Models
{
    public class UserVievModel
    {
        [StringLength(60, ErrorMessage = ">3 <60", MinimumLength = 3)]
        [ViewModel(
            ViewModelPropertyType = ViewModelPropertyType.String,
            Label = "UserName",
            ErrorMessage = "Not valid",
            HaveToValidate = true,
            ViewModelValidationTypes = new[] {
                ViewModelValidationType.NotNull
        })]
        public string Name { get; set; } = "Luke";

        [StringLength(60, ErrorMessage = ">3 <60", MinimumLength = 3)]
        [ViewModel(
            ViewModelPropertyType = ViewModelPropertyType.String,
            Label = "UserName",
            ErrorMessage = "Not valid",
            HaveToValidate = true
        )]
        public string Vorname { get; set; } = "Dieter";

        [StringLength(60, ErrorMessage = ">3 <60", MinimumLength = 3)]
        [ViewModel(
            ViewModelPropertyType = ViewModelPropertyType.String,
            HaveToValidate = true,
            ViewModelValidationTypes = new[] {
                ViewModelValidationType.NotNull
        })]
        public string Password { get; set; } = "123";
    }
}
using RR.AttributeService_V1;
using RR.Common_V1;

namespace Main.Web.Models
{
    public class UserVievModel
    {
        [ViewModel(
          ViewModelPropertyType = ViewModelPropertyType.String,
          Label = "UserName",
          ErrorMessage = "Not valid",
          HaveToValidate = true)]
        public string Name { get; set; } = "Rene";

        [ViewModel(
            ViewModelPropertyType = ViewModelPropertyType.String,
            HaveToValidate = true
        )]
        public string Vorname { get; set; }

        [ViewModel(
            ViewModelPropertyType = ViewModelPropertyType.String,
            HaveToValidate = true,
            ViewModelValidationTypes = new[] {
                ViewModelValidationType.NotNull
        })]
        public string Password { get; set; }
    }
}
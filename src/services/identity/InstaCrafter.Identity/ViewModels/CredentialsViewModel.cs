using FluentValidation.Attributes;
using InstaCrafter.Identity.ViewModels.Validations;

namespace InstaCrafter.Identity.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
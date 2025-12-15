using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Authentication.Command.Models;

namespace ThyroCareX.Core.Feature.Authentication.Command.Validation
{
    public class SignInCommandValidator:AbstractValidator<SignInCommand>
    {

        #region Field

        #endregion
        #region Constructor
        public SignInCommandValidator()
        {
            ApplyValidationRules();
        }
        #endregion
        #region Handle Function
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
             .EmailAddress().WithMessage("A valid email is required.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\!\@\#\$\%\^\&\*]").WithMessage("Password must contain at least one special character (!@#$%^&*).");

        }
        #endregion



    }
}

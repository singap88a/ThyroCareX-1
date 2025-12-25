using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.AplicationUser.Command.Models;

namespace ThyroCareX.Core.Feature.AplicationUser.Command.Validation
{
    public class ChangeUserPasswordValidator:AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Failed

        #endregion

        #region Construcor
        public ChangeUserPasswordValidator()
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

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("CurrentPassword is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("NewPassword is required")
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("New password must be different from current password.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\!\@\#\$\%\^\&\*]").WithMessage("Password must contain at least one special character (!@#$%^&*).");


            RuleFor(x=>x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
        }
        #endregion
    }
}

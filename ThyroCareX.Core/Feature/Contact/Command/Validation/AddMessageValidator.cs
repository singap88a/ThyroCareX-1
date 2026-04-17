using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Contact.Command.Models;

namespace ThyroCareX.Core.Feature.Contact.Command.Validation
{
    public class AddMessageValidator: AbstractValidator<AddMessageCommand>
    {

        public AddMessageValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {


            RuleFor(x => x.FullName)
                    .NotEmpty().WithMessage("Full name is required.")
                    .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("A valid email address is required.")
                    .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.");

            RuleFor(x => x.Subject)
                    .NotEmpty().WithMessage("Subject is required.")
                    .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");

            RuleFor(x => x.Message)
                    .NotEmpty().WithMessage("Message is required.")
                    .MaximumLength(1000).WithMessage("Message cannot exceed 1000 characters.");

           

            
        }
    }
}

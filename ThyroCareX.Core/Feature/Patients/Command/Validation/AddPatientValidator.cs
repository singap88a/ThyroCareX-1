using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Patients.Command.Model;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Patients.Command.Validation
{
    public class AddPatientValidator:AbstractValidator<AddPatientCommand>
    {
        private readonly IPatientService _patientService;
        public AddPatientValidator(IPatientService patientService)
        {
            _patientService = patientService;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
               .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
               .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
               .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
               .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }

    }
}

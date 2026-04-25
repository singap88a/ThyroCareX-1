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
              .NotEmpty().WithMessage("Full name is required")
              .MaximumLength(100).WithMessage("Full name must not exceed 100 characters");

            // Date Of Birth
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past");

            // Gender
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required");

            // Height
            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than 0");

            // Weight
            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0");

            // Phone Number
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^01[0-2,5]{1}[0-9]{8}$")
                .WithMessage("Invalid Egyptian phone number format");
              

            // Address
            RuleFor(x => x.Address)
                .MaximumLength(250).WithMessage("Address too long");

            // Attachment
            //RuleFor(x => x.AttachmentPath)
            //    .NotEmpty().WithMessage("Attachment is required");
        }

    }
}

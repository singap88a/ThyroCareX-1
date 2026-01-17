using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Doctors.Commands.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Doctors.Commands.Validation
{
    public class EditDoctorValidator: AbstractValidator<EditDoctorCommand>
    {
        private readonly IDoctorService _doctorService;
        public EditDoctorValidator(IDoctorService doctorService)
        {
            
            _doctorService = doctorService;
            ApplyValidationRules();
            ApplyCustomValidationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor (x=> x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");
          
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Email)
                .MustAsync(async (model, Key, CancellationToken) => !await _doctorService.IsEmailExistExcludeSelf(Key, model.Id))
                .WithMessage("Email is already taken by another doctor.");
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .MustAsync(async (model, Key, CancellationToken) => !await _doctorService.IsPhoneExistExcludeSelf(Key, model.Id))
                .WithMessage("Phone Number is already taken.");
            
            RuleFor(x => x.MedicaLicenseNumber)
                .MaximumLength(50).WithMessage("License number too long");

            RuleFor(x => x.ProfessionalBio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters");

            RuleFor(x=>x.ProfileImage)
                .Must(file => file.Length > 0 &&
                                file.Length <= 5 * 1024 * 1024 &&
                                (file.ContentType == "image/jpeg" ||
                                file.ContentType == "image/png"))
                .WithMessage("File must be JPEG or PNG and less than 5MB.");
        }
    }
}

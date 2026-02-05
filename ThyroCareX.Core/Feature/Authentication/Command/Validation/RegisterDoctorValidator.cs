using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Authentication.Command.Models;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Core.Feature.Authentication.Command.Validation
{
    public class RegisterDoctorValidator:AbstractValidator<RegisterDoctorCommand>
    {

        #region Field
        private readonly IDoctorService _doctorService;
        #endregion
        #region Constructor
        public RegisterDoctorValidator(IDoctorService doctorService)
        {
            _doctorService = doctorService;
            ApplyValidationRules();

        }
        #endregion

        #region Handle Function

        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.")
                .MustAsync(async (email, cancellation) => !await _doctorService.IsEmailTakenAsync(email))
                    .WithMessage("Email is already registered.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\!\@\#\$\%\^\&\*]").WithMessage("Password must contain at least one special character (!@#$%^&*).");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Phone number is invalid.")
                .MustAsync(async (phone, cancellation) => !await _doctorService.IsPhoneTakenAsync(phone))
                    .WithMessage("Phone number is already registered.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender must be valid.");

            RuleFor(x => x.DateofBirth)
                 .NotEmpty().WithMessage("Date of Birth is required.")
                  .Must(dob => dob <= DateOnly.FromDateTime(DateTime.Today.AddYears(-25)))
                   .WithMessage("Doctor must be at least 25 years old.");

            RuleFor(x => x.MedicalLicenseNumber)
                    .NotEmpty().WithMessage("Medical License Number is required.");

            RuleFor(x => x.Hospital)
                 .NotEmpty().WithMessage("Hospital is required.");


            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50);

            RuleFor(x => x.IdentificationImage)
                .NotNull()
                .WithMessage("Doctor must upload an image.")
                .Must(file => file.Length > 0 &&
                                file.Length <= 5 * 1024 * 1024 &&
                                (file.ContentType == "image/jpeg" ||
                                file.ContentType == "image/png"))
                .WithMessage("File must be JPEG or PNG and less than 5MB.");


            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{4,10}$").When(x => !string.IsNullOrEmpty(x.ZipCode))
                .WithMessage("Zip code must be numeric and between 4 to 10 digits.");

           

        }
        #endregion

    }
}

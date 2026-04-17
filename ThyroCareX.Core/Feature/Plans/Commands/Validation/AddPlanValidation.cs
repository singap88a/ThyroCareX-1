using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Plans.Commands.Model;

namespace ThyroCareX.Core.Feature.Plans.Commands.Validation
{
    public class AddPlanValidation: AbstractValidator<AddPlanCommand>
    {
        public AddPlanValidation() 
        {
            RuleFor(x => x.PlanType)
           .IsInEnum()
           .WithMessage("Invalid plan type");

            // Description
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters");

            // Price
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            // DurationInDays
            RuleFor(x => x.DurationInDays)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0 days")
                .LessThanOrEqualTo(3650) // مثلاً max 10 سنين
                .WithMessage("Duration is too long");
        }
    }
}

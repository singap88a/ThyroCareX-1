using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ThyroCareX.Core.Feature.Patients.Command.Model;

namespace ThyroCareX.Core.Feature.Patients.Command.Validation
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator()
        {
            RuleFor(x => x.PatientID)
                .GreaterThan(0)
                .WithMessage("Patient ID must be greater than zero.");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.TestWithAI.Commands.Models;

namespace ThyroCareX.Core.Feature.TestWithAI.Commands.Valditors
{
    public class AddTestValidator: AbstractValidator<AddTestCommand>
    {
        public AddTestValidator()
        {
            

        }
    }
}

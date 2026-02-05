using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Community.Command.Model;

namespace ThyroCareX.Core.Feature.Community.Command.Validation
{
    public class AddPostValidator: AbstractValidator<AddPostCommand>
    {
        #region Prop

        #endregion
        #region Constructor
        public AddPostValidator() 
        {
             ApplyValidationRules();
        }
        #endregion
        #region Handle Functions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Post content is required")
                .Must(content => !content.Contains('<') && !content.Contains('>'))
                .WithMessage("Content must not contain < or > characters")
                ;
        }
            #endregion
    }
}

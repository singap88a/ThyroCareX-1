using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Feature.Community.Command.Model;

namespace ThyroCareX.Core.Feature.Community.Command.Validation
{
    public class AddPostLikeValidator:AbstractValidator<AddPostLikeCommand>
    {
        #region Fieldes
        #endregion
        #region Cons
        public AddPostLikeValidator()
        {
            ApplyValidationRules();
        }
        #endregion
        #region Handle Functions
        public void ApplyValidationRules()
        {

        }
        #endregion
    }
}

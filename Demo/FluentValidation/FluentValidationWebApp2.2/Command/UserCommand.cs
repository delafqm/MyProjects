using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebApp2._2.Models.Command
{
    public class UserCommand
    {
        public ValidationResult validationResult;

        public bool IsValid(User user)
        {
            validationResult = new UserCommandValidation().Validate(user);

            return validationResult.IsValid;
        }
    }
}

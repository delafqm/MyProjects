using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebApp2._2.Models.Command
{
    public class UserCommand
    {
        //定义验证结果
        public ValidationResult validationResult;

        public bool IsValid(User user)
        {
            //验证结果，初始化并进行验证
            validationResult = new UserCommandValidation().Validate(user);
            //返回验证结果
            return validationResult.IsValid;
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebApp2._2.Models.Command
{
    //用户验证-泛型
    public class UserValidation<T> : AbstractValidator<T> where T : User
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("名字不能为空")
                .Length(4, 10).WithMessage("长度必需在4~10字符之间");
        }
    }
}

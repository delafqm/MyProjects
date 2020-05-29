using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationWebApp2._2.Models.Command
{
    //用户命令验证模型
    public class UserCommandValidation : UserValidation<User>
    {
        public UserCommandValidation()
        {
            ValidateName();
        }
    }
}

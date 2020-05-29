using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidationWebApp2._2.Models;
using FluentValidationWebApp2._2.Models.Command;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FluentValidationWebApp2._2.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpPost("PostName")]
        public Task<string> PostName(User user)
        {
            return Task.FromResult("OK");
        }

        [HttpGet("GetName")]
        public List<string> GetName()
        {
            User user = new User() { Name = "1" };
            UserCommand userCommand = new UserCommand();
            List<string> errorInfo = new List<string>();

            //手动调用验证
            if (!userCommand.IsValid(user))
            {
                foreach (var error in userCommand.validationResult.Errors)
                {
                    errorInfo.Add(error.ErrorMessage);
                }
            }


            return errorInfo;
        }
    }
}

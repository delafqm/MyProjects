using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapperForWebApp2._2.AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoMapperForWebApp2._2.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("GetOne")]
        public UserDto GetOne()
        {
            User user = new User() { Name = "delafqm" };
            return user.MapTo<UserDto>();
        }

        [HttpGet("GetList")]
        public List<UserDto> GetList()
        {
            List<User> users = new List<User>() {
             new User(){ Name="delafqm1" },
             new User(){ Name="delafqm2" },
             new User(){ Name="delafqm3" }
            };

            return users.MapToList<UserDto>();
        }

        [HttpGet("GetOne1")]
        public UserDto1 GetOne1()
        {
            User1 user = new User1() { Name1 = "delafqm10" };
            return user.MapTo<UserDto1>();
        }

        [HttpGet("GetList1")]
        public List<UserDto1> GetList1()
        {
            List<User1> users = new List<User1>() {
             new User1(){ Name1="delafqm11" },
             new User1(){ Name1="delafqm21" },
             new User1(){ Name1="delafqm31" }
            };

            return users.MapToList<UserDto1>();
        }
    }
}

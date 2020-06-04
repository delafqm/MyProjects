using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostgreWebApp2._2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgreWebApp2._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        UserDbContext _Context;
        public ValuesController(UserDbContext context)
        {
            _Context = context;
        }

        [HttpGet("User")]
        public string GetValues()
        {
            var query = _Context.User.ToList();

            return "ok";
        }
    }
}

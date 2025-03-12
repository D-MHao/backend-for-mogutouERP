using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using CYQ.Data;
using CYQ.Data.Table;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public Object Login([FromBody]JsonObject json) {
            var tel = json["tel"];
            var password = json["password"];
            var result = new { token = tel };
            CacheUtils.cache.Set(tel.ToString(), 1,30.0);
            return result;
        }

        [HttpGet("user")]
        public User GetRole()
        {
            var token = Request.Headers["Authorization"];
            Entity.User user = new Entity.User();
            user.roles = new String[] { "admin","PM","CM" };
            return user;
        }

        [HttpGet("users")]
        public List<Company> GetUsers()
        {
            using (MAction action = new MAction("Company"))
            {
                MDataTable mDataTable = action.Select();
                return mDataTable.ToList<Company>();
            }
        }
    }
}
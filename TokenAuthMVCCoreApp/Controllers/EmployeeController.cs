using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthMVCCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Authorize]

        //authorize this controller by passing Authorization as Bearer space_ the JWT token to get 200 response esle 401
        public IActionResult Get()
        {
            return Ok(new string[] { "e1", "e2", "e3" });
        }
    }
}

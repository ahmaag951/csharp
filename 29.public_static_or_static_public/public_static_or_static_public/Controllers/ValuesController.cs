using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace public_static_or_static_public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // you can have both static public or public static no problem in that
            // see the Helper classes
            Helper1.Id = "1";
            Helper2.Id = "2";
            return new string[] { Helper1.Id, Helper2.Id };
        }

    }
}

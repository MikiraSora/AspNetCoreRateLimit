using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreRateLimit.Demo.Controllers
{
    [Route("api")]
    public class ValuesController : Controller
    {
        [Route("zz")]
        public IActionResult ZZ()
        {
            return Content("zz");
        }

        [Route("a/a")]
        public IActionResult A()
        {
            return Content("a");
        }

        [Route("a/b")]
        public IActionResult B()
        {
            return Content("b");
        }
    }
}

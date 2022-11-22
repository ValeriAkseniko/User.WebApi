using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.WebApi.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        public string Home()
        {
            var home = "Server is running";
            return home;
        }
    }
}

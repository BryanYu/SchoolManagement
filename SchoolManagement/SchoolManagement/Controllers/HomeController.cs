using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello from MVC";
        }
    }
}

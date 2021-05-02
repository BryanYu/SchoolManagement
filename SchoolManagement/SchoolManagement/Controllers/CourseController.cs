using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.DataRepositories;

namespace SchoolManagement.Controllers
{
    public class CourseController : Controller
    {

        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

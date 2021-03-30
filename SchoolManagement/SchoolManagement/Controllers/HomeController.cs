using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using SchoolManagement.DataRepositories;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public string Index()
        {
            var student = _studentRepository.GetStudent(1);
            return student.Name;
        }

        public ViewResult Details()
        {
            var model = this._studentRepository.GetStudent(1);
            ViewBag.PageTitle = "學生詳情";
            return View(model);
        }
    }
}

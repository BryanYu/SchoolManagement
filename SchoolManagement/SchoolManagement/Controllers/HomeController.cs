using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using SchoolManagement.DataRepositories;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public ViewResult Index()
        {
            var students = this._studentRepository.GetAllStudents();
            return View(students);
        }

        public ViewResult Details()
        {
            var model = this._studentRepository.GetStudent(1);
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                PageTitle = "學生詳情",
                Student = model
            };
            return View(homeDetailsViewModel);
        }
    }
}

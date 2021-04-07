using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using SchoolManagement.DataRepositories;
using SchoolManagement.Models;
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

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                Student newStudent = _studentRepository.Add(student);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }

            return View();

        }

        public ViewResult Details(int id)
        {
            var model = this._studentRepository.GetStudent(id);
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                PageTitle = "學生詳情",
                Student = model
            };
            return View(homeDetailsViewModel);
        }
    }
}

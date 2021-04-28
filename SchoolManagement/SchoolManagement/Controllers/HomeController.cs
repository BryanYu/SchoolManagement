using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagement.DataRepositories;
using SchoolManagement.Models;
using SchoolManagement.Security;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDataProtector _dataProtector;

        public HomeController(IStudentRepository studentRepository, 
            IWebHostEnvironment webHostEnvironment, IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
            _dataProtector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.StudentIdRouteValue);
        }

        public ViewResult Index()
        {
            var students = this._studentRepository.GetAllStudents().Select(item =>
            {
                item.EncryptedId = _dataProtector.Protect(item.Id.ToString());
                return item;
            });
            return View(students);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var uniqueFileName = string.Empty;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (var photo in model.Photos)
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }

                var student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    PhotoPath = uniqueFileName
                };
                Student newStudent = _studentRepository.Insert(student);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
            return View();

        }

        public ViewResult Details(string id)
        {
            var decryptedId = _dataProtector.Unprotect(id);
            var decryptStudentId = Convert.ToInt32(decryptedId);
            var student = this._studentRepository.GetStudentById(decryptStudentId);
            if (student == null)
            {
                ViewBag.ErrorMessage = $"學生Id={id}資料不存在";
                return View("NotFound");
            }

            var homeDetailsViewModel = new HomeDetailsViewModel
                {
                    PageTitle = "學生詳情",
                    Student = student
                };
            

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var student = _studentRepository.GetStudentById(id);
            if (student == null)
            {
                ViewBag.ErrorMessage = $"學生Id={id}資料不存在";
                return View("NotFound");
            }
            var viewModel = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Major = student.Major,
                ExistingPhotoPath = student.PhotoPath
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _studentRepository.GetStudentById(model.Id);
                student.Name = model.Name;
                student.Email = model.Email;
                student.Major = model.Major;

                if (model.Photos.Count > 0)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars",
                            model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    student.PhotoPath = ProcessUploadedFile(model);
                }

                var updateStudent = _studentRepository.Update(student);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        private string ProcessUploadedFile(StudentEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos.Count > 0)
            {
                foreach (var photo in model.Photos)
                {
                    var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
    }
}

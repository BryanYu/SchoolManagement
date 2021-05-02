using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Models;
using SchoolManagement.Security;
using SchoolManagement.ViewModels;
using System.Linq.Dynamic.Core;
using SchoolManagement.Application;
using SchoolManagement.Application.Dtos;


namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Student, int> _studentRepository;
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDataProtector _dataProtector;

        public HomeController(IRepository<Student, int> studentRepository, 
            IWebHostEnvironment webHostEnvironment, 
            IDataProtectionProvider dataProtectionProvider, 
            DataProtectionPurposeStrings dataProtectionPurposeStrings, 
            IStudentService studentService
        )
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
            _dataProtector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.StudentIdRouteValue);
            _studentService = studentService;
        }

        public async Task<ViewResult> Index(string searchString, int currentPage = 1, string sortBy = "Id")
        {
            ViewBag.CurrentFilter = searchString = searchString?.Trim();
            var paginationModel = new PaginationModel();
            paginationModel.Count = await _studentRepository.CountAsync();
            paginationModel.CurrentPage = currentPage;
            paginationModel.PageSize = 2;

            var student = await _studentService.GetPaginatedResult(paginationModel.CurrentPage, searchString, sortBy, paginationModel.PageSize);
            paginationModel.Data = student.Select(item =>
            {
                item.EncryptedId = _dataProtector.Protect(item.Id.ToString());
                return item;
            }).ToList();
            return View(paginationModel);
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
                    PhotoPath = uniqueFileName,
                    EnrollmentDate = model.EnrollmentDate
                };
                Student newStudent = _studentRepository.Insert(student);
                var encryptedId = _dataProtector.Protect(newStudent.Id.ToString());
                return RedirectToAction("Details", new { id = encryptedId });
            }
            return View();

        }

        public ViewResult Details(string id)
        {
            var student = DecryptedStudent(id);
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
        public ViewResult Edit(string id)
        {
            var student = DecryptedStudent(id);
            if (student == null)
            {
                ViewBag.ErrorMessage = $"學生Id={id}資料不存在";
                return View("NotFound");
            }
            var viewModel = new StudentEditViewModel
            {
                Id = id,
                Name = student.Name,
                Email = student.Email,
                Major = student.Major,
                ExistingPhotoPath = student.PhotoPath,
                EnrollmentDate = student.EnrollmentDate
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = DecryptedStudent(model.Id);
                student.Name = model.Name;
                student.Email = model.Email;
                student.Major = model.Major;
                student.EnrollmentDate = model.EnrollmentDate;

                if (model.Photos != null && model.Photos.Count > 0)
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

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(item => item.Id == id);
            if (student == null)
            {
                ViewBag.ErrorMessage = $"找不到id為{id}的資料";
                return View("NotFound");
            }

            await _studentRepository.DeleteAsync(item => item.Id == id);
            return RedirectToAction("Index");
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

        private Student DecryptedStudent(string id)
        {
            var decryptedId = _dataProtector.Unprotect(id);
            var decryptStudentId = Convert.ToInt32(decryptedId);
            var student = this._studentRepository.FirstOrDefault(item => item.Id == decryptStudentId);
            return student;
        }
    }
}

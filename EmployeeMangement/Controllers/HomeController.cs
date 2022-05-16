using EmployeeMangement.Models;
using EmployeeMangement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _IEmployeeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository IEmployeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            _IEmployeeRepository = IEmployeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _IEmployeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEdit = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPat
            };
            return View(employeeEdit);
        } 
        
      





        public ViewResult Index()
        {
            var model = _IEmployeeRepository.GetEmployees();
            return View(model);
        }








        [HttpPost]
        public IActionResult Details()
        {
            return View();
        }

            [HttpGet]
            public ViewResult Details(int? id)
        {
            //return "id = " + id.Value.ToString() + " name = " + name;

            //Employee emp = _IEmployeeRepository.GetEmployee(id ?? 1);

            HomeDetailsViewModel homeDetailsView = new HomeDetailsViewModel()
            {
                Employee = _IEmployeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Employee Details"
            };
            return View(homeDetailsView);
        }

        
        



        [HttpGet]
        
        public IActionResult Delete(int id)
        {
            var employee = _IEmployeeRepository.Delete(id);
            
            return View("Index",employee);
        } 
        
       




            [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel EditViewModel)
        {
            if (ModelState.IsValid)
            {

                Employee employee = _IEmployeeRepository.GetEmployee(EditViewModel.Id);
                employee.Name = EditViewModel.Name;
                employee.Email = EditViewModel.Email;
                employee.Department = EditViewModel.Department;

                if(EditViewModel.IformPhoto != null)
                {     
                    if(EditViewModel.ExistingPhotoPath != null)
                    {
                        string filepath = Path.Combine(_hostingEnvironment.WebRootPath, "images", EditViewModel.ExistingPhotoPath);
                        System.IO.File.Delete(filepath);
                    }
                employee.PhotoPat = FileUploader(EditViewModel);
                }

               

                _IEmployeeRepository.Update(employee);
                return RedirectToAction("details");

            }
            return View();
        }

        private string FileUploader(EmployeeCreateViewModel createViewModel)
        {
            string uniqueFileName = null;
            if (createViewModel.IformPhoto != null && createViewModel.IformPhoto.Count > 0)
            {
                foreach (IFormFile photo in createViewModel.IformPhoto)

                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }     
        
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel createViewModel)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = FileUploader(createViewModel);

                //if (createViewModel.IformPhoto != null && createViewModel.IformPhoto.Count > 0)
                //{
                //    foreach(IFormFile photo in createViewModel.IformPhoto)

                //    {
                //        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                //        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //        using (var fileStream = new FileStream(filePath, FileMode.Create))
                //        {
                //            photo.CopyTo(fileStream);
                //        }

                //    }
                  

            Employee newEmployee = new Employee
            {
                Name = createViewModel.Name,
                Email = createViewModel.Email,
                Department = createViewModel.Department,
                PhotoPat = uniqueFileName
                };

                _IEmployeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });

            }
            return View();
        }











    }
}

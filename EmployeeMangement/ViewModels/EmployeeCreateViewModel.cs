using EmployeeMangement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModels
{
    public class EmployeeCreateViewModel
    {
        
        [Required]
        [MaxLength(50, ErrorMessage = "Name Connot exceed 50 character")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([\w\-\.]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        public Dept? Department { get; set; }
        public List<IFormFile> IformPhoto { get; set; }
    }
}

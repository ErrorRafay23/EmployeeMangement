using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                  new Employee
                  {
                      Id = 1,
                      Name = "Abdul Rafay",
                      Email = "Erorrafay@gmail.com",
                      Department = Dept.Manager
                  },
                  new Employee
                  {
                      Id = 2,
                      Name = "Sheryar Shaikh",
                      Email = "SheryarShaikh123@gmail.com",
                      Department = Dept.HR
                  }

                  );
        }
    }


    
}

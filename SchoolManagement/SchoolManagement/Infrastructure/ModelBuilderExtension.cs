using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagement.Infrastructure
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student
                {
                    Id = 1,
                    Name = "test1",
                    Major = MajorEnum.ComputerScience,
                    Email = "test1@test1.com"
                },
                new Student
                {
                    Id = 2,
                    Name = "test2",
                    Major = MajorEnum.ElectronicCommerce,
                    Email = "test2@test2.com"

                },
                new Student
                {
                    Id = 3,
                    Name = "test3",
                    Major = MajorEnum.Mathematics,
                    Email = "test3@test3.com"

                });
        }
    }
}

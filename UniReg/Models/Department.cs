using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace UniReg.Models
{
    public class Department
    {
      [Required]
      public int DepartmentId {get; set;}
      [Required]
      public string Name {get; set;}
      public List<Course> Courses {get; set;}
      public List<Student> Students {get; set;}
    }
}
  
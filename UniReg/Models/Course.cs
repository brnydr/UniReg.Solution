using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace UniReg.Models
{
  public class Course
  {
    [Required]
    public int CourseId {get; set;}
    [Required]
    public string Name {get; set;}
    public int DepartmentId {get; set;}
    public Department Department {get; set;}
    public List<CourseStudent> JoinEntities {get; set;}
  }
}
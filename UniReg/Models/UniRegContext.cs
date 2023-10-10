using Microsoft.EntityFrameworkCore;

namespace UniReg.Models
{
  public class UniRegContext : DbContext
  {
    public DbSet<Course> Courses {get; set;}
    public DbSet<Department> Departments {get; set;}
    public DbSet<Student> Students {get; set;}
    public DbSet<CourseStudent> CourseStudents {get; set;}
    public UniRegContext(DbContextOptions options) : base(options) { }  
  }
}
namespace UniReg.Models
{
  public class CourseStudent
  {
    public int CourseStudentId {get; set;}
    public int CourseId {get; set;}
    public int StudentId {get; set;}
    public Course Course {get; set;}
    public Student Student {get; set;}
  }
}
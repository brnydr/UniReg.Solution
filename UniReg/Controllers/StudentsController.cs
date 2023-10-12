using Microsoft.AspNetCore.Mvc;
using UniReg.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniReg.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniRegContext _db;
    
    public StudentsController(UniRegContext db)
    {
      _db = db;
    }
  

    public ActionResult Index()
    {
      List<Student> model = _db.Students.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student)
    {
      _db.Students.Add(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
        .Include(student => student.Department)
        .Include(student => student.JoinEntities)
        .ThenInclude(join => join.Course)
        .FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    public ActionResult Edit(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Students.Update(student);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCourse(int id)
    {
      Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult AddCourse(Student student, int courseId)
    {
      #nullable enable
      CourseStudent? joinEntity = _db.CourseStudents.FirstOrDefault(join => join.StudentId == student.StudentId && join.CourseId == courseId);
      #nullable disable
      if (joinEntity == null && courseId != 0)
      {
        _db.CourseStudents.Add(new CourseStudent() { CourseId = courseId, StudentId = student.StudentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = student.StudentId });
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      CourseStudent joinEntry = _db.CourseStudents.FirstOrDefault(entry => entry.CourseStudentId == joinId);
      int studentId = joinEntry.StudentId;
      _db.CourseStudents.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = studentId } );
    }
  }
}
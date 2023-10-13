using Microsoft.AspNetCore.Mvc;
using UniReg.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace UniReg.Controllers
{
  public class CoursesController : Controller
  {
    private readonly UniRegContext _db;

    public CoursesController(UniRegContext db)
    {
      _db = db;
    } 

    public ActionResult Index()
    {
      List<Course> model = _db.Courses.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Course thisCourse = _db.Courses
        .Include(course => course.Department)
        .Include(course => course.JoinEntities)
        .ThenInclude(join => join.Student)
        .FirstOrDefault(course => course.CourseId == id);
      return View(thisCourse);
    }

    public ActionResult Edit(int id)
    {

      Course thisCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
      string defaultValue = thisCourse.DepartmentId.ToString();
      ViewBag.DepartmentId = new SelectList(_db.Departments, "DepartmentId", "Name", defaultValue);
      return View(thisCourse);
    }
    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Courses.Update(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
  }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniReg.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace UniReg.Controllers
{
    public class DepartmentsController : Controller
    {
      
      private readonly UniRegContext _db;
      
      public DepartmentsController(UniRegContext db)
      {
        _db = db;
      }

      
      public ActionResult Index()
      {
        List<Department> model = _db.Departments.ToList();
        return View(model);
      }

      public ActionResult Create()
      {
        return View();
      }

      [HttpPost]
      public ActionResult Create(Department department)
      {
        _db.Departments.Add(department);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      public ActionResult Details(int id)
      {
        Department thisDepartment =  _db.Departments
          .Include(department => department.Students)
          .Include(department => department.Courses)
          .FirstOrDefault(department => department.DepartmentId == id);
          return View(thisDepartment);
      }

      public ActionResult ViewStudents(int id)
      {
        Department thisDepartment = _db.Departments
          .Include(department => department.Students)
          .FirstOrDefault(department => department.DepartmentId == id);
          return View(thisDepartment);
      }


    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigSchool.Models;
using System.Data.Entity;
using BigSchool.ViewModels;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();    
        }
        public ActionResult Index()
        {
            var upcommingCourse = _dbContext.Courses
                .Include(a => a.Lecturer)
                .Include(a => a.Category)
                .Where(a => a.DateTime > DateTime.Now);
            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = upcommingCourse,
                ShowAction = User.Identity.IsAuthenticated
            }; 
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
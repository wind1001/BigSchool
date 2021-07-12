using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BigSchool.Models;
using Microsoft.AspNet.Identity;


namespace BigSchool.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [System.Web.Http.HttpDelete]
      
        // GET: Courses
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(a => a.Id == id && a.LecturerId == userId);
            if (course.IsCanceled)
                return NotFound();
            course.IsCanceled = true;

            var notification = new Notification()
            {
                DateTime = DateTime.Now,
                Course = course,
                Type = NotificationType.CourseCanceled
            };
        var attendees = _dbContext.Attendances.Where(a => a.CourseId == course.Id).Select(a => a.Attendee).ToList();
        foreach(var attendee in attendees)
            {
                var userNotification = new UserNotification()
                {
                    User = attendee,
                    Notification = notification
                };
        _dbContext.UserNotifications.Add(userNotification);
            }
       
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
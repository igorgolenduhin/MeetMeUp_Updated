using MeetMeUp_Updated.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetMeUp_Updated.Controllers
{
    public class MeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Meeting
        public ActionResult Meetings()
        {
            return View(db.Meetings.ToList());
        }
        //Get: Create Meeting
        public ActionResult Create()
        {
            var model = new MeetingCreateViewModel();
            model.AllGroups = getMyGroups();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Date,Time,SelectedGroup")] MeetingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllGroups = getMyGroups();
                return View(model);
            }

            Meeting meeting = new Meeting();
            meeting.Title = model.Title;
            meeting.Date = model.Date;
            meeting.Time = model.Time;
            meeting.GroupID = int.Parse(model.SelectedGroup);


            db.Meetings.Add(meeting);
            db.SaveChanges();
            return RedirectToAction("Meetings");
        }

        public ActionResult Show(int id)
        {
            //var model = MeetingList.getMeetings()[id];
            return View();
        }

        private List<SelectListItem> getMyGroups()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var allGroups = db.Groups.ToList();
            return allGroups.Select(g =>
                new SelectListItem
                {
                    Value = g.GroupID.ToString(),
                    Text = g.GroupName
                }).ToList();
        }
    }
}
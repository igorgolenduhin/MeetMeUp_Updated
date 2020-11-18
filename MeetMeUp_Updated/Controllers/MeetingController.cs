using MeetMeUp_Updated.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetMeUp_Updated.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Meetings()
        {
            List<Meeting> meetings = MeetingList.getMeetings();
            return View(meetings);
        }
        public ActionResult Create()
        {
            var model = new Meeting()
            {
                Messages = new List<string>()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Meeting model)
        {
            model.Id = MeetingList.getMeetings().Count;
            MeetingList.addMeeting(model);
            return RedirectToAction("Meetings");
        }

        public ActionResult Show(int id)
        {
            var model = MeetingList.getMeetings()[id];
            return View(model);
        }
    }
}
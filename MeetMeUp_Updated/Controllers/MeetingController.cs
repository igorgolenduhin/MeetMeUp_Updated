using MeetMeUp_Updated.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MeetMeUp_Updated.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Meeting
        public ActionResult Meetings(String query)
        {
            var allMeetings = db.Meetings.ToList();

            if (query != null)
            {
                allMeetings = allMeetings.Where(m => m.Title.ToLower().Contains(query.ToLower())).ToList();
            }

            var myMeetings = allMeetings.Where(m => getMyGroupsList().Contains(m.Group)).ToList();
            return View(myMeetings);
        }
        //Get: Create Meeting
        public ActionResult Create(int? id)
        {
            var model = new MeetingCreateViewModel();
            model.AllGroups = getMyGroupsSelectList();
            if (id != null)
            {
                model.SelectedGroup = id.ToString();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Date,Time,SelectedGroup,Place,Image")] MeetingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllGroups = getMyGroupsSelectList();
                return View(model);
            }

            Meeting meeting = new Meeting();
            meeting.Title = model.Title;
            meeting.Date = model.Date;
            meeting.Time = model.Time;
            meeting.Place = model.Place;
            meeting.GroupID = int.Parse(model.SelectedGroup);

            if (model.Image != null)
            {
                //Creating filename and path for a picture
                string pic = System.IO.Path.GetFileName(model.Image.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("/Assets/Images/MeetingPics/"), pic);

                //saving image on a server and adding path to db
                model.Image.SaveAs(path);
                meeting.Image = "/Assets/Images/MeetingPics/" + pic;
            }


            db.Meetings.Add(meeting);
            db.SaveChanges();
            return RedirectToAction("Meetings");
        }

        public ActionResult Details(int? id)
        {
            if(id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var meeting = db.Meetings.Find(id);
            if(meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        private List<SelectListItem> getMyGroupsSelectList()
        {
            return getMyGroupsList().Select(g =>
                new SelectListItem
                {
                    Value = g.GroupID.ToString(),
                    Text = g.GroupName
                }).ToList();
        }

        private List<Group> getMyGroupsList()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var allGroups = db.Groups.ToList();
            return allGroups.Where(g => g.Members.Contains(user)).ToList();
        }

        [HttpGet]
        public ActionResult ChangeImage(int? meetingID)
        {
            if (meetingID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(meetingID);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }
        [HttpPost]
        public ActionResult ChangeImage(HttpPostedFileBase fileUpload1, int meetingID)
        {
            if (fileUpload1 != null)
            {
                string pic = System.IO.Path.GetFileName(fileUpload1.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("/Assets/Images/MeetingPics/"), pic);

                fileUpload1.SaveAs(path);
                Meeting meeting = db.Meetings.Find(meetingID);
                meeting.Image = "/Assets/Images/MeetingPics/" + pic;
                db.Entry(meeting).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = meetingID });
            }
            return View();
        }
    }
}
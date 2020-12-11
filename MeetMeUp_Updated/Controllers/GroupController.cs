using MeetMeUp_Updated.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Net;
using System.Data.Entity;

namespace MeetMeUp_Updated.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Group
        public ActionResult Groups()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var allGroups = db.Groups.Include(g => g.Members).ToList();
            var groups = allGroups.Where(g => g.Members.Contains(user)).ToList();
            return View(groups);
        }
        //GET: Create
        public ActionResult Create()
        {
            var groupCreateModel = new GroupCreateViewModel();
            groupCreateModel.Members = new List<string>();
            return View(groupCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupName,GroupImage,Members")] GroupCreateViewModel groupCreateModel)
        {
            if (ModelState.IsValid)
            {
                var group = new Group()
                {
                    Members = new List<ApplicationUser>()
                };
                group.GroupName = groupCreateModel.GroupName;

                //adding current user to the group
                var user = db.Users.Find(User.Identity.GetUserId());
                //var user = UserManager.FindById(User.Identity.GetUserId());
                group.Members.Add(user);

                //adding all provided users to the group
                foreach (var email in groupCreateModel.Members)
                {
                    var member = db.Users.Where(m => m.Email == email).SingleOrDefault();
                    //var member = UserManager.FindByEmail(email);
                    if (member == null)
                    {
                        ModelState.AddModelError("InvalidUser", "Users with such email don't exist");
                        return View(groupCreateModel);
                    }
                    group.Members.Add(member);
                }

                if (groupCreateModel.GroupImage != null)
                {
                    //Creating filename and path for a picture
                    string pic = System.IO.Path.GetFileName(groupCreateModel.GroupImage.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("/Assets/Images/GroupPics/"), pic);

                    //saving image on a server and adding path to db
                    groupCreateModel.GroupImage.SaveAs(path);
                    group.GroupImage = "/Assets/Images/GroupPics/" + pic;
                }

                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Groups");
            }

            return View(groupCreateModel);
        }

                // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetMeUp_Updated.Models;
using Microsoft.AspNet.Identity;


namespace MeetMeUp_Updated.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult MyProfile()
        {
            ApplicationUser applicationUser = db.Users.Find(User.Identity.GetUserId());
            return View(applicationUser);
        }

        [HttpGet]
        public ActionResult ChangeImage()
        {
            return View(db.Users.Find(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult ChangeImage(HttpPostedFileBase fileUpload1)
        {
            if(fileUpload1!= null)
            {
                string pic = System.IO.Path.GetFileName(fileUpload1.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("/Assets/Images/UserPics/"), pic); 

                fileUpload1.SaveAs(path);
                ApplicationUser applicationUser = db.Users.Find(User.Identity.GetUserId());
                applicationUser.UserImg = "/Assets/Images/UserPics/"+pic;
                db.Entry(applicationUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("MyProfile");
        }



    }
}
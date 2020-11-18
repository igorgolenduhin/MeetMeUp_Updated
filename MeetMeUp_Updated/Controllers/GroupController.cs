using MeetMeUp_Updated.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetMeUp_Updated.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Groups()
        {
            List<Group> groups = GroupList.getGroups();
            return View(groups);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(String groupName, string[] member)
        {
            var model = new Group()
            {
                GroupName = groupName,
                Members = member
            };
            GroupList.addGroup(model);
            return RedirectToAction("Groups");
        }


    }
}
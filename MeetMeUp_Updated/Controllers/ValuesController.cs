using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MeetMeUp_Updated.Models;

namespace MeetMeUp_Updated.Controllers
{
    public class ValuesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return db.Users.ToList();
        }
    }
}

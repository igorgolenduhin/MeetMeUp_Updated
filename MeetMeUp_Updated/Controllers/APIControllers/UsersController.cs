using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MeetMeUp_Updated.Models;

namespace MeetMeUp_Updated.Controllers.APIControllers
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<ApplicationUser> Get()
        {
            return db.Users.ToList();
        }
    }
}

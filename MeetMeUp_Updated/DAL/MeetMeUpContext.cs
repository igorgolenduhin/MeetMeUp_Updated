using MeetMeUp_Updated.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetMeUp_Updated.DAL
{
    public class MeetMeUpContext : DbContext
    {
        public MeetMeUpContext() : base("DefaultConnection")
        { }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetMeUp_Updated.Models
{
    public class GroupCreateViewModel
    {
        public string GroupName { get; set; }
        public string GroupImage { get; set; }
        public ICollection<ApplicationUser> Members { get; set; }

    }
}
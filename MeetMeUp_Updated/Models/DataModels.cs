using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeetMeUp_Updated.Models
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleID { get; set; }
        public string ApplicationUserID { get; set; }
        public string Occupation { get; set; }
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        public string GroupImage { get; set; } = "/Assets/Images/GroupPics/anonymous_group.png";
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
    }

    public class Meeting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeetingID { get; set; }
        public int GroupID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Image { get; set; } = "/Assets/Images/MeetingPics/anonymous_meeting.svg";
        public string Place { get; set; }
        public virtual Group Group { get; set; }
    }




}
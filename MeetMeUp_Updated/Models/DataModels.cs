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

        public string GroupImage { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
    }

    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public List<string> Messages { get; set; }
    }

    public class GroupList
    {
        static List<Group> groups = new List<Group>();

        static public void addGroup(Group group)
        {
            groups.Add(group);
        }
        static public List<Group> getGroups()
        {
            return groups;
        }
    }

    public class MeetingList
    {
        static List<Meeting> meetings = new List<Meeting>();

        static public void addMeeting(Meeting meeting)
        {
            meetings.Add(meeting);
        }
        static public List<Meeting> getMeetings()
        {
            meetings.Append(new Meeting()
            {
                Id = 0,
                Date = DateTime.Now,
                Time = DateTime.Now,
                Title = "kek",
                Messages = new List<string>()
            });
            return meetings;
        }
    }



}
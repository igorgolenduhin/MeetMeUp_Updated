using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetMeUp_Updated.Models
{
    public class GroupCreateViewModel
    {
        [Required(ErrorMessage = "Please provide group name")]
        public string GroupName { get; set; }

        public HttpPostedFileBase GroupImage { get; set; }

        public List<string> Members { get; set; }


    }

    public class MeetingCreateViewModel
    {
        [Required(ErrorMessage = "Please provide meeting title")]
        [Display(Name = "Meeting Title:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public HttpPostedFileBase Image { get; set; }


        [Required(ErrorMessage = "Please select one of your groups")]
        public string SelectedGroup { get; set; }
        public IEnumerable<SelectListItem> AllGroups { get; set; }

        [Required(ErrorMessage = "Please provide a place for a meeting")]
        public string Place { get; set; }

    }
}
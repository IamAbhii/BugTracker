using Bug_Tracker_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker_project.ViewModels
{
    public class AdminViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}
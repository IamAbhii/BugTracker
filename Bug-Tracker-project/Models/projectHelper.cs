using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker_project.Models
{
    public class ProjectHelper
    {
        ApplicationDbContext db;
        public ProjectHelper(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Project> GetAllProject()
        {
            var projects = db.Projects.ToList();
            return projects;
        }

       
        public Project GetProject(int projectId)
        {
            var project = db.Projects.Find(projectId);
            return project;
        }

        public List<Project> GetUserProjects(string userId)
        {
            var projects = db.Users.Find(userId).Projects.ToList();
            return projects;
        }

        
        public void AddProject(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            if (project != null)
            {
                var projectInDb = GetProject(project.Id);

                projectInDb.Name = project.Name;

                List<ApplicationUser> selectedUsers = project.Users.ToList();

                //Get all users to remove in removed list
                List<ApplicationUser> removedUsers = new List<ApplicationUser>();

                foreach (ApplicationUser user in projectInDb.Users)
                {
                    if (!selectedUsers.Contains(user))
                    {
                        removedUsers.Add(user);
                    }
                }

                //Remove it from project
                foreach (var user in removedUsers)
                {
                    projectInDb.Users.Remove(user);
                    db.SaveChanges();
                }

                //add all new user selected from view model
                foreach (var user in selectedUsers)
                {
                    if (!projectInDb.Users.Contains(user))
                    {
                        projectInDb.Users.Add(user);
                    }
                }
                db.SaveChanges();
            }
        }
       
        public string[] GetProjectUserIds(int projectId)
        {
            var project = GetProject(projectId);
            string[] userIds = project.Users.Select(user => user.Id).ToArray();

            return userIds;
        }
    }
}
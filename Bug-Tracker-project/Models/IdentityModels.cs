using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bug_Tracker_project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [InverseProperty("AssignedToUser")]
        public virtual ICollection<Ticket> AssignedTickets { get; set; }
        [InverseProperty("OwnersUser")]
        public virtual ICollection<Ticket> CreatedTickets { get; set; }
        public virtual ICollection<TicketAttachments> TicketAttachments { get; set; }
        public virtual ICollection<TicketComments> TicketComments { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }
        public virtual ICollection<TicketNotificatioin> TicketNotificatioins { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser > manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Ticket
    {
        public int Id { get; set; }        
        public string Title { get; set; }       
        public string Description { get; set; }         
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }        
        public int ProjectId { get; set; }
        public Project Project { get; set; }              
        public TicketType TicketType { get; set; }           
        public TicketPriorities TicketPriorities { get; set; }        
        public TicketStatuses TicketStatus { get; set; }         
        public string OwnerUserId { get; set; }
        [ForeignKey("OwnerUserId")]
        public ApplicationUser  OwnersUser { get; set; }         
        public string AssignedToUserId { get; set; }
        [ForeignKey("AssignedToUserId")]
        public ApplicationUser  AssignedToUser { get; set; }
        public virtual ICollection<TicketAttachments> TicketAttachments { get; set; }
        public virtual ICollection<TicketComments> TicketComments { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }
        public virtual ICollection<TicketNotificatioin> TicketNotificatioins { get; set; }
    }

    public enum TicketStatuses
    {
        Completed,
        Incompleted,
        WorkInProgress,
    }
    public enum TicketPriorities
    {
        High,
        Medium,
        Low,
    }
    public enum TicketType
    {        
        Technical,
        InsufficientResource,
    }
    public class TicketAttachments
    {
        public int Id { get; set; }        
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }         
        public string FilePath { get; set; }         
        public string Description { get; set; }         
        public DateTime Created { get; set; }        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }         
        public string FileUrl { get; set; }
    }
    public class TicketComments
    {
        public int Id { get; set; }        
        public string Comment { get; set; }        
        public DateTime Created { get; set; }        
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }         
        public string UserId { get; set; }
        public ApplicationUser  User  { get; set; }
    }
    public class TicketHistories
    {
        public int Id { get; set; }        
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }        
        public string Property { get; set; }         
        public string OldValue { get; set; }         
        public string NewValue { get; set; }         
        public DateTime Changed { get; set; }         
        public string UserId { get; set; }
        public ApplicationUser  User  { get; set; }
    }
    public class TicketNotificatioin
    {
        public int Id { get; set; }        
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }        
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
    public class Project
    {
        public int Id { get; set; }         
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser > Users { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser >
    {
        //relations
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachments> TicketAttachments { get; set; }
        public DbSet<TicketComments> TicketComments { get; set; }
        public DbSet<TicketHistories> TicketHistories { get; set; }
        public DbSet<TicketNotificatioin> TicketNotificatioins { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Ticket> CreatedTickets { get; set; }
        public ICollection<Ticket> AssignedTickets { get; set; }
        public ICollection<TicketWatch> WatchedTickets { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<OrganizationUser> Organizations { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<TeamUser> Teams { get; set; }
        public User()
        {
            Created = DateTime.UtcNow;
        }
    }
}

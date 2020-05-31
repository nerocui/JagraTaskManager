using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JagraTaskManager.Server.Models
{
    public class Ticket
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User Creator { get; set; }
        public User Assignee { get; set; }
        public ICollection<TicketWatch> Watchers { get; set; }
        public string CreatorId { get; set; }
        public string AssigneeId { get; set; }
        public Organization Organization { get; set; }
        public string OrganizationId { get; set; }
        public Team Team { get; set; }
        public string TeamId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<TicketDependency> Dependers { get; set; }
        public ICollection<TicketDependency> Dependees { get; set; }
        public ICollection<TicketTag> Tags { get; set; }
        public TicketStatus Status { get; set; }
        public string StatusId { get; set; }

        public Ticket()
        {
            Created = DateTime.UtcNow;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class Organization
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<OrganizationUser> Users { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Status> Statuses { get; set; }
        public DateTime Created { get; set; }
        public Organization()
        {
            Created = DateTime.UtcNow;
        }
    }
}

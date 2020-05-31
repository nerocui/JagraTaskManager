using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class Team
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public string OrganizationId { get; set; }
        public ICollection<TeamUser> Users { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public User Leader { get; set; }
        public string LeaderId { get; set; }
        public DateTime Created { get; set; }
        public Team()
        {
            Created = DateTime.UtcNow;
        }
    }
}

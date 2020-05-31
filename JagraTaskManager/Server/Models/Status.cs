using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JagraTaskManager.Server.Models
{
    public class Status
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<TicketStatus> Tickets { get; set; }
    }
}
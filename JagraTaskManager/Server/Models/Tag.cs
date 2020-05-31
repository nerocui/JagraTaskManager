using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JagraTaskManager.Server.Models
{
    public class Tag
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<TicketTag> Tickets { get; set; }
        public string OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
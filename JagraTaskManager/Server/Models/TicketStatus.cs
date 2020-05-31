using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JagraTaskManager.Server.Models
{
    public class TicketStatus
    {
        public string TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string StatusId { get; set; }
        public Status Status { get; set; }
    }
}
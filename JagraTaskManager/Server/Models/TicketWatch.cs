using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class TicketWatch
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public Ticket Ticket { get; set; }
        public string TicketId { get; set; }
    }
}

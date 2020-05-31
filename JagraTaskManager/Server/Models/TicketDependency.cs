using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class TicketDependency
    {
        public Ticket Depender { get; set; }
        public string DependerId { get; set; }
        public Ticket Dependee { get; set; }
        public string DependeeId { get; set; }
    }
}

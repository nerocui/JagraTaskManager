using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class Invitation
    {
        public Organization Organization { get; set; }
        public string OrganizationId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public Invitation()
        {
            Created = DateTime.UtcNow;
        }
    }
}

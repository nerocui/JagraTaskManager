using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class OrganizationUser
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public Organization Organization { get; set; }
        public string OrganizationId { get; set; }
    }
}

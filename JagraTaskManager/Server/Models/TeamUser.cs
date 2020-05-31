using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class TeamUser
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public Team Team { get; set; }
        public string TeamId { get; set; }
        public string Role { get; set; }
    }
}

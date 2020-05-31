using System;
using System.Collections.Generic;

namespace JagraTaskManager.Shared.Dto
{
    public class TeamForListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserForListDto> Users { get; set; }
        public ICollection<TicketForListDto> Tickets { get; set; }
        public UserForListDto Leader { get; set; }
        public DateTime Created { get; set; }
    }
}
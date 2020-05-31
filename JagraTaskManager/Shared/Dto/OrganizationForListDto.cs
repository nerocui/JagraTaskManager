using System;
using System.Collections.Generic;
using System.Text;

namespace JagraTaskManager.Shared.Dto
{
    public class OrganizationForListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<UserForListDto> Users { get; set; }
        public ICollection<TicketForListDto> Tickets { get; set; }
        public ICollection<InvitationForListDto> Invitations { get; set; }
    }
}

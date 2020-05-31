using System;
using System.Collections.Generic;
using System.Text;

namespace JagraTaskManager.Shared.Dto
{
    public class TicketForListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserForListDto Creator { get; set; }
        public UserForListDto Assignee { get; set; }
        public ICollection<UserForListDto> Watchers { get; set; }
        public string CreatorId { get; set; }
        public string AssigneeId { get; set; }
        public OrganizationForListDto Organization { get; set; }
        public string OrganizationId { get; set; }
        public TeamForListDto Team { get; set; }
        public string TeamId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public ICollection<TicketForListDto> Dependers { get; set; }
        public ICollection<TicketForListDto> Dependees { get; set; }
        public ICollection<TicketTagForListDto> Tags { get; set; }
        public TicketStatusForListDto Status { get; set; }
        public string StatusId { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

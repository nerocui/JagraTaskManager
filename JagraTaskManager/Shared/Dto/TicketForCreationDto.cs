using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JagraTaskManager.Shared.Dto
{
    public class TicketForCreationDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string CreatorId { get; set; }

        [Required]
        public string AssigneeId { get; set; }

        public string OrganizationId { get; set; }

        public string TeamId { get; set; }
    }
}

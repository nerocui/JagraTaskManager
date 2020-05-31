using System;
using System.Collections.Generic;
using System.Text;

namespace JagraTaskManager.Shared.Dto
{
    public class InvitationForListDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public OrganizationForInvitationDto Organization;
        override public string ToString()
        {
            return $"User: {Email}, Organization: {Organization.Name}";
        }
    }
}

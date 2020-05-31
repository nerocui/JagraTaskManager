using System;
using System.Collections.Generic;
using System.Text;

namespace JagraTaskManager.Shared.Dto
{
    public class UserForListDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}

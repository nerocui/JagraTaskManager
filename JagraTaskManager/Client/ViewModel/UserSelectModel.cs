using System.Collections.Generic;
using JagraTaskManager.Shared.Dto;

namespace JagraTaskManager.Client.ViewModel
{
    public class UserSelectModel
    {
        public UserForListDto User { get; set; }
        public ICollection<UserForListDto> Users { get; set; }
    }
}
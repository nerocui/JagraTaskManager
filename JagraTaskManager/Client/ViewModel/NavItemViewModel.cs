using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Client.ViewModel
{
    public class NavItemViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public List<NavItemViewModel> Children { get; set; }

        public NavItemViewModel()
        {
            Children = new List<NavItemViewModel>();
        }
    }
}

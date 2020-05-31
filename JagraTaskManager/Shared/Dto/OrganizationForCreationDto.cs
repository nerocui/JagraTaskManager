using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JagraTaskManager.Shared.Dto
{
    public class OrganizationForCreationDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
    }
}

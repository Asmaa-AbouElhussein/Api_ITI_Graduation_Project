using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.DTOs
{
    public class EmailSubject
    {   
        [Required]
        public string Email { get; init; }
        [Required]
        public string body { get; init; }

        [Required]
        public string name { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.DTOs
{
    public class UserLogin
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string username { set; get; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string password { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.DTOs
{
    public class DeletepurchasedcodeDTO
    {
        [Required]
        public string email { get; set; }
        
        [Required]
        public int crsid { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.DTOs
{
    public class Usercomment
    {
        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(150)]
        public string comment { get; init; }

        [Required]
        [DataType(DataType.Date)]
        public string date { get; init; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string userName { get; init; }
        [Required]
        public string avatarpath { get; init; }
       
    }
}

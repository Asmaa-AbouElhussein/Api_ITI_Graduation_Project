using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace courses_site_api.models
{
    public class Courses_category
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        
        public int Course_Detailesid { get; set; }
      
        public virtual Course_detailes Course_Detailes { get; set; } 
       
        public virtual ICollection<Courses_videos> Courses_Videos { get; set; }

    }
}

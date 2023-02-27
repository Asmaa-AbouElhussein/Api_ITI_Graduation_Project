using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace courses_site_api.models
{
    public class Courses_videos
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public string videopath { get; set; }

        public int Courses_Categoryid { get; set; }
        
        public virtual Courses_category Courses_Category { get; set; }

    }
}

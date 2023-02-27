using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace courses_site_api.models
{
    public class Course_detailes
    {
        [Key]
        public int id { set; get; }
        [Required]
        public string name { set; get; }
        [Required]
        public string imgpath { set; get; }
        [Required]
        public int price { set; get; }  
        public int discount { set; get; }
        [Required]
        public string description { set; get; }
        [Required]
        public int numberofvideos { set; get; }
        [Required]
        public int numberofhours { set; get; }
        [Required]
        public string date { set; get; }
     
         public virtual ICollection<Courses_category> Courses_Categories { get; set; }

    }
}

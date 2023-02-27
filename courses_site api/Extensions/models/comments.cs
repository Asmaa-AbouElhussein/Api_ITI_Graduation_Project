using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace courses_site_api.models
{
    public class comments
    {
        public int id { get; set; }
       
        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(150)]
        public string comment { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public string date { set; get; }
        [ForeignKey("registration")]
        public int registrationid { get; set; }
      
        public virtual registration registration { get; set; }  

    }
}

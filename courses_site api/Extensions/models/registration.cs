using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace courses_site_api.models
{
    public class registration
    {
        public int id { set; get; }

        [Required]
        [StringLength(20,MinimumLength =3)]
        public string username { set; get; }
       
        [Required]
        [StringLength( int.MaxValue,MinimumLength = 5)]
        public string password { set; get; }
       
        [Required]
        public string gender { set; get; }

     
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { set; get; }
        public string avatarpath { set; get; }
       
        public virtual ICollection<comments> comments { set; get; }
        
        public virtual ICollection<chat> chats { set; get; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.models
{
    public class chat
    {
        [Key]
        public int id { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string message { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
        
        [ForeignKey("Registration")]
        public int Registrationid { get; set; }
        public virtual registration Registration { get; set; }


    }
}

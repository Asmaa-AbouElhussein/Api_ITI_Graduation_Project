using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.DTOs
{
    public class chatDTO
    {
        public  string  Sender{ get; set; }

        public string Receiver { get; set; }

        public string message { get; set; }


    }
}

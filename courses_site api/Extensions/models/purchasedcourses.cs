using System.ComponentModel.DataAnnotations;

namespace courses_site_api.models
{
    public class purchasedcourses
    {
        public int id { set; get; }
        [Required]
        [EmailAddress]
        public string email { set; get; }
        [Required]
        public string code { set; get; }
        [Required]
        public int courseid { set; get; }

    }
}

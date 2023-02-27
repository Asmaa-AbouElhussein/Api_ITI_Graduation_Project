using System.ComponentModel.DataAnnotations;

namespace courses_site_api.DTOs
{
    public class newpasswordDTO
    {
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
    }
}

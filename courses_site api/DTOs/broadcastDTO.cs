using System.ComponentModel.DataAnnotations;

namespace courses_site_api.DTOs
{
    public class broadcastDTO
    {
      
        [Required]
        public string subject { get; init; }

        [Required]
        public string body { get; init; }
    }
}

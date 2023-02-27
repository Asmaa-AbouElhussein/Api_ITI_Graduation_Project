using System.ComponentModel.DataAnnotations;

namespace courses_site_api.DTOs
{
    public class emaildataDTO
    {
        [Required]
        public string mailto { get; init; }
        [Required]
        public string subject { get; init; }

        [Required]
        public string code { get; init; }
    }
}

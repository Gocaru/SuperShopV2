using System.ComponentModel.DataAnnotations;

namespace SuperShopV2.Models
{
    public class CityViewModel
    {
        public int CountryId { get; set; }

        public string CityId { get; set; }

        [Required]
        [Display(Name ="City")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contein {1} characters")]
        public string Name { get; set; }
    }
}

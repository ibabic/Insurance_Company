using System.ComponentModel.DataAnnotations;

namespace Partners.DataAccess.Models.DTOs
{
    public class PartnerSaveDto
    {

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "PartnerNumber must contain exactly 20 digits.")]
        public string PartnerNumber { get; set; }

        [StringLength(11)]
        public string CroatianPIN { get; set; }

        [Required]
        public PartnerType PartnerTypeId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string CreateByUser { get; set; }

        [Required]
        public bool IsForeign { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10)]
        public string ExternalCode { get; set; }

        [Required]
        [RegularExpression("^[MFN]$", ErrorMessage = "Gender must be either 'M', 'F', or 'N'.")]
        public char Gender { get; set; }
    }
}
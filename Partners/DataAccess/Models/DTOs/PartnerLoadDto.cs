using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Partners.DataAccess.Models
{
    public class PartnerLoadDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "PartnerNumber must contain exactly 20 digits.")]
        [DisplayName("Partner Number")]
        public string PartnerNumber { get; set; }

        [StringLength(11)]
        [DisplayName("Croatian PIN")]
        public string CroatianPIN { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "PartnerTypeId must be either 1 or 2.")]
        [DisplayName("Partner Type")]
        public PartnerType PartnerTypeId { get; set; }

        [DisplayName("Created At")]
        public DateTime? CreatedAtUtc { get; set; }

        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email format.")]
        [DisplayName("Created By User")]
        public string CreateByUser { get; set; }

        [Required]
        [DisplayName("Is Foreign")]
        public bool IsForeign { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10)]
        [DisplayName("External Code")]
        public string ExternalCode { get; set; }

        [Required]
        [RegularExpression("^[MFN]$", ErrorMessage = "Gender must be either 'M', 'F', or 'N'.")]
        public char Gender { get; set; }

        public int NumberOfPolicies { get; set; }
        public decimal PoliciesTotalAmount { get; set; }

        public PartnerLoadDto()
        {
            PoliciesTotalAmount = 0;
        }
    }
}
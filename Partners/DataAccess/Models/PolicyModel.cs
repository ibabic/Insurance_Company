using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Partners.DataAccess.Models
{
    public class PolicyModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10)]
        [DisplayName("Policy Number")]
        public string PolicyNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [DisplayName("Policy Amount")]
        public decimal PolicyAmount { get; set; }

        [Required]
        public int PartnerId { get; set; }
    }
}
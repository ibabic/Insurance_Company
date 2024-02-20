using System.ComponentModel.DataAnnotations;


namespace Partners.DataAccess.Models.DTOs
{
    public class PolicySaveDto
    {
        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string PolicyNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PolicyAmount { get; set; }

        [Required]
        public int PartnerId { get; set; }
    }
}
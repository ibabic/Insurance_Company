using Partners.DataAccess.Models;
using System.Linq;

namespace Partners.ViewModel
{
    public class PartnerViewModel
    {
        public IOrderedEnumerable<PartnerLoadDto> partners { get; set; }
        public bool postCreated { get; set; } = false;
    }
}
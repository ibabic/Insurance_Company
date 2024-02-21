using Partners.DataAccess.Models;
using Partners.DataAccess.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Partners.DataAccess.Data
{
    public interface IPartnerRepository
    {
        Task SoftDelete(int id);
        Task<PartnerModel> Get(int id);
        Task<IEnumerable<PartnerModel>> GetAll();
        Task Insert(PartnerSaveDto partner);
        Task Update(PartnerUpdateDto partner);
        IEnumerable<PartnerLoadDto> GetPartnersWithPolicies();
    }
}
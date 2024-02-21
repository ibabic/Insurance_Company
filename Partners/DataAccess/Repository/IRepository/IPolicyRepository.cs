using Partners.DataAccess.Models;
using Partners.DataAccess.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Partners.DataAccess.Data
{
    public interface IPolicyRepository
    {
        Task SoftDelete(int id);
        Task<PolicyModel> Get(int id);
        Task<IEnumerable<PolicyModel>> GetByPartnerId(int id);
        Task<IEnumerable<PolicyModel>> GetAll();
        Task Insert(PolicySaveDto partner);
        Task Update(PolicyUpdateDto partner);
    }
}
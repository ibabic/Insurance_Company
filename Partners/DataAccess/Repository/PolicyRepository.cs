using Partners.DataAccess.DbAccess;
using Partners.DataAccess.Models;
using Partners.DataAccess.Models.DTOs;
using Partners.DataAccess.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.DataAccess.Data
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ISqlDataAccess _context;

        public PolicyRepository(ISqlDataAccess context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolicyModel>> GetAll()
        {
            return await _context.LoadData<PolicyModel, dynamic>(StoredProcedures.Policy_GetAll, new { });
        }

        public async Task<PolicyModel> Get(int id)
        {
            var results = await _context.LoadData<PolicyModel, dynamic>(StoredProcedures.Policy_Get,
                new { Id = id });

            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<PolicyModel>> GetByPartnerId(int id)
        {
            var results = await _context.LoadData<PolicyModel, dynamic>(StoredProcedures.Policy_GetByPartnerId,
                new { partnerId = id });

            return results;
        }

        public async Task Insert(PolicySaveDto policy)
        {
            await _context.SaveData(StoredProcedures.Policy_Insert, policy);
        }

        public async Task Update(PolicyUpdateDto policy)
        {
            await _context.SaveData(StoredProcedures.Policy_Update, policy);
        }

        public async Task SoftDelete(int id)
        {
            await _context.SaveData(StoredProcedures.Policy_SoftDelete, new { Id = id });
        }
    }
}
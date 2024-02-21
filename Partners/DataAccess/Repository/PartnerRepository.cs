using Partners.DataAccess.DbAccess;
using Partners.DataAccess.Utility;
using Partners.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Partners.DataAccess.Models.DTOs;

namespace Partners.DataAccess.Data
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly ISqlDataAccess _context;
        public PartnerRepository(ISqlDataAccess context)
        {
            _context = context;
        }

        public Task<IEnumerable<PartnerModel>> GetAll()
        {
            return _context.LoadData<PartnerModel, dynamic>(StoredProcedures.Partner_GetAll, new { });
        }

        public async Task<PartnerModel> Get(int id)
        {
            var results = await _context.LoadData<PartnerModel, dynamic>(StoredProcedures.Partner_Get,
                new { Id = id });

            return results.FirstOrDefault();
        }

        public async Task Insert(PartnerSaveDto partner)
        {
            await _context.SaveData(StoredProcedures.Partner_Insert, partner);
        }

        public async Task Update(PartnerUpdateDto partner)
        {
            await _context.SaveData(StoredProcedures.Partner_Update, partner);
        }

        public async Task SoftDelete(int id)
        {
            await _context.SaveData(StoredProcedures.Partner_SoftDelete, new { Id = id });
        }

        public IEnumerable<PartnerLoadDto> GetPartnersWithPolicies()
        {
            try
            {
                _context.StartTransaction();
                var partners = _context.LoadDataInTransaction<PartnerModel, dynamic>(StoredProcedures.Partner_GetAll, new { });
                var policies = _context.LoadDataInTransaction<PolicyModel, dynamic>(StoredProcedures.Policy_GetAll, new { });
                _context.CommitTransaction();
                List<PartnerLoadDto> partnerLoadDtos = new List<PartnerLoadDto>();
                foreach (var partner in partners)
                {
                    var partnerPolicies = policies.Where(p => p.PartnerId == partner.Id).ToList();
                    int numberOfPolicies = partnerPolicies.Count;
                    decimal policiesTotalAmount = partnerPolicies.Sum(p => p.PolicyAmount);

                    PartnerLoadDto partnerLoadDto = new PartnerLoadDto
                    {
                        Id = partner.Id,
                        FirstName = partner.FirstName,
                        LastName = partner.LastName,
                        Address = partner.Address,
                        PartnerNumber = partner.PartnerNumber,
                        CroatianPIN = partner.CroatianPIN,
                        PartnerTypeId = partner.PartnerTypeId,
                        CreatedAtUtc = partner.CreatedAtUtc,
                        CreateByUser = partner.CreateByUser,
                        IsForeign = partner.IsForeign,
                        ExternalCode = partner.ExternalCode,
                        Gender = partner.Gender,
                        NumberOfPolicies = numberOfPolicies,
                        PoliciesTotalAmount = policiesTotalAmount
                    };
                    partnerLoadDtos.Add(partnerLoadDto);
                }
                return partnerLoadDtos;
            }
            catch
            {
                _context.RollbackTransaction();
                throw;
            }
            finally
            {
                _context.Dispose();
            }
        } 
    }
}
﻿using Partners.DataAccess.DbAccess;
using Partners.DataAccess.Utility;
using Partners.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Partners.DataAccess.Models.DTOs;
using System.EnterpriseServices;
using Serilog;

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

        public IEnumerable<PartnerModel> TransactionInsertOneAndGetAll(PartnerSaveDto partner)
        {
            try
            {
                _context.StartTransaction();
                _context.SaveDataInTransaction(StoredProcedures.Partner_Insert, partner);
                var partners = _context.LoadDataInTransaction<PartnerModel, dynamic>(StoredProcedures.Partner_GetAll, new { });
                _context.CommitTransaction();
                return partners;
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
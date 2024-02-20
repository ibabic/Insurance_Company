using System.Collections.Generic;
using System.Threading.Tasks;

namespace Partners.DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);
        void StartTransaction();
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        IEnumerable<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void CommitTransaction();
        void RollbackTransaction();
        void Dispose();
    }
}
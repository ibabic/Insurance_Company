using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Configuration;
using Partners.DataAccess.Utility.ExceptionHandling;
using Serilog;

namespace Partners.DataAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess, IDisposable
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure,
            U parameters)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException("Missing connection string");

            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    Log.Information("Retrieving data from db: {StoredProcedure}", storedProcedure);

                    return connection.Query<T>(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                throw new DataAccessException("An error occurred while accessing the database. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                throw;
            }
        }

        public async Task SaveData<T>(
            string storedProcedure,
            T parameters)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("Missing connection string");

            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    Log.Information("Saving data to database: {StoredProcedure}", storedProcedure);

                       connection.Execute(storedProcedure, parameters,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new UniqueKeyViolationException("The External Code you entered already exists in the system. Please choose a different External Code", ex);
                }
                else
                {
                    throw new DataAccessException("An error occurred while accessing the database. Please try again later.", ex);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                throw new Exception("Error saving data to the database.", ex);
            }
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public void StartTransaction()
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Log.Information("Transaction started");
        }

        public void SaveDataInTransaction<T>(
            string storedProcedure,
            T parameters)
        {          
            try
            {
                Log.Information("Saving data to database via transaction: {StoredProcedure}", storedProcedure);

                  _connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);
                
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new UniqueKeyViolationException("The data you are trying to save is a unique key that already exists", ex);
                }
                else
                {
                    throw new DataAccessException("An error occurred while accessing the database. Please try again later.", ex);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                throw new Exception("Error saving data to the database.", ex);
            }
        }

        public IEnumerable<T> LoadDataInTransaction<T, U>(
            string storedProcedure,
            U parameters)
        {
            try
            {
                Log.Information("Retrieving data from db via transaction: {StoredProcedure}", storedProcedure);

                return _connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);
                
            }
            catch (SqlException ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                throw new DataAccessException("An error occurred while accessing the database. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error executing stored procedure: {StoredProcedure}", storedProcedure);
                throw;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                _transaction?.Commit();
                Log.Information("Transaction committed");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error committing transaction");
                throw;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
                Log.Information("Transaction rolled back");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error rolling back transaction");
                throw;
            }
            finally
            {
                _connection?.Close();
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            Log.Information("Object disposed");
            _transaction = null;
            _connection = null;
        }
    }
}
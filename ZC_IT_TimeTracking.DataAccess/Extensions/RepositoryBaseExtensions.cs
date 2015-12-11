using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.DataAccess.Library.DBHelper;
using ZC_IT_TimeTracking.DataAccess.Library.MapperUtility;
using ZC_IT_TimeTracking.DataAccess.Library.Repository;

namespace ZC_IT_TimeTracking.DataAccess.Extensions
{
    public static class RepositoryBaseExtensions
    {
        public static T GetEntity<T>(this RepositoryBase<T> repository, T entity, string spName)
        {
            try
            {
                IDbCommand command = new SqlCommand().GetCommandWithParameters(entity, spName);
                SqlConnection conn = DBConnectionHelper.OpenNewSqlConnection(repository.ConnectionString);
                command.Connection = conn;
                entity = EntityMapper.MapSingle<T>(command.ExecuteReader());
                DBConnectionHelper.CloseSqlConnection(conn);
                return entity;
            }
            catch(SqlException ex)
            {
                if (ex.Message.ToLower().Contains("because the database is read-only"))
                {
                    repository.ValidationErrors.Add("APPLICATION_ARCHIVE_MODE_WARNING_MESSAGE", "Changes to data are not allowed within the archiving system.");
                    throw; //new DataBaseAccessException("DATABASE_IS_IN_READ_ONLY_MODE", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        // Supressing this as we pass sprocname from constant at all times
        public static List<T> GetEntityCollection<T>(this RepositoryBase<T> repository, T entity, string sprocName)
        {
            try
            {
                IDbCommand command;
                if (entity != null)
                {
                    command = new SqlCommand().GetCommandWithParameters(entity, sprocName);
                }
                else
                {
                    command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sprocName;
                }
                command.CommandTimeout = IDbCommandExtensions.timeout;
                SqlConnection conn = DBConnectionHelper.OpenNewSqlConnection(repository.ConnectionString);
                command.Connection = conn;
                List<T> entities = (List<T>)EntityMapper.MapCollection<T>(command.ExecuteReader());
                DBConnectionHelper.CloseSqlConnection(conn);
                return entities;
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToLower().Contains("because the database is read-only"))
                {
                    repository.ValidationErrors.Add("APPLICATION_ARCHIVE_MODE_WARNING_MESSAGE", "Changes to data are not allowed within the archiving system.");
                    throw;
                    //throw new DataBaseAccessException("DATABASE_IS_IN_READ_ONLY_MODE", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
        }

        public static bool Delete<T>(this RepositoryBase<T> repository, T entity, string sprocName)
        {
            try
            {                
                IDbCommand command = new SqlCommand().GetCommandWithParameters(entity, sprocName);
                SqlConnection conn = DBConnectionHelper.OpenNewSqlConnection(repository.ConnectionString);
                command.Connection = conn;
                command.ExecuteNonQuery();
                DBConnectionHelper.CloseSqlConnection(conn);
                //Debug.WriteLine(String.Format("{0} took {1} seconds to finish", sprocName, sw.ElapsedMilliseconds / 1000));
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToLower().Contains("because the database is read-only"))
                {
                    repository.ValidationErrors.Add("APPLICATION_ARCHIVE_MODE_WARNING_MESSAGE", "Changes to data are not allowed within the archiving system.");
                    throw;// new DataBaseAccessException("DATABASE_IS_IN_READ_ONLY_MODE", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
        }

        public static bool InsertOrUpdate<T>(this RepositoryBase<T> repository, T entity, string sprocName)
        {
            int isSuccess;
            try
            {
                IDbCommand command = new SqlCommand().GetCommandWithParameters(entity, sprocName);
                SqlConnection conn = DBConnectionHelper.OpenNewSqlConnection(repository.ConnectionString);
                command.Connection = conn;
                isSuccess = command.ExecuteNonQuery();
                DBConnectionHelper.CloseSqlConnection(conn);
                //Debug.WriteLine(String.Format("{0} took {1} seconds to finish", sprocName, sw.ElapsedMilliseconds / 1000));
                return isSuccess > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Message.ToLower().Contains("because the database is read-only"))
                {
                    repository.ValidationErrors.Add("APPLICATION_ARCHIVE_MODE_WARNING_MESSAGE", "Changes to data are not allowed within the archiving system.");
                    throw;// new DataBaseAccessException("DATABASE_IS_IN_READ_ONLY_MODE", ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

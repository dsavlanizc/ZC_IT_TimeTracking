﻿using System;
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
                //IDbCommand command = new SqlCommand().get
                return entity;
            }
            catch(SqlException)
            {
                return entity;
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
    }
}
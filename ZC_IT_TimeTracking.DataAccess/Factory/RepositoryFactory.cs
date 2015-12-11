﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.DataAccess.Repositories.Quarters;

namespace ZC_IT_TimeTracking.DataAccess.Factory
{
    public class RepositoryFactory
    {
        private static readonly Lazy<RepositoryFactory> _Factory = new Lazy<RepositoryFactory>(InitializeRepositoryFactory);
        private string _ConnectionString;

        private static RepositoryFactory InitializeRepositoryFactory()
        {
            string connectionStringKey = ConfigurationManager.AppSettings["connectionStringKey"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter("SELECT NameValue FROM NameValues where Name = @Name", conn);
                adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 40);
                adapter.SelectCommand.Parameters["@Name"].Value = connectionStringKey;
                System.Data.DataSet set = new System.Data.DataSet();
                conn.Open();
                adapter.Fill(set);
                connectionString = set.Tables[0].Rows[0][0].ToString();
            }
            return new RepositoryFactory(connectionString);
        }

        public RepositoryFactory() { }
        
        public RepositoryFactory(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public static RepositoryFactory GetInstance()
        {
            return _Factory.Value;
        }

        public static RepositoryFactory GetInstance(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            { return _Factory.Value; }
            else
            { return new RepositoryFactory(connectionString); }
        }

        #region interfaces
        public IQuarterRepository GetQuarterRepository()
        {
            return new QuarterRepository() { ConnectionString = _ConnectionString };
        }


        #endregion
    }
}
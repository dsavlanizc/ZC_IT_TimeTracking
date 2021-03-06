﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ZC_IT_TimeTracking.DataAccess.SQLParamFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"data source=INDINA04204VW\SQLEXPRESS;initial catalog=IT-Tracking;integrated security=True";
            SqlConnection conn = new SqlConnection(connectionString);

            Dictionary<string, string> sprocList = new Dictionary<string, string>();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from sys.procedures", conn);
            DataSet set = new DataSet();
            conn.Open();
            adapter.Fill(set);
            conn.Close();
            
            List<Sproc> _sproc = new List<Sproc>();
            foreach (DataRow row in set.Tables[0].Rows)
            {
                string spName = row[0].ToString();
                conn.Open();
                SqlCommand cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cmd);
                conn.Close();

                Sproc sp = new Sproc();
                sp.Name = spName;
                foreach (SqlParameter p in cmd.Parameters)
                {
                    Param param = new Param();
                    if (p.ParameterName != "@RETURN_VALUE" && p.ParameterName != null)
                    {
                        param.Name = p.ParameterName.Substring(1);
                        param.sqlDbType = (int)p.SqlDbType;
                        param.direction = (int)p.Direction;
                        sp.Params.Add(param);
                    }
                }
                _sproc.Add(sp);
            }
            string json = JsonConvert.SerializeObject(_sproc);
            System.IO.File.WriteAllText("SP_Mapper.json", json);
        }
    }
}

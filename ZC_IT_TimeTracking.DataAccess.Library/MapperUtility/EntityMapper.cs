using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;

namespace ZC_IT_TimeTracking.DataAccess.Library.MapperUtility
{
    /// <summary>
    /// Mapper utility used to convert DataReaders to entities
    /// </summary>
    public class EntityMapper
    {
        /// <summary>
        /// Maps the reader object the given entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T MapSingle<T>(IDataReader reader)
        {
           T obj = default(T);
            while(reader.Read())
            {
                Mapper.CreateMap<IDataReader, T>();
                obj = Mapper.Map<IDataReader, T>(reader);
            }
            return obj;
        }

        /// <summary>
        /// Maps the reader object the given entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T MapSingle<T>(DataTable dataTable)
        {
            T obj = default(T);
            Mapper.CreateMap<IDataReader, T>();
            DataTableReader rdr = dataTable.CreateDataReader();
            while (rdr.Read())
            {
                obj = Mapper.Map<IDataReader, T>(rdr);
            }
            return obj;
        }

        /// <summary>
        /// Maps the reader object to the list of given entity 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<T> MapCollection<T>(IDataReader reader)
        {
            Mapper.CreateMap<IDataReader, T>();
            List<T> tList = Mapper.Map<List<T>>(reader);
            return tList;
        }

        /// <summary>
        /// Maps the reader object to the list of Integers.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<int> MapIntegerCollection(IDataReader reader)
        {
            List<int> tList = new List<int>();
            SqlDataReader sqlReader = (SqlDataReader)reader;
            if(sqlReader != null && sqlReader.HasRows)
            {
                while(sqlReader.Read())
                {
                    tList.Add(Convert.ToInt32(sqlReader[0].ToString()));
                }
            }
            return tList;
        }

        public static IEnumerable<T> MapCollection<T>(DataTable dataTable)
        {
            Mapper.CreateMap<IDataReader, T>();
            List<T> tList = Mapper.Map<List<T>>(dataTable.CreateDataReader());
            return tList;
        }

        public static TTo Map<TFrom,TTo>(TFrom fromModel)
        {
            Mapper.CreateMap(typeof(TFrom), typeof(TTo));
            return Mapper.Map<TFrom, TTo>(fromModel);
        }

        public static IList<TTo> Map<TFrom, TTo>(IList<TFrom> fromModels)
        {
            Mapper.CreateMap(typeof(TFrom), typeof(TTo));
            return Mapper.Map<IList<TFrom>, IList<TTo>>(fromModels);
        }
        #region Custom methods to handle name conflicts
        //TODO: Handle the missing parts
        public T MapCustomSingle<T>(IDataReader reader)
        {
            Mapper.CreateMap<IDataReader, T>().ConvertUsing<CustomConverter<IDataReader, T>>();
            return Mapper.Map<T>(reader);
        }

        public IEnumerable<T> MapCustomCollection<T>(IDataReader reader)
        {
            Mapper.CreateMap<IDataReader, T>().ConvertUsing<CustomConverter<IDataReader, T>>();
            List<T> tList = Mapper.Map<List<T>>(reader);
            return tList;
        }

        #endregion

    }


}

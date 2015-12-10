using System.Collections.Generic;
using System.Data;

namespace ZC_IT_TimeTracking.DataAccess.Extensions
{
    public static class IDbCommandExtensions
    {
        internal static int timeout = 240;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        // Supressing this as we pass sprocname from constant at all times
        public static IDbCommand GetCommandWithParameters<T>(this IDbCommand command, T entity, string sprocName, bool isBatchOperation = false)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sprocName;
            command.CommandTimeout = IDbCommandExtensions.timeout;
            command = SQLParamFinder.SProcParamUtility.GenerateSQLParams(command, sprocName, entity, null, isBatchOperation);
            return command;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        // Supressing this as we pass sprocname from constant at all times
        public static IDbCommand BulkGetCommandWithParameters(this IDbCommand command, string entity, string sprocName)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sprocName;
            command.CommandTimeout = IDbCommandExtensions.timeout;
            return command;
        }

        /// <summary>
        /// Uses dictionary to generate sql parameters and returns command with the parameters.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sqlParametersDictionary"></param>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        // Supressing this as we pass sprocname from constant at all times
        public static IDbCommand GetCommandWithParameters(this IDbCommand command, Dictionary<string, object> sqlParametersDictionary, string sprocName)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sprocName;
            command.CommandTimeout = IDbCommandExtensions.timeout;
            command = SQLParamFinder.SProcParamUtility.GenerateSQLParams(command, sprocName, null, sqlParametersDictionary);
            return command;
        }

        /// <summary>
        /// Uses both entity's properties and dictionary to generate sql parameters and returns command with the parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <param name="sqlParametersDictionary"></param>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        // Supressing this as we pass sprocname from constant at all times
        public static IDbCommand GetCommandWithParameters<T>(this IDbCommand command, T entity, Dictionary<string, object> sqlParametersDictionary, string sprocName)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sprocName;
            command.CommandTimeout = IDbCommandExtensions.timeout;
            command = SQLParamFinder.SProcParamUtility.GenerateSQLParams(command, sprocName, entity, sqlParametersDictionary);
            return command;
        }
    }
}

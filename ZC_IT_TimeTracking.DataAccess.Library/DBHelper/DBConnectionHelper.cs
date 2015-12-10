using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace ZC_IT_TimeTracking.DataAccess.Library.DBHelper
{
    public class DBConnectionHelper
    {
        /// <summary>
        /// Instantiate and return open SqlConnection object
        /// </summary>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static SqlConnection OpenNewSqlConnection(string strConn)
        {
            SqlConnection objSqlConn = new SqlConnection(strConn);

            if (objSqlConn.State == ConnectionState.Closed)
            {
                objSqlConn.Open();
            }

            return objSqlConn;
        }

        /// <summary>
        /// Closes an open SqlConnection
        /// </summary>
        /// <param name="objConn">SqlConnection object to close</param>
        public static void CloseSqlConnection(SqlConnection objConn)
        {
            if (objConn == null)
            {
                return;
            }
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Dispose();
            }
        }

        /// <summary>
        /// Join the ambient transaction, or create a new one if one does not exist.
        /// </summary>
        /// <returns></returns>
        public static TransactionScope InitiateTransactionScope()
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            transactionOptions.Timeout = TransactionManager.MaximumTimeout;
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }

        /// <summary>
        /// Initiates TransactionScope within another transaction
        /// REVIEW NOTE: This method should be used inside another transaction scope only
        /// BEWARE: Please do not use this unless you have some transaction agnostic updates
        /// during transactional updates
        /// Fictional Example: While updating Requisition Candidate, I want to set Requisitions last touch date value. 
        /// Even if the original req candidate update failed I still want to set last touch date, then use 
        /// this method as
        /// TransactionScope nonTransactionScope = DBConnectionHelper.InitiateInnerTransactionScope(false)
        /// </summary>
        /// <param name="isInTransaction">
        /// true - Join the ambient transaction; Recomment not to use
        /// false - non-transactional code section inside a transaction scope : Recommend only in few scenarios
        /// </param>
        /// <returns></returns>
        //public static TransactionScope InitiateInnerTransactionScope(bool isInTransaction)
        //{
        //    var transactionOptions = new TransactionOptions();
        //    TransactionScopeOption transactionScopeOption = isInTransaction ? TransactionScopeOption.Required : TransactionScopeOption.Suppress;
        //    transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        //    transactionOptions.Timeout = TransactionManager.MaximumTimeout;
        //    return new TransactionScope(transactionScopeOption, transactionOptions);
        //}
    }
}

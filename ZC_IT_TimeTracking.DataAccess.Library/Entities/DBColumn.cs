namespace ZC_IT_TimeTracking.DataAccess.Library.Entities
{
    /// <summary>
    /// Use this attribute to map properties that are not as same as database columns
    /// Example of Usage Usage: 
    ///     [DBColumn("ReqID")]  
    ///     public int RequisitionID { get; set; }
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class DBColumn : System.Attribute
    {
        public string ColumnName { get; set; }
        public DBColumn(string columnName):base()
        {
            this.ColumnName = columnName;
        }
    }
}

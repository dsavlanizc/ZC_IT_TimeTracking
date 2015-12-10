using ZC_IT_TimeTracking.DataAccess.Library.Validations;

namespace ZC_IT_TimeTracking.DataAccess.Library.Repository
{
    public interface IRepositoryBase<T>
    {
        string ConnectionString { get; set; }
        ValidationErrorList ValidationErrors { get; set; }
        ValidationErrorList ValidationWarnings { get; set; }
    }
}

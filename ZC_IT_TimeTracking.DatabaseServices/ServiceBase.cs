using ZC_IT_TimeTracking.DataAccess.Library.Validations;
namespace ZC_IT_TimeTracking.Services
{
    public class ServiceBase : IServiceBase
    {
        public ServiceBase()
        {
            ValidationErrors = new ValidationErrorList();
            ValidationWarnings = new ValidationErrorList();
        }

        public ValidationErrorList ValidationErrors { get; set; }
        public ValidationErrorList ValidationWarnings { get; set; }

        public void ClearValidationErrors()
        {
            if (ValidationErrors != null && ValidationErrors.Errors != null)
            {
                ValidationErrors.Errors.Clear();
            }
        }
    }
}

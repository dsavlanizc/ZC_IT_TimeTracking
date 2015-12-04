using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.Services.Validations;

namespace ZC_IT_TimeTracking.Services
{
    public class ServiceBase : IServiceBase
    {
        public ServiceBase()
        {
            this.ValidationErrors = new ValidationErrorList();
            this.ValidationWarnings = new ValidationErrorList();
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

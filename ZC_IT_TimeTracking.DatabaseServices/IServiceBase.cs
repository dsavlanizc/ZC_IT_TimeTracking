using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZC_IT_TimeTracking.Services.Validations;
using System.Threading.Tasks;

namespace ZC_IT_TimeTracking.Services
{
    public interface IServiceBase
    {
        ValidationErrorList ValidationErrors { get; set; }
        ValidationErrorList ValidationWarnings { get; set; }
        void ClearValidationErrors();
    }
}

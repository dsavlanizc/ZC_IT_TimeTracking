using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.DataAccess.Library.Validations;

namespace ZC_IT_TimeTracking.Services
{
    public interface IServiceBase
    {
        ValidationErrorList ValidationErrors { get; set; }
        ValidationErrorList ValidationWarnings { get; set; }
        void ClearValidationErrors();
    }
}

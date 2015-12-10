﻿namespace ZC_IT_TimeTracking.Services
{
    public class ServiceBase : IServiceBase
    {
        public ServiceBase()
        {
            ValidationErrors = new Validations.ValidationErrorList();
            ValidationWarnings = new Validations.ValidationErrorList();
        }

        public Validations.ValidationErrorList ValidationErrors { get; set; }
        public Validations.ValidationErrorList ValidationWarnings { get; set; }

        public void ClearValidationErrors()
        {
            if (ValidationErrors != null && ValidationErrors.Errors != null)
            {
                ValidationErrors.Errors.Clear();
            }
        }
    }
}

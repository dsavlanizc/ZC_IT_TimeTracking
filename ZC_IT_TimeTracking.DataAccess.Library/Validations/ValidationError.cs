using System;
using System.Collections.Generic;

namespace ZC_IT_TimeTracking.DataAccess.Library.Validations
{
    [Serializable]
    public class ValidationError
    {
        public string ErrorMessageResourceKey { get; set; }
        public string ErrorDescription { get; set; }
        public List<string> ReplacePlaceHolderValues { get; set; }
        /// <summary>
        /// Indicates Error message for Success or failure.  
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Constructor to assign values from constructor parameters
        /// </summary>
        /// <param name="key">Error's message Key in resource files</param>
        /// <param name="description">Description of the error message</param>
        /// <param name="isSuccess">Message is for success or failure</param>
        public ValidationError(string key, string description, bool isSuccess = false)
        {
            this.ErrorMessageResourceKey = key;
            this.ErrorDescription = description;
            this.IsSuccess = isSuccess;
        }

        /// <summary>
        /// Constructor to assign values from constructor parameters
        /// </summary>
        /// <param name="key">Error's message Key in resource files</param>
        /// <param name="replacePlaceHolderValues">Collection of placeholder values</param>
        /// <param name="isSuccess">Message is for success or failure</param>
        public ValidationError(string key, List<string> replacePlaceHolderValues, bool isSuccess = false)
        {
            this.ErrorMessageResourceKey = key;
            this.ReplacePlaceHolderValues = replacePlaceHolderValues;
            this.IsSuccess = isSuccess;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ValidationError()
        {
 
        }
    }
}

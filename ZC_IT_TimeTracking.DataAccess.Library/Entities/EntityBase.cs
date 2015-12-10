using System;
using ZC_IT_TimeTracking.DataAccess.Library.Validations;

namespace ZC_IT_TimeTracking.DataAccess.Library.Entities
{
    [Serializable]
    public class EntityBase
    {
        public virtual int ID { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate{get;set;}
        
        public ValidationErrorList ValidationErrors { get; set; }

        /// <summary>
        /// UserPreferredLanguageID value for to store and pass loggedin user LanguageID
        /// </summary>
        public int UserPreferredLanguageID { get; set; }

        /// <summary>
        /// UserPreferredCNLanguageID value for to store and pass loggedin user CNLanguageID
        /// </summary>
        public int UserPreferredCNLanguageID { get; set; }

        /// <summary>
        /// A generic method to check if the entity is Valid or not.
        /// Please override this method in your entities with your own business rules.
        /// Also, we can right our own Validation methods like "ValidateForCandidateSubmission" etc.,
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            ValidationErrors = new ValidationErrorList();
            return ValidationErrors.IsValid;
        }

        public bool IsValid
        {
            get
            {
                return ValidationErrors == null || (ValidationErrors != null && ValidationErrors.IsValid);
            }
        }

        /// <summary>
        /// Creates a duplicate object.
        /// Explicit conversion is required (cast in targeted object type).
        /// </summary>
        /// <returns></returns>
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

    }
}

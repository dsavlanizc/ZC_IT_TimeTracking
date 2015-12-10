using System;

namespace ZC_IT_TimeTracking.DataAccess.Library.Entities
{
    /// <summary>
    /// Base class for all tables that are using Audit Logging
    /// </summary>
    [Serializable]
    public class AuditableEntity : UpdatableEntity
    {
        /// <summary>
        /// UpdatedByLogin value for audit logging purpose
        /// </summary>
        public string UpdatedByLogin { get; set; }

        /// <summary>
        /// CreationImpersonatedByUser value 
        /// </summary>
        public string CreationImpersonatedByUser { get; set; }

        /// <summary>
        /// ImpersonatedByUser value
        /// </summary>
        public string ImpersonatedByUser { get; set; }

        /// <summary>
        /// A generic method to check if the entity is Valid or not.
        /// Please override this method in your entities with your own business rules.
        /// Also, we can right our own Validation methods like "ValidateForCandidateSubmission" etc.,
        /// Checks UpdatedByLogin is empty or not
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            base.Validate();
            if(String.IsNullOrWhiteSpace(UpdatedByLogin))
            {
                this.ValidationErrors.Add("UPDATEDBYLOGIN_IS_EMPTY", "UpdatedByLogin value is empty");
            }
            return ValidationErrors.IsValid;
        }
    }
}

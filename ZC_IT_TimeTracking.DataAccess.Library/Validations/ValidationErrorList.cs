using System;
using System.Collections.Generic;
using System.Linq;

namespace ZC_IT_TimeTracking.DataAccess.Library.Validations
{
    /// <summary>
    /// A custom List object to maintain validations for entity
    /// </summary>
    [Serializable]
    public class ValidationErrorList
    {
        public List<ValidationError> Errors { get; set; }

        /// <summary>
        /// Constructor to initiate object
        /// </summary>
        public ValidationErrorList()
        {
            Errors = new List<ValidationError>();
        }

        /// <summary>
        /// Adds an error
        /// </summary>
        /// <param name="key">Error's message Key in resource files</param>
        /// <param name="description">Error's message description; Also used as a fallback mechanism when Key doesn't have localized text</param>
        public void Add(string key, string description, bool isSuccess = false)
        {
            // Add below condition to prevent duplicate Error Key and Message in list.
            if (Errors.Where(x => x.ErrorMessageResourceKey == key).Count() <= 0)
            {
                Errors.Add(new ValidationError(key, description, isSuccess));
            }
        }

        /// <summary>
        /// Add an Error with values to replace in placeholder
        /// </summary>
        /// <param name="key">Error's message Key in resource files</param>
        /// <param name="replaceValues">Collection of placeholder values</param>
        /// <param name="isSuccess">Message is for success or failure</param>
        public void Add(string key, List<string> replaceValues, bool isSuccess = false)
        {
            Errors.Add(new ValidationError(key, replaceValues, isSuccess));
        }

        /// <summary>
        /// Get's all Error keys
        /// </summary>
        /// <returns></returns>
        public IList<string> GetAllResourceKeys()
        {
           return Errors.Select(error => error.ErrorMessageResourceKey).ToList();
        }

        public Dictionary<string, bool> GetAllResources()
        {
            Dictionary<string, bool> errorDictionary = new Dictionary<string, bool>();
            foreach(ValidationError error in Errors)
            {
                if (!errorDictionary.ContainsKey(error.ErrorMessageResourceKey))
                {
                    errorDictionary.Add(error.ErrorMessageResourceKey, error.IsSuccess);
                }
            }
            return errorDictionary; 
        }

        /// <summary>
        /// Gets Descripion for a key from the list
        /// WARNING: Use this for the keys that you have from GetAllResourceKeys method only
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">If the given key is not found in errors then throws exception</exception>
        public string GetDescriptionForMissingResourceKey(string key)
        {
            return Errors.FirstOrDefault(error => error.ErrorMessageResourceKey == key).ErrorDescription;
        }

        public void AddMany(ValidationErrorList errorsList)
        {
           Errors.AddRange(errorsList.Errors);
        }

        /// <summary>
        /// Returns if the errorlist is valid or not
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Errors == null || Errors.Count == 0 || !Errors.Exists(error => error.IsSuccess == false);
            }
        }
    }
}

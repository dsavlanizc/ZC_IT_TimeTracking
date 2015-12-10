namespace ZC_IT_TimeTracking.DataAccess.Library.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    {
        public RepositoryBase()
        {
            ValidationErrors = new Validations.ValidationErrorList();
            ValidationWarnings = new Validations.ValidationErrorList();
        }
        public string ConnectionString { get; set; }

        public Validations.ValidationErrorList ValidationErrors { get; set; }
        public Validations.ValidationErrorList ValidationWarnings { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC_IT_TimeTracking.BusinessEntities;
using ZC_IT_TimeTracking.DataAccess.Interfaces.Quarters;
using ZC_IT_TimeTracking.Services.Interfaces;

namespace ZC_IT_TimeTracking.Services.Quarters
{
    public class QuarterService : ServiceBase, IQuarterService
    {
        private IQuarterRepository _repository;
        public QuarterService()
        {
            _repository = ZC_IT_TimeTracking.DataAccess.Factory.RepositoryFactory.GetInstance().GetQuarterRepository();
            this.ValidationErrors = _repository.ValidationErrors;
        }
        
        public List<GoalQuarters> GetQuarterFromYear(int year)
        {
            return _repository.GetQuarterFromYearDB(year);
        }

        public List<GoalQuarters> GetAllQuarters()
        {
            return _repository.GetAllQuartersDB();
        }

        public GoalQuarters GetQuarterById(int id)
        {
            return _repository.GetQuarterByIdDB(id);
        }

        public bool CheckQuarter(int quarter, int year)
        {
            return _repository.CheckQuarterDB(quarter, year);
        }
    }
}

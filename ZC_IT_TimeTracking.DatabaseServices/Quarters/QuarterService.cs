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
            try
            {
                var GQFY = _repository.GetQuarterFromYearDB(year);
                if (GQFY != null)
                    return GQFY;
                else
                {
                    this.ValidationErrors.Add("NO_QUA_AVL", "No Quarter Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error whle fetching data!");
                return null;
            }
        }

        public List<GoalQuarters> GetAllQuarters()
        {
            try
            {
                var GAQ = _repository.GetAllQuartersDB();
                if (GAQ != null)
                    return GAQ;
                else
                {
                    this.ValidationErrors.Add("NO_QUA_AVL", "No Quarters are available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("Quarter_FETCH_ERROR", "");
                return null;
            }
        }

        public GoalQuarters GetQuarterById(int id)
        {
            try
            {
                var GQD = _repository.GetQuarterByIdDB(id);
                if (GQD != null)
                    return GQD;
                else
                {
                    this.ValidationErrors.Add("NO_QUA_AVL", "No Such Quarter are Available!");
                    return null;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_FETCH_DATA", "Error While fetching Quarter!");
                return null;
            }
        }

        public GoalQuarters CheckQuarter(int quarter, int year)
        {
            return _repository.CheckQuarterDB(quarter, year);
        }

        public JsonResponse CreateQuarter(GoalQuarters QuarterDetail)
        {
            JsonResponse js = new JsonResponse();
            try
            {
                var Cq = CheckQuarter(QuarterDetail.GoalQuarter, QuarterDetail.QuarterYear);
                if (Cq != null)
                {
                    var qurter = _repository.CreateQuarterDB(QuarterDetail);
                    if (qurter)
                    {
                        js.message = "Quarter created successfully!";
                        js.success = true;
                        return js;
                    }
                    else
                    {
                        js.message = "Error while creating quarter!";
                        js.success = false;
                        return js;
                    }
                }
                else
                {
                    js.message = "Quarter Is already Added!";
                    js.success = false;
                    return js;
                }
            }
            catch
            {
                this.ValidationErrors.Add("ERR_DEL_GOAL", "Error Occured while Creating Quarter!");
                return null;
            }
        }
    }
}

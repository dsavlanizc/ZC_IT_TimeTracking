using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZC_IT_TimeTracking.Controllers;
using ZC_IT_TimeTracking.Models;


namespace ZC_IT_TimeTracking.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            HomeController idx = new HomeController();
            ViewResult result = idx.Index() as ViewResult;

            List<Goal_Master> list = new DatabaseEntities().Goal_Master.AsNoTracking().ToList();
            List<Goal_Master> resultList = result.Model as List<Goal_Master>;
            Assert.AreEqual(list.Count, resultList.Count);
        }
    }
}

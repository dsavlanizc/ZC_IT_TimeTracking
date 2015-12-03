using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ZC_IT_TimeTracking.DatabaseServices
{
    public class DatabaseContext : IdentityDbContext
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DatabaseEntities"].ConnectionString;
    }
}

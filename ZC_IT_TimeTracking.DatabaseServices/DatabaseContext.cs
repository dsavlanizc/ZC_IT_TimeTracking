﻿using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ZC_IT_TimeTracking.Services
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext() : base("name=DefaultConnection") { }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AisWebServicesDao
{
    public interface IDao
    {
        MySqlCommand Commander{get;}
        void SetConnectionString(String connectString);
    }
}

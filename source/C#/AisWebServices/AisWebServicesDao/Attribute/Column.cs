﻿using System;
using System.Data;

namespace AisWebServicesDao
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Column : System.Attribute
    {
        public String ColumnName
        {
            get;
            set;
        }
        public SqlDbType ColumnType
        {
            get;
            set;
        }
        public Boolean Key
        {
            get;
            set;
        }
        public Boolean Identity
        {
            get;
            set;
        }

        public String LogicalName
        {
            get;
            set;
        }
        public Column(String ColumnName, SqlDbType ColumnType)
        {

            this.ColumnName = ColumnName;
            this.ColumnType = ColumnType;
            this.Key = false;
            this.Identity = false;
        }
    }
}

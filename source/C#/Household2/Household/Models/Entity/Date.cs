﻿using System;
using Household.Common;

namespace Household.Models.Entity
{
    //Not use
    public class Date
    {
        private Int64 fastTime;
        private String cdate;

        public Int64 FastTime
        {
            set { fastTime = value; }
        }
        public string Cdate
        {
            set { cdate = value; }
        }
        public DateTime ToDateTime()
        {
            return Util.TransDateTImeByJavaTicks(fastTime);
        }
    }
}
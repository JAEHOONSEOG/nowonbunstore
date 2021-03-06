﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;
using Household.Models.Master;
using Household.Common;

namespace Household
{
    // 참고: IIS6 또는 IIS7 클래식 모드를 사용하도록 설정하는 지침을 보려면 
    // http://go.microsoft.com/?LinkId=9394801을 방문하십시오.

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            
            HttpConnector.CreateInstance(HtmlUtil.GetConfig("serviceUrl"));
            HtmlUtil.Initialize(HtmlUtil.GetConfig("clientID"), HtmlUtil.GetConfig("clientSecret"), HtmlUtil.GetConfig("redirectUrl"));
            FactoryMaster.CreateInstance();

            XmlConfigurator.Configure();
            ILog logger = LogManager.GetLogger("ApplicationStart");
            logger.Info("Started up this program.");
            
        }
    }
}
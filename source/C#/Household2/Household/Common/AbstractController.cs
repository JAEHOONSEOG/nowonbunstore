﻿using System;
using log4net;
using System.Web;
using System.Web.Mvc;
using Household.Models.Bean;

namespace Household.Common
{
    public class AbstractController : Controller
    {
        private ILog logger;

        protected AbstractController() : base()
        {
            logger = LogManager.GetLogger(this.GetType());
        }

        public LoginBean UserSession
        {
            get
            {
                return Session[Define.USER_SESSION_NAME] as LoginBean;
            }
            set
            {
                Session[Define.USER_SESSION_NAME] = value;
            }
        }

        public void SetCookie(String name, String value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(cookie);
        }

        public String GetCookie(String name)
        {
            return Request.Cookies[name] != null ? Request.Cookies[name].Value : null;
        }

        protected ActionResult WorkAjax(AbstractBean bean, Func<AbstractBean, AjaxResultBean, AjaxResultBean> work)
        {
            AjaxResultBean ret = work(bean, new AjaxResultBean());
            if (String.IsNullOrEmpty(ret.Result))
            {
                throw new ArgumentNullException();
            }
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        protected ILog Logger
        {
            get { return logger; }
        }
    }
}

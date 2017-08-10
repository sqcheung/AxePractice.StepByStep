﻿using System;
using System.Web.Http;

namespace SimpleIntegration
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration configuration = GlobalConfiguration.Configuration;
            Bootstrapper.Init(configuration, (b => { }));
        }
    }

    public class UserInfo
    {
        public string UserName => "Shengqi";
    }
}
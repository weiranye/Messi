﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Messi.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            

            return View();
        }
       
    }
}

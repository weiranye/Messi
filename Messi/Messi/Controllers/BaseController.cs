using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Messi.Controllers
{
    public class BaseController : Controller
    {
        public int CurrentUserId
        {
            get
            {
                int userId = 0;

                if (User.Identity.IsAuthenticated)
                {
                    userId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                }

                return userId;
            }
        }

        public string CurrentUserName
        {
            get
            {
                string userName = string.Empty;

                if (User.Identity.IsAuthenticated)
                {
                    userName = User.Identity.Name.Split('|')[0];
                }

                return userName;
            }
        }

    }
}

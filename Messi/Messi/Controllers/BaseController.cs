using System;
using System.Web.Mvc;
using Messi.Logic;

namespace Messi.Controllers
{
    public class BaseController : Controller
    {
        private readonly GameLogic _gameLogic=new GameLogic();
        public GameLogic GameLogic
        {
            get { return _gameLogic; }
        }

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

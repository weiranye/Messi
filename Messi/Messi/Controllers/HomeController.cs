using Messi.Logic;
using System;
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
            // testing
            //List<Tuple<string, string, string>> newGameObj = GameLogic.GetWordDefImageList();
            GameLogic.AddGame(new CreateGameObject()
            {
                ImageUrl1 = "asdf",
                ImageUrl2 = "asdf",
                ImageUrl3 = "qwer",
                ImageUrl4 = "1234",
                SelectedImageUrl = "1234",
                Word = "cat"
            });
            return View();
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Messi.ViewModels;

namespace Messi.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            
            if (GameLogic.IsGameAvailable(CurrentUserId))
            {
                return View("Game");
            }
            else
            {
                var result = GameLogic.GetWordDefImageList();
                int id = 0;
                //var createGames = result.Select(tuple => new CreateGameViewModel()
                //    {
                //        Definition = tuple.Item2,
                //        Word = tuple.Item1,
                //        ImageUrl = tuple.Item3,
                //        ImageId = id++
                //    }).ToList();
                return View("CreateGame");
            }
        }

        [HttpPost]
        public ActionResult CreateGame(int Id)
        {
            return View("CreateGame");
        }

    }
}

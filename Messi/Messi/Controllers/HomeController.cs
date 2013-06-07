using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Messi.Logic;
using Messi.ViewModels;

namespace Messi.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {
            var game = GameLogicObj.GetAndReserveGame(CurrentUserId);
            if (game!=null)
            {
                return View("Game");
            }
            else
            {
                var result = GameLogic.GetWordDefImageList();
                int id = 0;
                ListCreateGameViewModel createGames = new ListCreateGameViewModel()
                    {
                        CreateGameViewModels = result.Select(tuple => new CreateGameViewModel()
                            {
                                Definition = tuple.Item2,
                                Word = tuple.Item1,
                                ImageUrl = tuple.Item3,
                                ImageId = id++
                            }).ToList()
                    };
                Session["CreateGames"] = createGames;
                return View("CreateGame",createGames);
            }
        }

        [HttpPost]
        public ActionResult ShowDefinition(int id)
        {
            ListCreateGameViewModel createGames = (ListCreateGameViewModel)Session["CreateGames"];
            var createGame = createGames.CreateGameViewModels.FirstOrDefault(cg => cg.ImageId == id);
            Session["CreateGame"] = createGame;
            return PartialView("ShowDefinition", createGame);
        }

        [HttpPost]
        public ActionResult SaveData(FormCollection form)
        {
            var test = "";
            return View("Index");
        }

    }
}

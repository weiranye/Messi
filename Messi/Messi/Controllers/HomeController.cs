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
                TempData["CreateGames"] = createGames;
                return View("CreateGame",createGames);
            }
        }

        [HttpPost]
        public ActionResult ShowDefinition(int id)
        {
            ListCreateGameViewModel createGames = (ListCreateGameViewModel)TempData["CreateGames"];
            var createGame = createGames.CreateGameViewModels.FirstOrDefault(cg => cg.ImageId == id);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messi.Logic
{

    public class GameLogic
    {
        // <Word, Definition, ImageUrl>
        public List<Tuple<string, string, string>> GetWordDefImageList()
        {
            return null;
        }

        // returns GameId
        public int AddGame(CreateGameObject obj)
        {
            return 0;
        }

        public int AddRound(int gameId, int roundNum, int userId, string recognizedText)
        {
            return 0;
        }

        public bool IsGameAvailable(int userId)
        {
            return false;
        }

        public GameObject GetGame(int userId)
        {
            return null;
        }
    }

    public class CreateGameObject
    {
        public string Word { get; set; }
        public string SelectedImageUrl { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }
    }

    public class GameObject
    {
        public int RoundNum { get; set; }
        public int GameId { get; set; }
        public string Text { get; set; }
        public CreateGameObject CreateGameObj { get; set; }
    }
}
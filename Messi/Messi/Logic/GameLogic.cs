using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Messi.Models;

namespace Messi.Logic
{
    public class GameLogic
    {
        // constants
        public const int MAX_ROUNDS = 4;

        // <Word, Definition, ImageUrl>
        public static List<Tuple<string, string, string>> GetWordDefImageList()
        {
            List<Tuple<string, string, string>> result = new List<Tuple<string, string, string>>();

            // get 4 random words
            List<string> fourWords = new List<string>();
            while (fourWords.Count < 4)
            {
                Random rnd = new Random();
                string randomWord = WordRepo.Words[rnd.Next(0, WordRepo.Words.Count)];
                if (!fourWords.Contains(randomWord))
                {
                    fourWords.Add(randomWord);
                }
            }

            string imageUrl = "";
            string definition = "";
            for (int i = 0; i < 4; i++)
            {
                // get imageUrl
                DkApiResult dkResult = Helper.ImageLookUp(fourWords[i]);
                if (dkResult.total < 1)
                {
                    throw new Exception("Number of images for this word is less than 1. Word: " + fourWords[i]);
                }
                else
                {
                    imageUrl = dkResult.images[0].url;
                }

                // get definition
                JObject lmResult = Helper.DefinitionLookUpObj(fourWords[i]);
                JToken lmEntry = (lmResult["Entries"]["Entry"] is JArray) ? lmResult["Entries"]["Entry"][0] : lmResult["Entries"]["Entry"];
                JToken lmSense = (lmEntry["Sense"] is JArray) ? lmEntry["Sense"][0] : lmEntry["Sense"];
                JToken lmDEF = lmSense["DEF"];
                if (lmDEF == null)
                {
                    JToken lmSubsense = (lmSense["Subsense"] is JArray) ? lmSense["Subsense"][0] : lmSense["Subsense"];
                    lmDEF = lmSubsense["DEF"];
                }
                definition = lmDEF["#text"].ToString();

                // done
                result.Add(new Tuple<string, string, string>(fourWords[i], imageUrl, definition));
            }
            return result;
        }

        // returns GameId
        public static int AddGame(CreateGameObject obj)
        {
            using (Models.Messi messi = new Models.Messi())
            {
                Game newGame = new Game()
                {
                    StatusId = 1,
                    ImageUrl1 = obj.ImageUrl1,
                    ImageUrl2 = obj.ImageUrl2,
                    ImageUrl3 = obj.ImageUrl3,
                    ImageUrl4 = obj.ImageUrl4,
                    SelectedImageUrl = obj.SelectedImageUrl,
                    Word = obj.Word
                };
                messi.Games.Add(newGame);
                messi.SaveChanges();
                return newGame.GameId;
            }
        }

        // change Game status as well if adding the last round
        public int AddRound(Round round)
        {
            using (Models.Messi messi = new Models.Messi())
            {
                messi.Rounds.Add(round);
                if (round.RoundNum == MAX_ROUNDS)
                {
                    round.Game.StatusId = 3;
                }
                messi.SaveChanges();
                return round.RoundId;
            }
        }

        // return null if no available game; otherwise, return the game and reserve it. 
        public GameObject GetAndReserveGame(int userId)
        {
            // avaliable game: 1) status is open; 2) not created/played by the given user; 3) has at least one round
            using (Models.Messi messi = new Models.Messi())
            {
                Game anAvailableGame = messi.Games.Where(g =>
                    g.StatusId == 1
                    && g.Rounds.Where(r => r.UserId == userId).FirstOrDefault() == null
                    && g.Rounds.Count > 0
                ).FirstOrDefault();

                if (anAvailableGame != null)
                {
                    GameObject game = new GameObject()
                    {
                        GameId = anAvailableGame.GameId,
                        RoundNum = anAvailableGame.Rounds.Max(r => r.RoundNum + 1),
                        Text = anAvailableGame.Word
                    };
                    // reserve it
                    anAvailableGame.StatusId = 2;
                    messi.SaveChanges();
                    return game;
                }
                else return null;
            }
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
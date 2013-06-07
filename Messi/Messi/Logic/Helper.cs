using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Messi.Logic
{
    public class Helper
    {
        public const string DK_KEY = "9b7305c0523c3902ec01b44e5a5c53ad";
        public const string DK_URL = "https://api.pearson.com/dk/v1/images";
        public const string LM_KEY = "9b7305c0523c3902ec01b44e5a5c53ad";
        public const string LM_URL = "https://api.pearson.com/longman/dictionary/entry.json";

        public static HttpResponseMessage ApiRequest(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            return client.GetAsync("").Result;
        }

        public static DkApiResult ImageLookUp(string word)
        {
            HttpResponseMessage response = ApiRequest(DK_URL + "?caption=" + word + "&apikey=" + DK_KEY);
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                DkApiResult result = JsonConvert.DeserializeObject<DkApiResult>(jsonResult);
                return result;
            }
            else throw new Exception("Failed getting result back from DK API. ImageLookUp failed. ");
        }

        public static JObject DefinitionLookUpObj(string word)
        {
            HttpResponseMessage response = ApiRequest(LM_URL + "?q=" + word + "&apikey=" + LM_KEY);
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                return (JObject) JsonConvert.DeserializeObject(jsonResult);
            }
            else throw new Exception("Failed getting result back from Longman API. DefinitionLookUp failed. ");
        }
    }

    public class DkApiResult
    {
        public int total { get; set; }
        public List<DkApiResultImage> images { get; set; }
    }

    public class DkApiResultImage
    {
        public string url { get; set; }
    }
}
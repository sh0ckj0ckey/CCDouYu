using DouyuTV.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DouyuTV.Helpers
{
    public static class ApiHelper
    {
        private static async Task<string> GetJson(string url)
        {
            string jsonMessage = "";
            using (HttpClient http = new HttpClient())
            {
                try
                {
                    var response = await http.GetAsync(new Uri(url));
                    jsonMessage = await response.Content.ReadAsStringAsync();
                }
                catch
                { }
            }
            return jsonMessage;
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        public static async Task<Games> GetGamesCategory()
        {
            Games games = null;
            try
            {
                string jsonMessage = await GetJson("http://open.douyucdn.cn/api/RoomApi/game");
                JsonSerializerSettings jss = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                games = JsonConvert.DeserializeObject<Games>(jsonMessage, jss);
            }
            catch
            { }
            return games;
        }

        /// <summary>
        /// 获取首页轮播图
        /// </summary>
        /// <returns></returns>
        public static async Task<Slides> GetSlides()
        {
            Slides slides = null;
            try
            {
                string jsonMessage = await GetJson("http://www.douyutv.com/api/v1/slide/6");
                JsonSerializerSettings jss = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                slides = JsonConvert.DeserializeObject<Slides>(jsonMessage, jss);
            }
            catch
            { }
            return slides;
        }

        /// <summary>
        /// 获取全部直播房间
        /// </summary>
        /// <returns></returns>
        public static async Task<Rooms> GetAllRooms(string limit, string offset)
        {
            Rooms rooms = null;
            try
            {
                string jsonMessage = await GetJson("http://capi.douyucdn.cn/api/v1/live?limit=" + limit + "&offset=" + offset);
                JsonSerializerSettings jss = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                if (jsonMessage.StartsWith("\"Not Found\"") || jsonMessage.StartsWith("Not Found"))
                {
                    return new Rooms
                    {
                        error = 1234
                    };
                }
                rooms = JsonConvert.DeserializeObject<Rooms>(jsonMessage, jss);
            }
            catch
            { }
            return rooms;
        }

        /// <summary>
        /// 获取指定分类的直播房间
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static async Task<CategoryRooms> GetCategoryRooms(string cateid, string limit, string offset)
        {
            CategoryRooms rooms = null;
            try
            {
                string jsonMessage = await GetJson("http://open.douyucdn.cn/api/RoomApi/live/" + cateid + "?limit=" + limit + "&offset=" + offset);
                JsonSerializerSettings jss = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                if (jsonMessage.StartsWith("\"Not Found\"") || jsonMessage.StartsWith("Not Found"))
                {
                    return new CategoryRooms
                    {
                        error = 1234
                    };
                }
                rooms = JsonConvert.DeserializeObject<CategoryRooms>(jsonMessage, jss);
            }
            catch
            { }
            return rooms;
        }

    }
}

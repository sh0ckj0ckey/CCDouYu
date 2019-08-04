using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouyuTV.ViewModels
{
    public class Games
    {
        public int error { get; set; }
        public GamesCategoryViewModel[] data { get; set; }
    }

    public class GamesCategoryViewModel
    {
        public int cate_id { get; set; }
        public string game_name { get; set; }
        public string short_name { get; set; }
        public string game_url { get; set; }
        public string game_src { get; set; }
        public string game_icon { get; set; }
    }
}

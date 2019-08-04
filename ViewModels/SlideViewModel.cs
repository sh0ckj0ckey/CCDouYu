using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouyuTV.ViewModels
{
    public class Slides
    {
        public int error { get; set; }
        public SlideViewModel[] data { get; set; }
    }

    public class SlideViewModel
    {
        public int id { get; set; }
        public int main_id { get; set; }
        public int source { get; set; }
        public int oa_source { get; set; }
        public string title { get; set; }
        public string pic_url { get; set; }
        public string tv_pic_url { get; set; }
        public Room room { get; set; }
    }

    public class Room
    {
        public string room_id { get; set; }
        public string room_src { get; set; }
        public string vertical_src { get; set; }
        public int isVertical { get; set; }
        public string cate_id { get; set; }
        public string room_name { get; set; }
        public string vod_quality { get; set; }
        public string show_status { get; set; }
        public string show_time { get; set; }
        public string owner_uid { get; set; }
        public string specific_catalog { get; set; }
        public string specific_status { get; set; }
        public string credit_illegal { get; set; }
        public string is_white_list { get; set; }
        public string cur_credit { get; set; }
        public string low_credit { get; set; }
        public int online { get; set; }
        public string nickname { get; set; }
        public string url { get; set; }
        public string game_url { get; set; }
        public string game_name { get; set; }
        public string game_icon_url { get; set; }
        public string show_details { get; set; }
        public string owner_avatar { get; set; }
        public Cdnswithname[] cdnsWithName { get; set; }
        public int is_pass_player { get; set; }
        public int open_full_screen { get; set; }
        public string nrt { get; set; }
        public string owner_weight { get; set; }
        public string fans { get; set; }
        public string column_id { get; set; }
        public Cate_Limit cate_limit { get; set; }
    }

    public class Cate_Limit
    {
        public int limit_type { get; set; }
        public int limit_num { get; set; }
        public int limit_threshold { get; set; }
        public int limit_time { get; set; }
    }

    public class Cdnswithname
    {
        public string name { get; set; }
        public string cdn { get; set; }
    }

}

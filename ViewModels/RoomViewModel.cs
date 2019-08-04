using DouyuTV.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace DouyuTV.ViewModels
{
    public class Rooms
    {
        public int error { get; set; }
        public RoomViewModel[] data { get; set; }
    }

    public class RoomViewModel
    {
        public string room_id { get; set; }
        public string room_src { get; set; }
        public string vertical_src { get; set; }
        public int isVertical { get; set; }
        public int cate_id { get; set; }
        public string room_name { get; set; }
        public string show_status { get; set; }
        public string subject { get; set; }
        public string show_time { get; set; }
        public string owner_uid { get; set; }
        public string specific_catalog { get; set; }
        public string specific_status { get; set; }
        public string vod_quality { get; set; }
        public string nickname { get; set; }
        public int online { get; set; }
        public int hn { get; set; }
        public string url { get; set; }
        public string game_url { get; set; }
        public string game_name { get; set; }
        public int child_id { get; set; }
        public string avatar { get; set; }
        public string avatar_mid { get; set; }
        public string avatar_small { get; set; }
        public string jumpUrl { get; set; }
        public string fans { get; set; }
        public int ranktype { get; set; }
        public int is_noble_rec { get; set; }
        public string anchor_city { get; set; }
    }

    public class AllRoomsIncrementalLoadingCollection : ObservableCollection<RoomViewModel>, ISupportIncrementalLoading
    {
        // 起始位置，比如要获取前20个之后的房间，则 offset=20
        uint roomPageOffset = 0;

        public bool HasMoreItems { get; set; }

        //the count is the number requested
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async cancelToken =>
            {
                Rooms gottenRoom = null;
                gottenRoom = await ApiHelper.GetAllRooms("20", roomPageOffset.ToString());
                if (gottenRoom != null && gottenRoom.error == 0)
                {
                    roomPageOffset += 20;
                    int itemCount = 0;
                    foreach (var item in gottenRoom.data)
                    {
                        this.Add(item);
                        itemCount++;
                    }
                    if (itemCount < 20)
                    {
                        this.HasMoreItems = false;
                    }
                    else
                    {
                        this.HasMoreItems = true;
                    }
                }
                else
                {
                    if (gottenRoom != null && gottenRoom.error != 1234)
                    {
                        ShowDialog("获取数据失败，请重试\r\n" + gottenRoom.error);
                    }
                    else if (gottenRoom != null && gottenRoom.error == 1234)
                    {

                    }
                    else
                    {
                        ShowDialog("获取数据失败，请重试\r\n");
                    }
                    this.HasMoreItems = false;
                    return new LoadMoreItemsResult { Count = 0 };
                }
                //return the actual number of items loaded (here it's just maxed)
                return new LoadMoreItemsResult { Count = count };
            });
        }

        /// <summary>
        /// 显示程序异常对话框，自定义内容
        /// </summary>
        public static async void ShowDialog(string content)
        {
            var dialog = new Windows.UI.Xaml.Controls.ContentDialog()
            {
                Title = ":(",
                Content = content,
                PrimaryButtonText = "好的",
                FullSizeDesired = false
            };

            dialog.PrimaryButtonClick += (_s, _e) => { };
            try
            {
                await dialog.ShowAsync();
            }
            catch { }
        }
    }

}

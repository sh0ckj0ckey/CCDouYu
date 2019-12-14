using DouyuTV.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace DouyuTV.ViewModels
{
    public class CategoryRooms
    {
        public int error { get; set; }
        public CategoryRoomViewModel[] data { get; set; }
    }

    public class CategoryRoomViewModel
    {
        public int room_id { get; set; }
        public string room_src { get; set; }
        public string room_name { get; set; }
        public string owner_uid { get; set; }
        public int online { get; set; }
        public int hn { get; set; }
        public string nickname { get; set; }
        public string url { get; set; }
    }

    public class CategoryRoomsIncrementalLoadingCollection : ObservableCollection<CategoryRoomViewModel>, ISupportIncrementalLoading
    {
        /// <summary>
        /// 起始位置，比如要获取前20个之后的房间，则 offset=20
        /// </summary>
        uint roomPageOffset = 0;

        /// <summary>
        /// 标记当前所加载的房间类别
        /// </summary>
        public string CategoryId { get; set; }

        public bool HasMoreItems { get; set; }

        //the count is the number requested
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async cancelToken =>
            {
                CategoryRooms gottenRoom = null;
                gottenRoom = await ApiHelper.GetCategoryRooms(this.CategoryId, "20", roomPageOffset.ToString());
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

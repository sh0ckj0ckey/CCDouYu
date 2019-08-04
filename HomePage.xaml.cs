using DouyuTV.Helpers;
using DouyuTV.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DouyuTV
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        ObservableCollection<SlideViewModel> SlidesObservableCollection = new ObservableCollection<SlideViewModel>();
        AllRoomsIncrementalLoadingCollection RoomsObservableCollection = new AllRoomsIncrementalLoadingCollection();

        public HomePage()
        {
            this.InitializeComponent();
            _ = LoadSlide();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RoomsObservableCollection.HasMoreItems = true;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            RoomsObservableCollection.HasMoreItems = false;
            RoomsObservableCollection.Clear();
            RoomsObservableCollection = null;
            SlidesObservableCollection.Clear();
            SlidesObservableCollection = null;
            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// 加载轮播图
        /// </summary>
        public async Task LoadSlide()
        {
            Slides slides = await ApiHelper.GetSlides();
            if (slides != null && slides.error == 0)
            {
                foreach (var item in slides.data)
                {
                    SlidesObservableCollection.Add(item);
                }
            }
            else
            {
                SlidesObservableCollection.Add(new SlideViewModel { tv_pic_url = "ms-appx:///Assets/Pictures/nocontent.png", title = "获取轮播图失败" });
            }
        }

        /// <summary>
        /// 点击热门推荐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton btn = sender as AppBarButton;
            RoomViewModel room = (RoomViewModel)btn.DataContext;
            this.Frame.Navigate(typeof(WatchingPage), room.room_id);
        }

        /// <summary>
        /// 点击轮播图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            AppBarButton btn = sender as AppBarButton;
            SlideViewModel slide = (SlideViewModel)btn.DataContext;
            this.Frame.Navigate(typeof(WatchingPage), slide.room.room_id);
        }

        /// <summary>
        /// 进入直播间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WatchingPage), EnterRoomTextBox.Text);
        }


    }
}

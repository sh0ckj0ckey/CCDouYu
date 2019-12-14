using DouyuTV.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DouyuTV
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CategoryRoomPage : Page
    {
        GamesCategoryViewModel selectedCategory;
        CategoryRoomsIncrementalLoadingCollection CateRoomsObservableCollection = new CategoryRoomsIncrementalLoadingCollection();
        public CategoryRoomPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType().Equals(typeof(GamesCategoryViewModel)))
            {
                try
                {
                    CateRoomsObservableCollection.HasMoreItems = true;
                    selectedCategory = (GamesCategoryViewModel)e.Parameter;
                    CateRoomsObservableCollection.CategoryId = selectedCategory.cate_id.ToString();
                    TitleImage.Source = new BitmapImage(new Uri(selectedCategory.game_icon, UriKind.Absolute));
                    TitleTextBlock.Text = selectedCategory.game_name;
                }
                catch
                {
                    // error
                }
            }
            else
            {
                // error
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            CateRoomsObservableCollection.HasMoreItems = false;
            CateRoomsObservableCollection.Clear();
            CateRoomsObservableCollection = null;
            base.OnNavigatedFrom(e);
        }


        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack == true)
            {
                this.Frame.GoBack();
            }
        }

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            AppBarButton btn = sender as AppBarButton;
            CategoryRoomViewModel room = (CategoryRoomViewModel)btn.DataContext;
            this.Frame.Navigate(typeof(WatchingPage), room.room_id.ToString());
        }
    }
}

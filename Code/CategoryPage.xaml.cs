using DouyuTV.Helpers;
using DouyuTV.ViewModels;
using MaterialLibs.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DouyuTV
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        ObservableCollection<GamesCategoryViewModel> CategoryObservableCollection = new ObservableCollection<GamesCategoryViewModel>();
        ObservableCollection<GamesCategoryViewModel> HotCategoryObservableCollection = new ObservableCollection<GamesCategoryViewModel>();
        public CategoryPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 页面加载完成后获取分类列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Games games = await ApiHelper.GetGamesCategory();
            if (games == null)
            {
                FailedGrid.Visibility = Visibility.Visible;
                FailedNoteTextBlock.Text = "加载失败，请重试~";
                return;
            }
            else if (games.error != 0)
            {
                FailedGrid.Visibility = Visibility.Visible;
                FailedNoteTextBlock.Text = "加载失败，错误代码：" + games.error + "，烦请反馈~";
                return;
            }
            else
            {
                foreach (var item in games.data)
                {
                    CategoryObservableCollection.Add(item);
                }
                CategoryGridView.ItemsSource = CategoryObservableCollection;

                for (int i = 0; i < 20; i++)
                {
                    HotCategoryObservableCollection.Add(CategoryObservableCollection[i]);
                }
                HotCategoryListView.ItemsSource = HotCategoryObservableCollection;
            }
            FailedGrid.Visibility = Visibility.Collapsed;
            LoadingProgressRing.IsActive = false;
            LoadingGrid.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HotCategoryObservableCollection.Clear();
            HotCategoryObservableCollection = null;
            CategoryObservableCollection.Clear();
            CategoryObservableCollection = null;
            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// 推荐分类封面图片指向缩放的动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (sender is UIElement s)
            {
                VisualHelper.SetScale(s, "1.15,1.15,1.15");
            }
        }

        /// <summary>
        /// 推荐分类封面图片指向缩放的动画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (sender is UIElement s)
            {
                VisualHelper.SetScale(s, "1,1,1");
            }
        }

        private void HotCategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HotCategoryListView.SelectedIndex == -1)
            {
                return;
            }
            this.Frame.Navigate(typeof(CategoryRoomPage), HotCategoryObservableCollection[HotCategoryListView.SelectedIndex]);
            HotCategoryListView.SelectedIndex = -1;
        }

        private void CategoryGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryGridView.SelectedIndex == -1)
            {
                return;
            }
            this.Frame.Navigate(typeof(CategoryRoomPage), CategoryObservableCollection[CategoryGridView.SelectedIndex]);
            CategoryGridView.SelectedIndex = -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace DouyuTV
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage mainPage = null;

        public MainPage()
        {
            this.InitializeComponent();

            mainPage = this;

            // 设置标题栏样式
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.Black;
            titleBar.ButtonInactiveForegroundColor = Colors.Gray;

            if (App.SettingContainer.Values["acrylic"] == null || App.SettingContainer.Values["acrylic"].ToString() == "on")
            {
                MainPageAcrylicBrush.AlwaysUseFallback = false;
            }
            else
            {
                MainPageAcrylicBrush.AlwaysUseFallback = true;
            }

            LargeMenuListView.SelectedIndex = 0;
            CompactMenuListView.SelectedIndex = 0;
        }

        /// <summary>
        /// 完整样式的侧栏菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LargeMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompactMenuListView.SelectedIndex != LargeMenuListView.SelectedIndex)
            {
                CompactMenuListView.SelectedIndex = LargeMenuListView.SelectedIndex;
            }
            switch (LargeMenuListView.SelectedIndex)
            {
                case 0:
                    MainFrame.Navigate(typeof(HomePage));
                    break;
                case 1:
                    MainFrame.Navigate(typeof(CategoryPage));
                    break;
                case 2:
                    MainFrame.Navigate(typeof(FollowingPage));
                    break;
                case 3:
                    MainFrame.Navigate(typeof(SettingPage));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 收缩样式的侧栏菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompactMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LargeMenuListView.SelectedIndex != CompactMenuListView.SelectedIndex)
            {
                LargeMenuListView.SelectedIndex = CompactMenuListView.SelectedIndex;
            }
            switch (CompactMenuListView.SelectedIndex)
            {
                case 0:
                    MainFrame.Navigate(typeof(HomePage));
                    break;
                case 1:
                    MainFrame.Navigate(typeof(CategoryPage));
                    break;
                case 2:
                    MainFrame.Navigate(typeof(FollowingPage));
                    break;
                case 3:
                    MainFrame.Navigate(typeof(SettingPage));
                    break;
                default:
                    break;
            }
        }


    }
}

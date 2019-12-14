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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DouyuTV
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();

            if (App.SettingContainer.Values["acrylic"] == null || App.SettingContainer.Values["acrylic"].ToString() == "on")
            {
                AcrylicToggleSwitch.IsOn = true;
            }
            else
            {
                AcrylicToggleSwitch.IsOn = false;
            }
        }

        /// <summary>
        /// 设置亚克力是否启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcrylicToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (AcrylicToggleSwitch.IsOn == true)
            {
                MainPage.mainPage.MainPageAcrylicBrush.AlwaysUseFallback = false;
                App.SettingContainer.Values["acrylic"] = "on";
            }
            else
            {
                MainPage.mainPage.MainPageAcrylicBrush.AlwaysUseFallback = true;
                App.SettingContainer.Values["acrylic"] = "off";
            }
        }

        /// <summary>
        /// 去我的 Steam 页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://steamcommunity.com/profiles/76561198194624815/"));
        }
    }
}

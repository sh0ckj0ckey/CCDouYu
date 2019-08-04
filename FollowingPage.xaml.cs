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
    public sealed partial class FollowingPage : Page
    {
        public FollowingPage()
        {
            this.InitializeComponent();
        }

        private void SigninButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 注册，跳转网页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.douyu.com/"));
        }
    }
}

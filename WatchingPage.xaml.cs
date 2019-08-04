using DouyuTV.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Streaming.Adaptive;
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
    public sealed partial class WatchingPage : Page
    {
        string watchingRoomId;

        public WatchingPage()
        {
            this.InitializeComponent();
            InitializeAdaptiveMediaSource(new Uri("http://hls1a.douyucdn.cn/live/60937rMqtkMFa5Uu_550/playlist.m3u8?wsSecret=e48338eba0a50e00ad7b2ede1120c1f8&wsTime=1559203393&token=h5-douyu-0-60937-cb83845926c7d39bd9e72c278fd1b5e4&did=h5_did"));
        }

        async private void InitializeAdaptiveMediaSource(System.Uri uri)
        {
            StreamMediaPlayerElement.Source = MediaSource.CreateFromUri(uri);
            StreamMediaPlayerElement.MediaPlayer.Play();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType().Equals(typeof(string)))
            {
                try
                {
                    watchingRoomId = (string)e.Parameter;
                    Tb.Text = watchingRoomId;
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
    }
}

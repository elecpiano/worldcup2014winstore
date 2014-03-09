using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class SpinningBall : UserControl
    {
        public SpinningBall()
        {
            this.InitializeComponent();
            StartSpin();
        }

        #region Spinning Ball

        private DispatcherTimer timer;
        private int ballIndex = 1;
        private const int ballMaxIndex = 27;

        public void StartSpin()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(30);
                timer.Tick += timer_Tick;
            }
            timer.Start();
        }

        void timer_Tick(object sender, object e)
        {
            if (ballIndex > ballMaxIndex)
            {
                ballIndex = 1;
            }
            imageBall.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/SpinningBall/" + ballIndex.ToString() + ".png", UriKind.Absolute));
            ballIndex++;
        }

        #endregion

    }
}

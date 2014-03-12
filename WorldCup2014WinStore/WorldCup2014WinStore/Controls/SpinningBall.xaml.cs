using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class SpinningBall : UserControl
    {
        #region Singleton

        private static SpinningBall _Current;

        public static SpinningBall Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new SpinningBall();
                }
                return _Current;
            }
        }

        #endregion

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

        public static void Attach(Grid parent)
        {
            parent.Children.Add(SpinningBall.Current);
        }

        public static void Detach()
        {
            if (SpinningBall.Current.Parent != null)
            {
                ((Grid)SpinningBall.Current.Parent).Children.Remove(SpinningBall.Current);
            }
        }

        #endregion

    }
}

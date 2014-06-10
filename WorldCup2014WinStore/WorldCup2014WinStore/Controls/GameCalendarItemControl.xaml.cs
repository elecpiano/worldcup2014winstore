using System;
using Utility.Animations;
using Windows.UI.Xaml.Controls;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class GameCalendarItemControl : UserControl
    {
        private static TimeSpan duration = TimeSpan.FromMilliseconds(50);
        public GameCalendarItemControl()
        {
            InitializeComponent();
        }

        private void Border_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FadeAnimation.Fade(rect, 0d, 1d, duration,
                fe =>
                {
                    FadeAnimation.Fade(fe, 1d, 0d, duration, null);
                });
        }

    }
}

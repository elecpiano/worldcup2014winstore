using System;
using Utility.Animations;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class NewsListPage : PageBase
    {
        public static bool NavigatingFromHome = false;

        public NewsListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigatingFromHome)
            {
                PageMask.AttachAndOpen(this.maskPanel, () =>
                    {
                        PageTitle.AttachAndShow(this.pageTitlePanel, "News List");
                        flipTiles.Expand(2);
                    });
            }
            else
            {
                PageMask.Attach(this.maskPanel);
                PageTitle.AttachAndShow(this.pageTitlePanel, "News List");
                FadeAnimation.Fade(this.backgroundClear, 0d, 1d, Constants.DURATION_CONTENT_FADING, null);
                flipTiles.Expand(0);
                FadeAnimation.Fade(this.contentPanel, 0d, 1d, Constants.DURATION_CONTENT_FADING, null);
            }

            NavigatingFromHome = false;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SpinningBall.Detach();
            PageMask.Detach();
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

        public override void OnBack()
        {
            PageTitle.Hide();
            PageMask.Close(() =>
            {
                base.OnBack();
            });
        }

        private void Item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageTitle.Hide();
            FadeAnimation.Fade(this.contentPanel, 1d, 0d, Constants.DURATION_CONTENT_FADING, null);
            FadeAnimation.Fade(this.backgroundClear, 1d, 0d, Constants.DURATION_CONTENT_FADING, fe =>
            {
                this.Frame.Navigate(typeof(NewsDetailPage));
            });

        }

    }
}

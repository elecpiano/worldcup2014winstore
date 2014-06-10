using Utility.Animations;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class NewsDetailPage : PageBase
    {
        public NewsDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PageTitle.AttachAndShow(this.pageTitlePanel, "News Detail Page #001");
            FadeAnimation.Fade(this.contentPanel, 0d, 1d, Constants.DURATION_CONTENT_FADING, null);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SpinningBall.Detach();
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

        public override void OnBack()
        {
            PageTitle.Hide();
            FadeAnimation.Fade(this.contentPanel, 1d, 0d, Constants.DURATION_CONTENT_FADING, fe =>
                {
                    base.OnBack();
                });
        }

    }
}

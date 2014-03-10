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
            base.ContentPanel = contentPanel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PageTitle.AttachAndShow(this.pageTitlePanel, "News Detail Page #001");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

    }
}

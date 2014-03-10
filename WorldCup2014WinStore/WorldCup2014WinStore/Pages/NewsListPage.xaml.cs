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
            base.ContentPanel = contentPanel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigatingFromHome)
            {
                PageMask.AttachAndOpen(this.maskPanel, () =>
                    {
                        PageTitle.AttachAndShow(this.pageTitlePanel, "News List");
                    });
            }
            else
            {
                PageMask.Attach(this.maskPanel);
                PageTitle.AttachAndShow(this.pageTitlePanel, "News List");
            }

            NavigatingFromHome = false;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PageMask.Detach();
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

        public override void OnBack()
        {
            PageMask.Close(() =>
            {
                base.OnBack();
            });
        }

        private void Item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageTitle.Hide(() => Navigate(typeof(NewsDetailPage)));
            ;
        }

    }
}

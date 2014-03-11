using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class HomePage : PageBase
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PageMask.AttachAndOpen(this.maskPanel,() =>
                {
                    PageTitle.AttachAndShow(this.pageTitlePanel, "Homepage");
                    flipTiles.Expand();
                });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PageMask.Detach();
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

        private void SwitchMask_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PageMask.Close(() =>
            {
                NewsListPage.NavigatingFromHome = true;
                this.Frame.Navigate(typeof(NewsListPage));
            });
        }


    }
}

//var properties = e.GetCurrentPoint(this).Properties;
//if (properties.IsLeftButtonPressed)
//{
//}
//else if (properties.IsRightButtonPressed)
//{
//}

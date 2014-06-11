using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WorldCup2014WinStore.Pages;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class NavBar : AppBar
    {
        private Page page = null;

        public NavBar(Page page)
        {
            this.InitializeComponent();
            this.page = page;
        }

        private void OnTV(object sender, RoutedEventArgs e)
        {
            HideAppBars();
            this.page.Frame.Navigate(typeof(EpgListPage));
        }

        private void OnChapter(object sender, RoutedEventArgs e)
        {
            HideAppBars();
            //this.page.Frame.Navigate(typeof(Catalog));
        }

        private void OnMark(object sender, RoutedEventArgs e)
        {
            HideAppBars();
            //this.page.Frame.Navigate(typeof(Bookmark));
        }

        private void HideAppBars()
        {
            if (page.TopAppBar != null)
            {
                page.TopAppBar.IsOpen = false;
            }
            if (page.BottomAppBar != null)
            {
                page.BottomAppBar.IsOpen = false;
            }
        }


    }
}

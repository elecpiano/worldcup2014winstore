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

        private void OnItemClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Tag.ToString())
            {
                case "tv":
                    this.page.Frame.Navigate(typeof(EpgListPage));
                    break;
                case "stadium":
                    this.page.Frame.Navigate(typeof(StadiumListPage));
                    break;
                default:
                    break;
            }
            HideAppBars();
        }
    }
}

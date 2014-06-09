using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class PageTitle : UserControl
    {
        #region Properties

        private static PageBase HostingPage;

        #endregion

        public PageTitle()
        {
            this.InitializeComponent();
        }

        public void Show(string pageTitle)
        {
            this.pageTitleTextBlock.Text = pageTitle;
            this.StoryShowPageTile.Begin();
        }

        public void Hide()
        {
            this.StoryHidePageTile.Begin();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (HostingPage == null)
            {
                return;
            }
            HostingPage.OnBack();
        }

        #region Auto Registration

        public static void RegisterForNavigation()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigated += rootFrame_Navigated;
        }

        static void rootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            PageBase page = e.Content as PageBase;
            HostingPage = page;
        }

        #endregion

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

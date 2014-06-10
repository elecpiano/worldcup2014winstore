using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class PageTitle : UserControl
    {
        #region Properties

        private static Page HostingPage;

        #endregion

        public PageTitle()
        {
            this.InitializeComponent();
        }

        public void Show(string pageTitle)
        {
            //HostingPage = page;
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
            GoBack();
        }

        private void GoBack()
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                if (frame.CanGoBack)
                {
                    frame.GoBack();
                }
            }
        }

        #region Auto Registration

        public static void RegisterForNavigation()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigated += rootFrame_Navigated;
        }

        static void rootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            Page page = e.Content as Page;
            HostingPage = page;
        }

        #endregion

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTopAppBar();
        }

        private void ShowTopAppBar()
        {
            if (HostingPage.TopAppBar != null)
            {
                HostingPage.TopAppBar.IsOpen = true;
            }
            //if (HostingPage.BottomAppBar != null)
            //{
            //    HostingPage.BottomAppBar.IsOpen = false;
            //}
        }

    }
}

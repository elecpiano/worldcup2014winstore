using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;
using System.Collections.Generic;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class NewsDetailPage : Page
    {
        #region Property

        App App { get { return App.Current as App; } }

        private string newsID = string.Empty;
        private string newsTitle = string.Empty;
        private string newsImage = string.Empty;

        #endregion

        #region Lifecycle

        public NewsDetailPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
            if (param != null)
            {
                newsID = param[NaviParam.NEWS_ID];
                newsTitle = param[NaviParam.NEWS_TITLE];
                newsImage = param[NaviParam.NEWS_IMAGE];
            }

            pageTitle.Show(newsTitle);

            LoadHTML();
        }

        #endregion

        #region HTML

        DataLoader<HTML> htmlLoader = new DataLoader<HTML>();

        private void LoadHTML()
        {
            if (htmlLoader.Loaded || htmlLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            htmlLoader.Load("getdetail", "&id=" + newsID, true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_DETAIL_FILE_NAME_FORMAT, newsID),
                html =>
                {
                    string title = @"<h2 align=""center"">" + newsTitle + "</h2>";
                    string htmlContent = html.Content.Insert(html.Content.IndexOf("</style>") + 8, title);
                    htmlContent = htmlContent.Replace("max-width: 100%;", "width: 100%;");
                    browser.NavigateToString(htmlContent);
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion


    }
}

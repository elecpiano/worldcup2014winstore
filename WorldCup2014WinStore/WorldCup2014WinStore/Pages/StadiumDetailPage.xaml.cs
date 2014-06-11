using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;
using System.Collections.Generic;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class StadiumDetailPage : Page
    {
        #region Property

        App App { get { return App.Current as App; } }

        private string stadiumID = string.Empty;
        private string stadiumName = string.Empty;

        #endregion

        #region Lifecycle

        public StadiumDetailPage()
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
                stadiumID = param[NaviParam.STADIUM_ID];
                stadiumName = param[NaviParam.STADIUM_NAME];
            }

            pageTitle.Show(stadiumName);

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

            htmlLoader.Load("getstadiumdetail", "&id=" + stadiumID, true, Constants.STADIUM_MODULE, string.Format(Constants.STADIUM_DETAIL_FILE_NAME_FORMAT, stadiumID),
                html =>
                {
                    string title = @"<h2 align=""center"">" + stadiumName + "</h2>";
                    string htmlContent = html.Content.Insert(html.Content.IndexOf("</style>") + 8, title);
                    htmlContent = htmlContent.Replace("max-width: 100%;", "width: 100%;");
                    browser.NavigateToString(htmlContent);
                    //browser.NavigateToString(html.Content);
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion


    }
}

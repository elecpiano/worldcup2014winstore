using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class SubjectPage : Page
    {
        private string subjectID = string.Empty;

        #region Lifecycle

        public SubjectPage()
        {
            InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
            if (param != null)
            {
                subjectID = param[NaviParam.SUBJECT_ID];
            }

            pageTitle.Show("热门推荐");
            Loadsubject();
        }

        #endregion

        #region data

        DataLoader<Subject> subjectLoader = new DataLoader<Subject>();

        private void Loadsubject()
        {
            if (subjectLoader.Busy)
            {
                return;
            }

            //busy
            progressbar.Visibility = Visibility.Visible;

            //load
            subjectLoader.Load("getsubject", "&id=" + subjectID, true, Constants.SUBJECT_MODULE, string.Format(Constants.SUBJECT_FILE_NAME_FORMAT, subjectID),
                result =>
                {
                    foreach (var focus in result.FocusList)
                    {
                        Image img = new Image() { Stretch = Stretch.Uniform, VerticalAlignment = VerticalAlignment.Top };
                        img.Source = new BitmapImage(new Uri(focus.Image, UriKind.RelativeOrAbsolute));
                        focusSlideShow.Items.Add(img);

                        backgroundImage.Source = new BitmapImage(new Uri(result.FocusList[0].Image, UriKind.RelativeOrAbsolute));
                    }

                    scrollViewer.ChangeView(0, null, null);

                    List<NewsGroup> newsGroups = new List<NewsGroup>();
                    foreach (var group in result.NewsGroups)
                    {
                        if (group.NewsList != null && group.NewsList.Length > 0)
                        {
                            newsGroups.Add(group);
                        }
                    }
                    newsGroupListBox.ItemsSource = newsGroups;

                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion

        private void news_ItemClick(object sender, ItemClickEventArgs e)
        {
            News news = e.ClickedItem as News;
            if (news != null)
            {
                NewsHandler.OnNewsTap(this, news);
            }
        }

    }
}

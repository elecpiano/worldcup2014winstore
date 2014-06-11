using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class DiaryListPage : Page
    {
        #region Property

        private string authorId = string.Empty;
        private string authorName = string.Empty;
        private string authorFace = string.Empty;

        #endregion

        #region Lifecycle

        public DiaryListPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
                if (param != null)
                {
                    authorId = param[NaviParam.AUTHOR_ID];
                    authorName = param[NaviParam.AUTHOR_NAME];
                    authorFace = param[NaviParam.AUTHOR_FACE];
                }

                //nameTextBlock.Text = authorName;
                faceImage.Source = new BitmapImage(new Uri(authorFace, UriKind.RelativeOrAbsolute));

                pageTitle.Show(authorName);

                LoadData();
            }
        }

        #endregion

        #region Data

        GenericDataLoader<DiaryList> dataLoader = new GenericDataLoader<DiaryList>();

        private void LoadData()
        {
            if (dataLoader.Loaded || dataLoader.Busy)
            {
                return;
            }

            //busy
            progressbar.Visibility = Visibility.Visible;

            //load
            dataLoader.Load("getdiarylist", "&id=" + authorId, true, Constants.DIARY_MODULE, string.Format(Constants.DIARY_LIST_FILE_NAME_FORMAT, authorId),
                result =>
                {
                    coverImage.Source = new BitmapImage(new Uri(result.BigImage, UriKind.RelativeOrAbsolute));
                    diaryListBox.ItemsSource = result.data;
                    //not busy
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion

        private void diary_ItemClick(object sender, ItemClickEventArgs e)
        {
            DiaryItem item = e.ClickedItem as DiaryItem;
            if (item != null)
            {
                NewsHandler.OnNewsTap(this, item.ID, item.Name, item.Image);
            }
        }
    }
}

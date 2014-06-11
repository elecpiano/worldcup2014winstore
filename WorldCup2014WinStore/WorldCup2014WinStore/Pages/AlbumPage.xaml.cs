using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class AlbumPage : Page
    {
        #region Property

        private string albumID = string.Empty;

        private const string FILE_NAME_PREFIX = "CCTV5_";

        #endregion

        #region Lifecycle

        public AlbumPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
            listBox.ItemsSource = albumItems;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
            if (param != null)
            {
                albumID = param[NaviParam.ALBUM_ID];
            }

            pageTitle.Show("看大图");
            LoadAlbumData(albumID);
        }

        #endregion

        #region Data

        DataLoader<WorldCup2014WinStore.Models.Album> albumloader = new DataLoader<WorldCup2014WinStore.Models.Album>();
        ObservableCollection<AlbumItem> albumItems = new ObservableCollection<AlbumItem>();
        //ImageHelper imageHelper = new ImageHelper();

        private void LoadAlbumData(string id)
        {
            if (albumloader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            albumloader.Load("getalbum", "&id=" + id, true, Constants.ALBUM_MODULE, string.Format(Constants.ALBUM_FILE_NAME_FORMAT, id),
                album =>
                {
                    albumItems.Clear();
                    int index = 0;
                    foreach (var item in album.Items)
                    {
                        albumItems.Add(item);
                        if (index==0)
                        {
                            bigImagePanel.DataContext = item;
                        }
                        index++;
                    }
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion

        private void imageList_ItemClick(object sender, ItemClickEventArgs e)
        {
            AlbumItem item = e.ClickedItem as AlbumItem;
            if (item!=null)
            {
                bigImagePanel.DataContext = item;
            }
        }
    }
}

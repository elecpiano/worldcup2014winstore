using System;
using System.Collections.ObjectModel;
using Utility.Animations;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class AlbumPage : PageBase
    {
        public AlbumPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PageTitle.AttachAndShow(this.pageTitlePanel, "Cool Photos");
            FadeAnimation.Fade(this.contentPanel, 0d, 1d, Constants.DURATION_CONTENT_FADING, null);
            LoadData();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SpinningBall.Detach();
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

        public override void OnBack()
        {
            PageTitle.Hide();
            FadeAnimation.Fade(this.contentPanel, 1d, 0d, Constants.DURATION_CONTENT_FADING, fe =>
                {
                    base.OnBack();
                });
        }

        #region data

        private ObservableCollection<string> photos = new ObservableCollection<string>();
        private void LoadData()
        {
            //itemsControlPhotos.ItemsSource = photos;
            itemsControlPhotos.ItemLoaded += itemsControlPhotos_ItemLoaded;
            photos.Clear();
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");
            photos.Add("1");

            itemsControlPhotos.ShowItems(photos);
        }

        void itemsControlPhotos_ItemLoaded(Windows.UI.Xaml.FrameworkElement visual, object item)
        {
            FadeAnimation.Fade(visual, 0, 1, TimeSpan.FromSeconds(0.5), null);
        }

        #endregion

    }
}

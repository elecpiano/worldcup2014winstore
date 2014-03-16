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
            photos.Add("2");
            photos.Add("3");
            photos.Add("4");
            photos.Add("5");
            photos.Add("6");
            photos.Add("7");
            photos.Add("8");
            photos.Add("9");
            photos.Add("10");
            photos.Add("11");
            photos.Add("12");
            photos.Add("13");
            photos.Add("14");
            photos.Add("15");
            photos.Add("16");
            photos.Add("17");
            photos.Add("18");
            photos.Add("19");
            photos.Add("20");
            photos.Add("21");
            photos.Add("22");
            photos.Add("23");
            photos.Add("24");
            photos.Add("25");

            itemsControlPhotos.ShowItems(photos);
        }

        void itemsControlPhotos_ItemLoaded(Windows.UI.Xaml.FrameworkElement visual, object item)
        {
            //ScaleAnimation.ScaleFromTo(visual, 0.5d, 0.5d, 1d, 1d, Constants.DURATION_LIST_ITEM_TRANSITION, null);
            FadeAnimation.Fade(visual, 0d, 1d, Constants.DURATION_LIST_ITEM_TRANSITION, null);
        }

        #endregion

    }
}

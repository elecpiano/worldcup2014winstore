using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class PageTitle : UserControl
    {
        #region Singleton

        private static PageTitle _Current;
        public static PageTitle Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new PageTitle();
                }
                return _Current;
            }
        }
        public static Action OnBack = null;

        #endregion

        private Action onPageTitleShown;
        private Action onPageTitleHidden;

        public PageTitle()
        {
            this.InitializeComponent();
        }

        private void InstanceShowPageTitle(string pageTitle, Action completed)
        {
            onPageTitleShown = completed;
            this.pageTitleTextBlock.Text = pageTitle;
            this.StoryShowPageTile.Begin();
        }

        private void InstanceHidePageTitle(Action completed)
        {
            onPageTitleHidden = completed;
            this.StoryHidePageTile.Begin();
        }

        private void StoryShowPageTile_Completed(object sender, object e)
        {
            if (onPageTitleShown != null)
            {
                onPageTitleShown();
                onPageTitleShown = null;
            }
        }

        private void StoryHidePageTile_Completed(object sender, object e)
        {
            if (onPageTitleHidden != null)
            {
                onPageTitleHidden();
                onPageTitleHidden = null;
            }
        }

        #region Static Methods

        public static void Detach()
        {
            if (PageTitle.Current.Parent != null)
            {
                ((Grid)PageTitle.Current.Parent).Children.Remove(PageTitle.Current);
            }
        }

        public static void AttachAndShow(Grid titlePanel, string pageTitle, Action completed = null)
        {
            titlePanel.Children.Add(PageTitle.Current);
            PageTitle.Show(pageTitle, completed);
        }

        public static void Show(string pageTitle, Action completed = null)
        {
            PageTitle.Current.InstanceShowPageTitle(pageTitle, completed);
        }

        public static void Hide(Action completed = null)
        {
            PageTitle.Current.InstanceHidePageTitle(completed);
        }

        #endregion

        private void titleBall_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (OnBack != null)
            {
                OnBack();
                OnBack = null;
            }
            else
            {
                PageTitle.Hide(() =>
                    {
                        TryGoBack();
                    });
            }
        }

        public static void TryGoBack()
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


    }
}

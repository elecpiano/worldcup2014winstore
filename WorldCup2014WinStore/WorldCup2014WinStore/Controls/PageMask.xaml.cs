using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class PageMask : UserControl
    {
        #region Singleton

        private static PageMask _Current;
        public static PageMask Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new PageMask();
                }
                return _Current;
            }
        }

        #endregion

        private Action onClosed;
        private Action onOpened;
        
        private bool isOpen = false;
        private bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set
            {
                if (isOpen != value)
                {
                    isOpen = value;
                    double maskOpacity = isOpen ? 0d : 1d;
                    this.restOfMask.Opacity = maskOpacity;
                    this.spinningBall.Opacity = maskOpacity;
                    this.ring0.Opacity = maskOpacity;
                    this.ring1.Opacity = maskOpacity;
                    this.ring2.Opacity = maskOpacity;
                    this.ring3.Opacity = maskOpacity;
                    this.ring4.Opacity = maskOpacity;
                    this.ring5.Opacity = maskOpacity;
                    this.ring6.Opacity = maskOpacity;
                    this.fillup.Opacity = maskOpacity;
                }
            }
        }

        public PageMask()
        {
            this.InitializeComponent();
        }

        private void InstantOpen(Action completed)
        {
            onOpened = completed;
            DetectPageTitleOffset();
            this.StoryOpen.Begin();
        }

        private void InstantClose(Action completed)
        {
            onClosed = completed;
            this.StoryClose.Begin();
        }

        private void StoryOpen_Completed(object sender, object e)
        {
            if (onOpened != null)
            {
                onOpened();
                onOpened = null;
            }
        }

        private void StoryClose_Completed(object sender, object e)
        {
            if (onClosed != null)
            {
                onClosed();
                onClosed = null;
            }
        }

        #region Page Title Ball Positioning

        private void DetectPageTitleOffset()
        {
            GeneralTransform transform = titleBallPlaceholder.TransformToVisual(this.rootGrid);
            Point pageTitleBallCenterPoint = transform.TransformPoint(new Point(30, 30));
            double distX = pageTitleBallCenterPoint.X - Window.Current.Bounds.Width * 0.5d;
            double distY = pageTitleBallCenterPoint.Y - Window.Current.Bounds.Height * 0.5d;

            keyFrameBallFlyX.Value = distX;
            keyFrameBallFlyY.Value = distY;
        }

        #endregion

        #region Static Methods

        public static void Open(Action completed = null)
        {
            PageMask.Current.InstantOpen(completed);
        }

        public static void Close(Action completed = null)
        {
            PageMask.Current.InstantClose(completed);
        }

        public static void Detach()
        {
            if (PageMask.Current.Parent != null)
            {
                ((Grid)PageMask.Current.Parent).Children.Remove(PageMask.Current);
            }
        }

        public static void Attach(Grid maskPanel)
        {
            maskPanel.Children.Add(PageMask.Current);
        }

        public static void AttachAndOpen(Grid maskPanel, Action completed = null)
        {
            maskPanel.Children.Add(PageMask.Current);
            PageMask.Open(completed);
        }

        #endregion

        private void titleBallPlaceholder_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
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

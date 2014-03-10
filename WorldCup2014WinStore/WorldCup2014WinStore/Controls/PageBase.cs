using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Utility.Animations;

namespace WorldCup2014WinStore.Controls
{
    public class PageBase : Page
    {
        #region Properties

        public double ScreenWidth
        {
            get
            {
                return Window.Current.Bounds.Width;
            }
        }

        public double ScreenHeight
        {
            get
            {
                return Window.Current.Bounds.Height;
            }
        }

        protected FrameworkElement ContentPanel = null;

        #endregion

        #region AppBar

        public void HideAppBars()
        {
            if (this.TopAppBar != null)
            {
                this.TopAppBar.IsOpen = false;
            }
            if (this.BottomAppBar != null)
            {
                this.BottomAppBar.IsOpen = false;
            }
        }

        #endregion

        #region Lifecycle

        public virtual void OnBack()
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

        #endregion

        #region Navigation

        protected void Navigate(Type page, object param = null)
        {
            this.Frame.Navigate(page, param);
        }

        public void ContentFadeIn()
        {
            if (ContentPanel == null)
            {
                return;
            }
            FadeAnimation.Fade(ContentPanel, 0d, 1d, Constants.DURATION_CONTENT_FADING,
                            fe =>
                            {
                            });
        }

        public void ContentFadeOut(Action completed = null)
        {
            if (ContentPanel == null)
            {
                return;
            }
            FadeAnimation.Fade(ContentPanel, 1d, 0d, Constants.DURATION_CONTENT_FADING,
                            fe =>
                            {
                                if (completed!=null)
                                {
                                    completed();
                                }
                            });
        }

        #endregion
    }
}

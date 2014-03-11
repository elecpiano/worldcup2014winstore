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

    }
}

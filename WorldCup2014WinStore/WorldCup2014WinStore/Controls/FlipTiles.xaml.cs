using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class FlipTiles : UserControl
    {
        public FlipTiles()
        {
            this.InitializeComponent();
        }

        public void Expand(int type = 1)
        {
            if (type == 0)
            {
                this.StoryExpand0.Begin();
            }
            else if (type == 1)
            {
                this.StoryExpand.Begin();
            }
            else if (type == 2)
            {
                this.StoryExpand2.Begin();
            }
            else if (type == 3)
            {
                this.StoryExpand3.Begin();
            }
        }

        #region Events

        public event TappedEventHandler ItemTapped;

        private void tile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ItemTapped!=null)
            {
                ItemTapped((sender as FrameworkElement).Name, e);
            }
        }

        #endregion

    }

}

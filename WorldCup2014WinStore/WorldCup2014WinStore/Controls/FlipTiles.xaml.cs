using Windows.UI.Xaml.Controls;

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
        }
    }

}

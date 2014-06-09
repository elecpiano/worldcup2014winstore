using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class AuthorItem : UserControl
    {
        public AuthorItem()
        {
            this.InitializeComponent();
        }

        public void SetImageListVisibility(bool hasImageList)
        {
            imageListBox.Visibility = hasImageList ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class LivePageItemLiveText : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemLiveText()
        {
            this.InitializeComponent();
        }

    }
}

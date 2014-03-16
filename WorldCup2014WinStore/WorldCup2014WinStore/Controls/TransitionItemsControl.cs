using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WorldCup2014WinStore.Controls
{
    public delegate void TransitionItemsControlItemLoaded(FrameworkElement visual, object item);
    public class TransitionItemsControl : ItemsControl
    {
        #region Override

        protected override Windows.UI.Xaml.DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }

        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (ItemLoaded != null)
            {
                ContentPresenter container = element as ContentPresenter;
                itemToContainer.Add(item, container);
                if (container != null)
                {
                    container.Opacity = 0;
                }
                if (!timerStarted)
                {
                    StartTimer();
                }
            }
        }

        #endregion

        #region Items

        private ObservableCollection<object> _items;
        private Dictionary<object, FrameworkElement> _itemToContainer;
        private Dictionary<object, FrameworkElement> itemToContainer
        {
            get
            {
                if (_itemToContainer == null)
                {
                    _itemToContainer = new Dictionary<object, FrameworkElement>();
                }
                return _itemToContainer;
            }
        }

        private DispatcherTimer timer = null;
        private bool timerStarted = false;
        private int itemCount = 0;
        private int timerIndex = 0;

        public void ShowItems<T>(ObservableCollection<T> items)
        {
            _items = items;
            itemToContainer.Clear();

            itemCount = items.Count();
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(50);
                timer.Tick += timer_Tick;
            }

            this.ItemsSource = items;
        }

        private void StartTimer()
        {
            timerStarted = true;
            timer.Start();
        }

        private void timer_Tick(object sender, object e)
        {
            object item = _items[timerIndex];
            ItemLoaded(itemToContainer[item],item);
            timerIndex++;
            if (timerIndex>=itemCount)
            {
                timer.Stop();
            }
        }

        public void HideItems()
        {

        }

        #endregion

        #region Event

        public event TransitionItemsControlItemLoaded ItemLoaded;

        #endregion
    }
}

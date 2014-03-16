using System;
using System.Collections;
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
                int index = _items.IndexOf(item);
                Containers.Insert(index, container);
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

        private IList _items;
        private List<FrameworkElement> _containers;
        private List<FrameworkElement> Containers
        {
            get
            {
                if (_containers == null)
                {
                    _containers = new List<FrameworkElement>();
                }
                return _containers;
            }
        }

        private DispatcherTimer timer = null;
        private bool timerStarted = false;
        private int timerIndex = 0;

        public void ShowItems(IList items)
        {
            _items = items;
            Containers.Clear();

            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(30);
                timer.Tick += timer_Tick;
            }

            this.ItemsSource = items;
        }

        private void StartTimer()
        {
            timerStarted = true;
            timerIndex = 0;
            timer.Start();
        }

        private void timer_Tick(object sender, object e)
        {
            object item = _items[timerIndex];
            int index = _items.IndexOf(item);
            ItemLoaded(Containers[index], item);
            timerIndex++;
            if (timerIndex >= _items.Count)
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

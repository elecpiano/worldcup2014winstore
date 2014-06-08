using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Collections;

namespace WorldCup2014WinStore.Models
{
    public class SubscriptionDataContext
    {
        #region Singleton

        private static SubscriptionDataContext instance = new SubscriptionDataContext();

        static SubscriptionDataContext()
        {
            //for thread-safe
        }

        private SubscriptionDataContext()
        {
        }

        public static SubscriptionDataContext Current
        {
            get { return instance; }
        }

        #endregion

        IPropertySet _Settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;

        public List<EPG> LoadSubscriptions()
        {
            List<EPG> list = null;
            if (_Settings.ContainsKey(Constants.KEY_SUBSCRIPTION_LIST))
            {
                list = _Settings[Constants.KEY_SUBSCRIPTION_LIST] as List<EPG>;
            }
            if (list == null)
            {
                list = new List<EPG>();
            }
            return list;
        }

        //public void AddSubscription(EPG item)
        //{
        //    List<EPG> list = LoadSubscriptions();
        //    var duplication = list.FirstOrDefault(x => x.ID == item.ID);
        //    if (duplication == null)
        //    {
        //        list.Add(item);
        //        SaveSubscriptions(list);
        //    }
        //}

        public void AddSubscription(EPG item)
        {
            List<EPG> list = LoadSubscriptions();
            var duplication = list.FirstOrDefault(x => x.ID == item.ID);
            if (duplication == null)
            {
                list.Add(item);
                SaveSubscriptions(list);
            }
        }

        public void RemoveSubscription(string id)
        {
            List<EPG> list = LoadSubscriptions();
            var item = list.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                list.Remove(item);
                SaveSubscriptions(list);
            }
        }

        public void SaveSubscriptions(List<EPG> list)
        {
            // txtInput is a TextBox defined in XAML.
            if (_Settings.ContainsKey(Constants.KEY_SUBSCRIPTION_LIST))
            {
                _Settings[Constants.KEY_SUBSCRIPTION_LIST] = list;
            }
            else
            {
                _Settings.Add(Constants.KEY_SUBSCRIPTION_LIST, list);
            }
        }

        public void ClearSubscriptions()
        {
            if (_Settings.ContainsKey(Constants.KEY_SUBSCRIPTION_LIST))
            {
                _Settings[Constants.KEY_SUBSCRIPTION_LIST] = new List<EPG>();
            }
        }


    }
}

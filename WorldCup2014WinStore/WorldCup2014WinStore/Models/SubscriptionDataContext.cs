using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using WorldCup2014WinStore.Utility;

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

        public List<EPG> LoadSubscriptions2()
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
        public async Task<List<EPG>> LoadSubscriptions()
        {
            var cachedJson = await IsolatedStorageHelper.ReadFile(Constants.SUBSCRIPTION_MODULE, Constants.SUBSCRIPTION_FILE_NAME);
            List<EPG> result = JsonSerializer.Deserialize<List<EPG>>(cachedJson);
            if (result==null)
            {
                result = new List<EPG>();
            }
            return result;
        }

        public async void AddSubscription2(EPG item)
        {
            List<EPG> list = await LoadSubscriptions();
            var duplication = list.FirstOrDefault(x => x.ID == item.ID);
            if (duplication == null)
            {
                list.Add(item);
                SaveSubscriptions(list);
            }
        }

        public async void AddSubscription(EPG item)
        {
            List<EPG> list = await LoadSubscriptions();
            var duplication = list.FirstOrDefault(x => x.ID == item.ID);
            if (duplication == null)
            {
                list.Add(item);
                SaveSubscriptions(list);
            }
        }

        public async void RemoveSubscription(string id)
        {
            List<EPG> list = await LoadSubscriptions();
            var item = list.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                list.Remove(item);
                SaveSubscriptions(list);
            }
        }

        public void SaveSubscriptions2(List<EPG> list)
        {
            // txtInput is a TextBox defined in XAML.
            if (_Settings.ContainsKey(Constants.KEY_SUBSCRIPTION_LIST))
            {
                _Settings[Constants.KEY_SUBSCRIPTION_LIST] = list;
            }
            else
            {
                //_Settings.Add(Constants.KEY_SUBSCRIPTION_LIST, list);
                _Settings[Constants.KEY_SUBSCRIPTION_LIST] = list;
            }
        }

        public async void SaveSubscriptions(List<EPG> list)
        {
            string json = JsonSerializer.Serialize(list);
            await IsolatedStorageHelper.WriteToFile(Constants.SUBSCRIPTION_MODULE, Constants.SUBSCRIPTION_FILE_NAME, json);
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

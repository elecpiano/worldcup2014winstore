using Windows.ApplicationModel.Core;
using Windows.Foundation.Collections;
using Windows.UI.Core;

namespace WorldCup2014WinStore
{
    public partial class App
    {
        IPropertySet _Settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;

        #region Load & Update
        
        private void LoadSettings()
        {
            //dismissed banner
            if (_Settings.ContainsKey(KEY_DISMISSED_BANNER))
            {
                _DismissedBannerId = (string)_Settings[KEY_DISMISSED_BANNER];
            }
            else
            {
                _Settings.Add(KEY_DISMISSED_BANNER, string.Empty);
            }
        }

        private void UpdateSetting(string key, object value)
        {
            _Settings[key] = value;
        }

        #endregion

        #region Banner

        private const string KEY_DISMISSED_BANNER = "banner";

        private string _DismissedBannerId = string.Empty;
        public string DismissedBannerId
        {
            get
            {
                return _DismissedBannerId;
            }
            set
            {
                if (_DismissedBannerId != value)
                {
                    _DismissedBannerId = value;
                    UpdateSetting(KEY_DISMISSED_BANNER, value);
                }
            }
        }

        #endregion

        #region Toast Notification

        //public CuteToast Toast { get; set; }

        public void ShowToastMessage(string message)
        {
            //if (Toast!=null)
            //{
            //    Toast.ShowMessage(message);
            //}
        }

        #endregion

        #region Dispatcher

        public CoreDispatcher Dispatcher
        {
            get
            {
                return CoreApplication.MainView.CoreWindow.Dispatcher;
            }
        }

        #endregion
    }
}

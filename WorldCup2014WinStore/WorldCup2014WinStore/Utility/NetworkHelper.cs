using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace WorldCup2014WinStore.Utility
{
    public class NetworkHelper
    {
        public static bool IsNetworkAvailable
        {
            get
            {
                return GetIsNetworkAvailable();
            }
        }

        private static bool GetIsNetworkAvailable(NetworkConnectivityLevel minimumLevelRequired = NetworkConnectivityLevel.InternetAccess)
        {
            ConnectionProfile profile =
                 NetworkInformation.GetInternetConnectionProfile();

            NetworkConnectivityLevel level =
                profile.GetNetworkConnectivityLevel();

            return level >= minimumLevelRequired;
        }
    }
}

using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Models
{
    [DataContract]
    public class CalendarItem
    {
        [DataMember(Name = "date")]
        public string DateStr { get; set; }

        [IgnoreDataMember]
        public DateTime Date
        {
            get
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(DateStr, out dt))
                {
                    return dt;
                }
                return dt;
            }
            set
            {
                DateStr = value.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        [DataMember(Name = "football")]
        public string Football { get; set; }

        [IgnoreDataMember]
        public Visibility HasGame
        {
            get
            {
                return Football == "0" ? Visibility.Collapsed : Visibility.Visible;
            }
        }

    }
}

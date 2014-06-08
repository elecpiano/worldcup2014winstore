using System;
using System.Runtime.Serialization;

namespace WorldCup2014WinStore.Models
{
    [DataContract]
    public class Focus
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "time")]
        public string TimeStr { get; set; }

        [IgnoreDataMember]
        public DateTime Time
        {
            get
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(TimeStr, out dt))
                {
                    return dt;
                }
                return dt;
            }
        }
    }

    [DataContract]
    public class Recommendation
    {
        [DataMember]
        public Focus[] focus { get; set; }

        [DataMember]
        public News[] data { get; set; }
    }
}

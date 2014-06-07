using System;
using System.Runtime.Serialization;

namespace WorldCup2014WinStore.Models
{
    [DataContract]
    public class News
    {
        [DataMember(Name="id")]
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
        public bool IsMoreButton { get; set; }

        [IgnoreDataMember]
        public bool IsDateHeader { get; set; }

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

        [IgnoreDataMember]
        public DateTime HeaderDate { get; set; }
    }

    [DataContract]
    public class NewsList
    {
        [DataMember]
        public News[] data { get; set; }

        [DataMember(Name = "total")]
        public int TotalPageCount { get; set; }

        [DataMember(Name = "page")]
        public int CurrentPageIndex { get; set; }

    }
}

using System.Runtime.Serialization;

namespace WorldCup2014WinStore.Models
{
    [DataContract]
    public class StatisticsAddress
    {
        [DataMember(Name = "wcdata")]
        public string URL { get; set; }
    }
}

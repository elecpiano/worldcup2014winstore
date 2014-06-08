using System;
using System.Runtime.Serialization;

namespace WorldCup2014WinStore.Models
{
    [DataContract]
    public class Splash
    {
        [DataMember(Name = "img")]
        public string Image { get; set; }
    }
}

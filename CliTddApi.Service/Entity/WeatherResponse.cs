using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTddApi.Service.Entity
{
    public class WeatherResponse
    {
        public int queryCost { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string resolvedAddress { get; set; }
        public string address { get; set; }
        public string timezone { get; set; }
        public float tzoffset { get; set; }
        public string description { get; set; }
        public Day[] days { get; set; }
        public object[] alerts { get; set; }
        public Stations stations { get; set; }
        public Currentconditions currentConditions { get; set; }
    }
}

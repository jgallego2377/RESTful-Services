using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace uvInfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "uvService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select uvService.svc or uvService.svc.cs at the Solution Explorer and start debugging.
    public class uvService : IuvService
    {
        public double uvIndex(string zipcode)
        {
            double uvTotal = 0, uvAverage;

            // *** api key removed from github due to privacy reasons
            // api key would go at the end of the url link***
            string url = @"https://api.weatherbit.io/v2.0/forecast/daily?postal_code=" + zipcode + "&country=US&key=";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);

            string responseReader = sreader.ReadToEnd();

            Info info = JsonConvert.DeserializeObject<Info>(responseReader);

            for (int i = 0; i < info.data.Count; i++)
                uvTotal = uvTotal + info.data[i].uv;

            uvAverage = uvTotal / info.data.Count;

            return uvAverage;
        }
    }

    public class Info
    {
        public List<Data> data { get; set; }
        public string city_name { get; set; }
        public string lon { get; set; }
        public string timezone { get; set; }
        public string lat { get; set; }
        public string country_code { get; set; }
        public string state_code { get; set; }
    }

    public class Data
    {
        public string valid_date { get; set; }
        public string ts { get; set; }
        public string datetime { get; set; }
        public double wind_gust_spd { get; set; }
        public double wind_spd { get; set; }
        public int wind_dir { get; set; }
        public string wind_cdir { get; set; }
        public string wind_cdir_full { get; set; }
        public double temp { get; set; }
        public double max_temp { get; set; }
        public double min_temp { get; set; }
        public double high_temp { get; set; }
        public double low_temp { get; set; }
        public double app_max_temp { get; set; }
        public double app_min_temp { get; set; }
        public double pop { get; set; }
        public double precip { get; set; }
        public double snow { get; set; }
        public double snow_depth { get; set; }
        public double slp { get; set; }
        public double pres { get; set; }
        public double dewpt { get; set; }
        public double rh { get; set; }
        public List<Weather> weatherInfo { get; set; }
        public string pod { get; set; }
        public double clouds_low { get; set; }
        public double clouds_mid { get; set; }
        public double clouds_hi { get; set; }
        public double clouds { get; set; }
        public string vis { get; set; }
        public double? max_dhi { get; set; }
        public double uv { get; set; }
        public double moon_phase { get; set; }
        public string moonrise_ts { get; set; }
        public string moonset_ts { get; set; }
        public string sunrise_ts { get; set; }
        public string sunset_ts { get; set; }
    }

    public class Weather
    {
        public string icon { get; set; }
        public string code { get; set; }
        public string description { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IstocksService" in both code and config file together.
    [ServiceContract]
    public interface IstocksService
    {
        // method allows the user to see averages of their specified company's stock
        // price for a certain 7 - 21 day period 
        [OperationContract]
        [WebGet(UriTemplate = "stockinfo/{symbol}/{days}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<StockReturn> stockinfo(string symbol, string days);
    }

    // basic structure of our JSON response
    public class StockReturn
    {
        public string name { get; set; }
        public string avg_open { get; set; }
        public string avg_close { get; set; }
        public string avg_high { get; set; }
        public string avg_low { get; set; }
    }
}

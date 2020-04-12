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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStockInfoService" in both code and config file together.
    [ServiceContract]
    public interface IStockInfoService
    {
        [OperationContract]
        [WebGet(UriTemplate = "stockinfo/{symbol}/{days}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<StockReturn> stockinfo(string symbol, string days);
    }

    public class StockReturn
    {
        public string name { get; set; }
        public string avg_open { get; set; }
        public string avg_close { get; set; }
        public string avg_high { get; set; }
        public string avg_low { get; set; }
    }
}

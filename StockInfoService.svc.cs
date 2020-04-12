using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StockInfoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StockInfoService.svc or StockInfoService.svc.cs at the Solution Explorer and start debugging.
    public class StockInfoService : IStockInfoService
    {
        public List<StockReturn> stockinfo(string symbol, string days)
        {
            // url is the one to use when the service is deployed
            // dummy url is a test url

            string url = @"https://cloud.iexapis.com/stable/stock/" + symbol + "/chart/1m?token=pk_9a8773f364654b7baa1c5c4ade676a3a";
            //string dummyUrl = @"https://sandbox.iexapis.com/stable/stock/" + symbol + "/chart/1m?token=Tsk_6fb5a6b8c4c44cdc8bcf370f6b7d6a2b";

            // Gets a JSON response from the URL above
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);

            string responsereader = sreader.ReadToEnd();
            response.Close();

            // Since the JSON response is an array of objects, it gets deserialized into a List of RootObjects
            List<RootObject> stockInfo = JsonConvert.DeserializeObject<List<RootObject>>(responsereader);

            // variables to calculate the averages of the information the service is providing
            double closeTotal = 0, closeAvg;
            double highTotal = 0, highAvg;
            double lowTotal = 0, lowAvg;
            double openTotal = 0, openAvg;

            // for loop executes according to how many days the user wants to use to find their data
            for (int i = 0; i < Int32.Parse(days); i++)
            {
                // adds the prices at a certain date to the overall price
                // since the JSON response gives the oldest dates first, we must 
                // traverse the List backwards, which is why we have count - (i + 1) 
                // as the index 
                closeTotal += (stockInfo[stockInfo.Count - (i + 1)].close);
                highTotal += (stockInfo[stockInfo.Count - (i + 1)].high);
                lowTotal += (stockInfo[stockInfo.Count - (i + 1)].low);
                openTotal += (stockInfo[stockInfo.Count - (i + 1)].open);
            }

            // calculates the averages
            closeAvg = closeTotal / Int32.Parse(days);
            highAvg = highTotal / Int32.Parse(days);
            lowAvg = lowTotal / Int32.Parse(days);
            openAvg = openTotal / Int32.Parse(days);

            // creates a new StockReturn object and StockReturn List
            // which will help us create our JSON response
            StockReturn objStockReturn = new StockReturn();
            List<StockReturn> stockData = new List<StockReturn>();

            // initializes all the values of the object
            objStockReturn.name = symbol;
            objStockReturn.avg_close = closeAvg.ToString();
            objStockReturn.avg_high = highAvg.ToString();
            objStockReturn.avg_low = lowAvg.ToString();
            objStockReturn.avg_open = openAvg.ToString();
            // adds the object to the list
            stockData.Add(objStockReturn);

            // returns the list to be our JSON response from this service
            return stockData;
        }
    }

    // The Root Object which also contains all the information belonging to a specified company's stock
    public class RootObject
    {
        public string date { get; set; }
        public double open { get; set; }
        public double close { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public int volume { get; set; }
        public double uOpen { get; set; }
        public double uClose { get; set; }
        public double uHigh { get; set; }
        public double uLow { get; set; }
        public int uVolume { get; set; }
        public double change { get; set; }
        public double changePercent { get; set; }
        public string label { get; set; }
        public double changeOverTime { get; set; }
    }
}

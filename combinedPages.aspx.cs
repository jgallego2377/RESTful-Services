using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace serviceClient
{
    public partial class combinedTryItPages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string symbol = stockSymbol_textBox.Text;
            string days = stockTimeFrame_textBox.Text;

            // The url uses two parameters, symbol and days which are from the user's input
            //  string url = @"http://localhost:56210/StockInfoService.svc/stockinfo/" + symbol + "/" + days;

            // url using the deployed web service on the server
            string url = @"http://webstrar29.fulton.asu.edu/page8/stocksService.svc/stockinfo/" + symbol + "/" + days;

            // Gets the JSON response
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string responsereader = reader.ReadToEnd();
            response.Close();

            // Deserializes the JSON response obtained from the URL call
            RootObject info = JsonConvert.DeserializeObject<RootObject>(responsereader);

            // Creates double-type variables to make formatting easier and simplify code a bit
            double avgClose = Convert.ToDouble(info.stockinfoResult[0].avg_close);
            double avgHigh = Convert.ToDouble(info.stockinfoResult[0].avg_high);
            double avgLow = Convert.ToDouble(info.stockinfoResult[0].avg_low);
            double avgOpen = Convert.ToDouble(info.stockinfoResult[0].avg_open);

            // Displays all the necessary information
            stockAvgClose_label.Text = "The average close price for " + symbol.ToUpper() + " in the last " + days + " days was: " + "$" + avgClose.ToString("0.00");
            stockAvgHigh_label.Text = "The average high price for " + symbol.ToUpper() + " in the last " + days + " days was: " + "$" + avgHigh.ToString("0.00");
            stockAvgLow_label.Text = "The average low price for " + symbol.ToUpper() + " in the last " + days + " days was: " + "$" + avgLow.ToString("0.00");
            stockAvgOpen_label.Text = "The average open price for " + symbol.ToUpper() + " in the last " + days + " days was: " + "$" + avgOpen.ToString("0.00");
        }

        protected void domainNews_btn_Click(object sender, EventArgs e)
        {
            string domain = domainNews_textBox.SelectedValue;

            // localhost url & server url for the service call
            //string url = @"http://localhost:56210/News.svc/domainNews/" + domain;
            string url = @"http://webstrar29.fulton.asu.edu/page9/newsService.svc/domainNews/" + domain;

            // gets a JSON response from the service mentioned above
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string responsereader = reader.ReadToEnd();
            //Response.Close();

            // parses the JSON response into a DomainRootObject object instance
            DomainRootObject newsResult = JsonConvert.DeserializeObject<DomainRootObject>(responsereader);

            //Label1.Text = responsereader;

            // creates a variable for the forloop
            int count = newsResult.domainNewsResult.Count;
            // creates a list of labels 1 - 5 that is used to display the articles
            List<Label> labels = new List<Label>();
            labels.Add(articleLabel1);
            labels.Add(articleLabel2);
            labels.Add(articleLabel3);
            labels.Add(articleLabel4);
            labels.Add(articleLabel5);

            // clears all the text of the labels to get rid of previous displayed articles
            for (int i = 0; i < labels.Count; i++)
                labels[i].Text = "";

            for (int i = 0; i < count; i++)
            {
                // checks to see if there is an author credited to the article
                // if no author is found, displays the title with no author
                if (string.IsNullOrEmpty(newsResult.domainNewsResult[i].author))
                {
                    labels[i].Text = newsResult.domainNewsResult[i].title +
                        "<br>" + "URL: " + newsResult.domainNewsResult[i].url +
                        "<br>" + "Published at: " + newsResult.domainNewsResult[i].publishedAt + "<br>";
                }
                else
                {
                    labels[i].Text = newsResult.domainNewsResult[i].title + " by " + newsResult.domainNewsResult[i].author +
                        "<br>" + "URL: " + newsResult.domainNewsResult[i].url +
                        "<br>" + "Published at: " + newsResult.domainNewsResult[i].publishedAt + "<br>";
                }
            }

            // if the JSON response returns an empty list, we have no articles so 
            // we display this message to let the user know there were no articles
            if (count == 0)
                labels[0].Text = "Sorry, no articles could be found";
        }

        protected void topicNews_btn_Click(object sender, EventArgs e)
        {
            string topic = topicNews_textBox.Text;
            //Label1.Text = topic;

            // url for the JSON response from the service
            //string url = @"http://localhost:56210/News.svc/topicNews/" + topic;
            string url = @"http://webstrar29.fulton.asu.edu/page9/newsService.svc/topicNews/" + topic;

            // gets a JSON response from the url above
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string responsereader = reader.ReadToEnd();
            //Response.Close();

            // parses the JSON response into newsResult
            TopicRootObject newsResult = JsonConvert.DeserializeObject<TopicRootObject>(responsereader);

            //Label1.Text = responsereader;

            // count variable is used for the forloop 
            int count = newsResult.topicNewsResult.Count;
            // list of labels is used to display articles
            List<Label> labels = new List<Label>();
            labels.Add(articleLabel1);
            labels.Add(articleLabel2);
            labels.Add(articleLabel3);
            labels.Add(articleLabel4);
            labels.Add(articleLabel5);

            // resets any previous articles that were on the screen
            for (int i = 0; i < labels.Count; i++)
                labels[i].Text = "";

            // sets the values of all the labels to the appropriate article with the 
            // appropriate information
            for (int i = 0; i < count; i++)
            {
                if (string.IsNullOrEmpty(newsResult.topicNewsResult[i].author))
                {
                    labels[i].Text = newsResult.topicNewsResult[i].title +
                        "<br>" + "URL: " + newsResult.topicNewsResult[i].url +
                        "<br>" + "Published at: " + newsResult.topicNewsResult[i].publishedAt + "<br>";
                }
                else
                {
                    labels[i].Text = newsResult.topicNewsResult[i].title + " by " + newsResult.topicNewsResult[i].author +
                        "<br>" + "URL: " + newsResult.topicNewsResult[i].url +
                        "<br>" + "Published at: " + newsResult.topicNewsResult[i].publishedAt + "<br>";
                }
            }


            // displays a message if the JSON response returns no articles
            if (count == 0)
                labels[0].Text = "Sorry, no articles could be found";
        }

        protected void locationNews_btn_Click(object sender, EventArgs e)
        {
            string location = locationNews_textBox.Text;
            //Label1.Text = location;

            // server url to get the correct output for the articles
            //string url = @"http://localhost:56210/News.svc/locationNews/" + location;
            string url = @"http://webstrar29.fulton.asu.edu/page9/newsService.svc/locationNews/" + location;

            // gets a JSON response using the servive described above
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string responsereader = reader.ReadToEnd();
            //Response.Close();

            // parses the JSON response
            LocationRootObject newsResult = JsonConvert.DeserializeObject<LocationRootObject>(responsereader);

            //Label1.Text = responsereader;

            // count variable for the forloop
            int count = newsResult.locationNewsResult.Count;
            // list of labels to iterate through in a loop when displaying articles
            List<Label> labels = new List<Label>();
            labels.Add(articleLabel1);
            labels.Add(articleLabel2);
            labels.Add(articleLabel3);
            labels.Add(articleLabel4);
            labels.Add(articleLabel5);

            // resets any previous articles on the screen
            for (int i = 0; i < labels.Count; i++)
                labels[i].Text = "";

            // loop to display all the articles and their information
            for (int i = 0; i < count; i++)
            {
                if (string.IsNullOrEmpty(newsResult.locationNewsResult[i].author))
                {
                    labels[i].Text = newsResult.locationNewsResult[i].title +
                        "<br>" + "URL: " + newsResult.locationNewsResult[i].url +
                        "<br>" + "Published at: " + newsResult.locationNewsResult[i].publishedAt + "<br>";
                }
                else
                {
                    labels[i].Text = newsResult.locationNewsResult[i].title + " by " + newsResult.locationNewsResult[i].author +
                        "<br>" + "URL: " + newsResult.locationNewsResult[i].url +
                        "<br>" + "Published at: " + newsResult.locationNewsResult[i].publishedAt + "<br>";
                }
            }

            // displays a message when the JSON response returns no articles
            if (count == 0)
                labels[0].Text = "Sorry, no articles could be found";
        }

        // uv index button
        protected void Button1_Click(object sender, EventArgs e)
        {
            // gets the zipcode from the text box
            string zipcode = zipcode_textBox.Text;

            // the url to get the JSON response from 
            string url = @"http://webstrar29.fulton.asu.edu/page7/uvService.svc/uvIndex/" + zipcode;

            // gets the JSON response
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string responsereader = reader.ReadToEnd();

            // parses the JSON response
            UVRootObject uvIndex = JsonConvert.DeserializeObject<UVRootObject>(responsereader);

            // outputs the uv index and result depending on the uv number returned
            if (uvIndex.uvIndexResult > 0 && uvIndex.uvIndexResult <= 3)
                uvResult_label.Text = "The UV Index for " + zipcode + " is " + uvIndex.uvIndexResult.ToString("0.00") + ". This area is a poor place for solar energy.";
            else if (uvIndex.uvIndexResult > 3 && uvIndex.uvIndexResult <= 6)
                uvResult_label.Text = "The UV Index for " + zipcode + " is " + uvIndex.uvIndexResult.ToString("0.00") + ". This area is an ok place for solar energy.";
            else if (uvIndex.uvIndexResult > 6 && uvIndex.uvIndexResult <= 10)
                uvResult_label.Text = "The UV Index for " + zipcode + " is " + uvIndex.uvIndexResult.ToString("0.00") + ". This area is a good place for solar energy.";
        }
    }

    public class UVRootObject
    { 
        public double uvIndexResult { get; set; }
    }
}

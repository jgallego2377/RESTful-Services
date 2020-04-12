using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "News" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select News.svc or News.svc.cs at the Solution Explorer and start debugging.
    public class News : INews
    {
        public List<NewsArticle> domainNews(string domain)
        {
            //cnbc, bbc-news, cbs-news, fox-news, cnn, reuters, abc-news, nbc-news
            List<NewsArticle> objNewsData = new List<NewsArticle>();

            // api key removed from github due to privacy reasons
            // api key would go at the end of the url link
            string url = @"https://newsapi.org/v2/top-headlines?sources=" + domain + "&apiKey=";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);

            string responsereader = sreader.ReadToEnd();
            response.Close();

            TopicNewsRootObject newsInfo = JsonConvert.DeserializeObject<TopicNewsRootObject>(responsereader);

            int count = 0;
            if (newsInfo.articles.Count > 5)
                count = 5;
            else
                count = newsInfo.articles.Count;
            
            for (int i = 0; i < count; i++)
            {
                NewsArticle objNews = new NewsArticle();
                objNews.url = newsInfo.articles[i].url;
                objNews.author = newsInfo.articles[i].author;
                objNews.title = newsInfo.articles[i].title;
                objNews.publishedAt = newsInfo.articles[i].publishedAt;
                objNewsData.Add(objNews);
            }
            return objNewsData;
        }

        public List<NewsArticle> topicNews(string topic)
        {
            List<NewsArticle> objNewsData = new List<NewsArticle>();

            // api key removed from github due to privacy reasons
            // api key would go at the end of the url link
            string url = @"https://newsapi.org/v2/top-headlines?language=en&q=" + topic + "&apiKey=";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);

            string responsereader = sreader.ReadToEnd();
            response.Close();

            TopicNewsRootObject newsInfo = JsonConvert.DeserializeObject<TopicNewsRootObject>(responsereader);

            int count = 0;
            if (newsInfo.articles.Count > 5)
                count = 5;
            else
                count = newsInfo.articles.Count;

            for (int i = 0; i < count; i++)
            {
                NewsArticle objNews = new NewsArticle();
                objNews.url = newsInfo.articles[i].url;
                objNews.author = newsInfo.articles[i].author;
                objNews.title = newsInfo.articles[i].title;
                objNews.publishedAt = newsInfo.articles[i].publishedAt;
                objNewsData.Add(objNews);
            }
            return objNewsData;
        }

        public List<NewsArticle> locationNews(string location)
        {
            List<NewsArticle> objNewsData = new List<NewsArticle>();

            // api key removed from github due to privacy reasons
            // api key would go at the end of the url link
            string url = @"https://newsapi.org/v2/top-headlines?country=" + location + "&apiKey=";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);

            string responsereader = sreader.ReadToEnd();
            response.Close();

            TopicNewsRootObject newsInfo = JsonConvert.DeserializeObject<TopicNewsRootObject>(responsereader);


            int count = 0;
            if (newsInfo.articles.Count > 5)
                count = 5;
            else
                count = newsInfo.articles.Count;

            for (int i = 0; i < count; i++)
            {
                NewsArticle objNews = new NewsArticle();
                objNews.url = newsInfo.articles[i].url;
                objNews.author = newsInfo.articles[i].author;
                objNews.title = newsInfo.articles[i].title;
                objNews.publishedAt = newsInfo.articles[i].publishedAt;
                objNewsData.Add(objNews);
            }
            return objNewsData;
        }
    }

    public class TopicSource
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class TopicArticle
    {
        public TopicSource source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
    }

    public class TopicNewsRootObject
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<TopicArticle> articles{ get; set; }
    }
}

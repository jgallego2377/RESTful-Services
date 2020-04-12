using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INews" in both code and config file together.
    [ServiceContract]
    public interface INews
    {
        [OperationContract]
        [WebGet(UriTemplate = "/domainNews/{domain}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<NewsArticle> domainNews(string domain);

        [OperationContract]
        [WebGet(UriTemplate = "/topicNews/{topic}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<NewsArticle> topicNews(string topic);

        [OperationContract]
        [WebGet(UriTemplate = "/locationNews/{location}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<NewsArticle> locationNews(string location);
    }

    public class NewsArticle
    {
        public string url { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public DateTime? publishedAt { get; set; }
    }
}

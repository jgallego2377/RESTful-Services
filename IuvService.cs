using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace uvInfoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IuvService" in both code and config file together.
    [ServiceContract]
    public interface IuvService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/uvIndex/{zipcode}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        double uvIndex(string zipcode);
    }
}

using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Linq;
using CustomerManagement.Model.Students;

namespace CustomerManagement
{
    [ServiceContract]
    public interface ICustomerManagementRestService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/students")]
        XElement Search();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/students/{studentId}")]
        XElement Get(string studentId);

        [XmlSerializerFormat]
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/students")]
        XElement Create(Student student);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/students/{studentId}")]
        XElement Delete(string studentId);

    }
}
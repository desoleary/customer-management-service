using System.Xml.Linq;

namespace CustomerManagement.Handlers
{
    public interface ISearchRequestHandler
    {
        XElement HandleRequest(IRequestParameter requestParameter);
    }
}
using System.Xml.Linq;
using CustomerManagement.Model.Students;

namespace CustomerManagement.Handlers
{
    public interface IRequestHandler
    {
        XElement HandleRequestFor(string id);
        XElement HandleRequestFor(Student student);
    }
}
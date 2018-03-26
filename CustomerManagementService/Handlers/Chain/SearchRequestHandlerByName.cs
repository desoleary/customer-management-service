using System.Xml.Linq;
using CustomerManagement.Model.Students;
using CustomerManagement.View;

namespace CustomerManagement.Handlers.Chain
{
    public class SearchRequestHandlerByName : ISearchRequestHandler
    {
        private readonly ISearchRequestHandler successor;
        private readonly IStudentRepository repository;
        private readonly IViewEngine viewEngine;

        public SearchRequestHandlerByName(ISearchRequestHandler successor, IStudentRepository repository, IViewEngine viewEngine)
        {
            this.successor = successor;
            this.repository = repository;
            this.viewEngine = viewEngine;
        }

        public XElement HandleRequest(IRequestParameter requestParameter)
        {
            if (IsSatisfiedBy(requestParameter))
            {
                var students = repository.SearchByName(requestParameter.FirstName, requestParameter.LastName);
                return XElement.Parse(viewEngine.Render(students));
            }

            return successor.HandleRequest(requestParameter);
        }

        private static bool IsSatisfiedBy(IRequestParameter requestParameter)
        {
            return !string.IsNullOrEmpty(requestParameter.FirstName) && !string.IsNullOrEmpty(requestParameter.LastName);
        }
    }
}
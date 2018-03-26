using System.Collections;
using System.Collections.Generic;
using CustomerManagement.Exceptions;
using CustomerManagement.Templates;
using CustomerManagement.Web;

namespace CustomerManagement.View
{
    public class ViewEngine : IViewEngine
    {
        private readonly ITemplateEngine templateEngine;
        private readonly IDictionary<string, string> templates;
        private readonly IWebOperationContext webOperationContext;

        public ViewEngine(ITemplateEngine templateEngine, IDictionary<string, string> templates, IWebOperationContext webOperationContext)
        {
            this.templateEngine = templateEngine;
            this.templates = templates;
            this.webOperationContext = webOperationContext;
        }

        public string Render<T>(T objectToRender)
        {
            var key = GetKeyFrom(objectToRender);
            if (templates.ContainsKey(key))
                return templateEngine.Merge(templates[key], CreateContextFor(GetTypeName(objectToRender), objectToRender));

            throw new TemplateResolutionException(typeof(T));
        }

        private string GetKeyFrom<T>(T objectToRender)
        {
            return webOperationContext.IncomingRequest.HttpMethod + "-" + GetTypeName(objectToRender);
        }

        private static IDictionary<string, object> CreateContextFor<T>(string typeName, T objectToRender)
        {
            return new Dictionary<string, object> { { typeName, objectToRender } };
        }

        private static string GetTypeName<T>(T objectToRender)
        {
            if (objectToRender is IEnumerable)
            {                
                foreach(var enumeration in objectToRender as IEnumerable)
                {
                    return enumeration.GetType().Name + "List";
                }
            }

            return objectToRender.GetType().Name;
        }
    }
}
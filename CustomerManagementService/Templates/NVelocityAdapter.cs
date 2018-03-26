using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Commons.Collections;
using NVelocity;
using NVelocity.App;

namespace CustomerManagement.Templates
{
    public class NVelocityAdapter : ITemplateEngine
    {
        private readonly VelocityEngine velocity;

        public NVelocityAdapter() :
            this(new Dictionary<string, string>
                     {
                         {"resource.loader", "assembly"},
                         {"assembly.resource.loader.assembly", Assembly.GetExecutingAssembly().GetName().Name},
                         {"assembly.resource.loader.class", "NVelocity.Runtime.Resource.Loader.AssemblyResourceLoader"}
                     })
        {
        }

        private NVelocityAdapter(IDictionary dictionary)
        {
            velocity = new VelocityEngine(LoadExtendedPropertiesFrom(dictionary));
        }

        public string Merge(string templateName, IDictionary<string, object> dictionary)
        {
            using (var writer = new StringWriter())
            {
                velocity.GetTemplate(templateName).Merge(LoadContextFrom(dictionary), writer);
                return writer.ToString();
            }
        }

        private static ExtendedProperties LoadExtendedPropertiesFrom(IDictionary dictionary)
        {
            var properties = new ExtendedProperties();
            foreach (string key in dictionary.Keys)
                properties.AddProperty(key, dictionary[key]);

            return properties;
        }

        private static VelocityContext LoadContextFrom(IDictionary<string, object> dictionary)
        {
            var velocityContext = new VelocityContext();
            foreach (var key in dictionary.Keys)
                velocityContext.Put(key, dictionary[key]);

            return velocityContext;
        }
    }
}
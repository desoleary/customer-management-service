using System;
using System.IO;
using System.Net;
using System.Text;
using CustomerManagement.Logging;
using Request = System.Net.WebRequest;
using Response = System.Net.WebResponse;

namespace CustomerManagement.Web.Adapters
{
    public class WebRequest : IWebRequest
    {
        public WebRequest(int timeout)
        {
            Timeout = timeout;
        }

        public int Timeout { get; private set; }

        public IWebResponse GetResponseFor(string httpMethod, string uri)
        {
            Log.For(this).Debug(uri);

            var request = Request.Create(uri);
            request.Method = httpMethod;
            request.Timeout = Timeout;

            try
            {
                return CreateResponseFrom(request.GetResponse());
            }
            catch (WebException e)
            {
                if (e.Response != null)
                    return CreateResponseFrom(e.Response);

                throw;
            }
        }

        public IWebResponse GetResponseFor(string httpMethod, string uri, string xmlBody)
        {
            Log.For(this).Debug(uri);

            var request = Request.Create(uri);
            request.Method = httpMethod;
            request.Timeout = Timeout;
            request.ContentType = "text/xml";
            request.ContentLength = xmlBody.Length;

            using (var writeStream = request.GetRequestStream())
            {
                var encoding = new UTF8Encoding();
                var bytes = encoding.GetBytes(xmlBody);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                return CreateResponseFrom(request.GetResponse());
            }
            catch (WebException e)
            {
                if (e.Response != null)
                    return CreateResponseFrom(e.Response);

                throw;
            }
        }

        private static IWebResponse CreateResponseFrom(Response response)
        {
            try
            {
                var httpWebResponse = (HttpWebResponse)response;
                return new WebResponse(httpWebResponse.StatusCode, ReadBodyFrom(httpWebResponse));
            }
            finally
            {
                response.Close();
            }
        }

        private static string ReadBodyFrom(Response response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
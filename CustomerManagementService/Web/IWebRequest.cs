namespace CustomerManagement.Web
{
    public interface IWebRequest
    {
        IWebResponse GetResponseFor(string httpMethod, string uri);
        int Timeout { get; }
    }
}
using CustomerManagement.Infrastructure.Container;

namespace CustomerManagement.Web
{
    [ExcludeFromContainer]
    public class ClientAddress
    {
        private readonly string address;
        private readonly int port;

        public ClientAddress(string address, int port)
        {
            this.address = address;
            this.port = port;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", address, port);
        }
    }
}
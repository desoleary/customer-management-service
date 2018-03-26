namespace CustomerManagement.Web.Adapters
{
    public class WebOperationContext : IWebOperationContext
    {
        public IIncomingRequest IncomingRequest
        {
            get { return new IncomingRequest(); }
        }

        public IOutgoingResponse OutgoingResponse
        {
            get { return new OutgoingResponse(); }
        }
    }
}
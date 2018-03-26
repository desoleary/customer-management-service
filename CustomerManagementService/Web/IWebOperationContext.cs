namespace CustomerManagement.Web
{
    public interface IWebOperationContext
    {
        IIncomingRequest IncomingRequest{ get; }
        IOutgoingResponse OutgoingResponse { get; }
    }
}
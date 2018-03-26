namespace CustomerManagement
{
    public interface IXmlSerializerAdapter
    {
        string Serialize<T>(T objectToBeSerialised);
        T Deserialize<T>(string xml);
    }
}
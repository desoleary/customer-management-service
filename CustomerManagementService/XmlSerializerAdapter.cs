using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CustomerManagement
{
    public class XmlSerializerAdapter : IXmlSerializerAdapter
    {
        public string Serialize<T>(T objectToBeSerialised)
        {
            using (var memoryStream = new MemoryStream())
            {
                var xs = new XmlSerializer(typeof(T));
                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, objectToBeSerialised);

                var serializedXml = UTF8ByteArrayToString(((MemoryStream)xmlTextWriter.BaseStream).ToArray());
                xmlTextWriter.Close();
                return serializedXml;
            }
        } 

        public T Deserialize<T>(string xml)
        {
            using (var memoryStream = new MemoryStream(StringToUTF8ByteArray(xml)))
            {
                var xs = new XmlSerializer(typeof (T));
                return (T) xs.Deserialize(memoryStream);
            }
        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            var encoding = new UTF8Encoding();
            var byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        private string UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetString(characters);
        }

    }
}
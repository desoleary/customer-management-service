using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Properties;

namespace CustomerManagement.Model.Students
{
    [ExcludeFromContainer]
//    [XmlRoot(Namespace = "", ElementName = "Student")]
    public class Student : IHyperMediaContext
    {
        public Student(){}
        public Student(int id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string addressLine1,
                       string addressLine2, string grade)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Grade = grade;
        }

        public Student(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string addressLine1, string addressLine2, string grade):
            this(Int32.MinValue, firstName, lastName, dateOfBirth, phoneNumber, addressLine1, addressLine2, grade)
        {
            
        }

//        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }

//        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

//        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }

//        [XmlElement(ElementName = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

//        [XmlElement(ElementName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

//        [XmlElement(ElementName = "AddressLine1")]
        public string AddressLine1 { get; set; }

//        [XmlElement(ElementName = "AddressLine2")]
        public string AddressLine2 { get; set; }

//        [XmlElement(ElementName = "Grade")]
        public string Grade { get; set; }
        
        [IgnoreDataMember]
        public string GetUri
        {
            get
            {
                return string.Format(Settings.Default.GetStudentByIdUriTemplate, Id);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using CustomerManagement.Common.Extensions;
using CustomerManagement.Model.Students;
using CustomerManagement.Templates;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CustomerManagement.FunctionalTests.Templates
{
    [TestFixture]
    public class NVelocityAdapterTest
    {
        [Test]
        public void ShouldRenderListOfStudents()
        {
            var adapter = new NVelocityAdapter();

            IEnumerable<Student> students = new List<Student>
                                                {
                                                    new Student(12345, "firstName", "lastName", new DateTime(2010, 2, 3), "phoneNumber",
                                                                "addressLine1", "addressLine2", "grade")
                                                };
            IDictionary<string, object> dictionary = new Dictionary<string, object> { { "StudentList",  students}};

            var actualXml = adapter.Merge("CustomerManagement.Resources.SearchStudentsByName.vm", dictionary);
            actualXml.WithoutFormattingShouldEqual(ToXmlString(students));
        }

        [Test]
        public void ShouldRenderGetStudent()
        {
            var adapter = new NVelocityAdapter();

            var student = new Student(12345, "firstName", "lastName", new DateTime(2010, 2, 3), "phoneNumber",
                                      "addressLine1", "addressLine2", "grade");

            IDictionary<string, object> dictionary = new Dictionary<string, object> { { "Student", student } };

            var actualXml = adapter.Merge("CustomerManagement.Resources.GetStudent.vm", dictionary);
            actualXml.WithoutFormattingShouldEqual(StudentToStringBuilder(student));
        }

        [Test]
        public void ShouldRenderCreateStudent()
        {
            var adapter = new NVelocityAdapter();
            const int id = 123;

            var student = new Student(id, "firstName", "lastName", new DateTime(2010, 2, 3), "phoneNumber",
                                      "addressLine1", "addressLine2", "grade");

            IDictionary<string, object> dictionary = new Dictionary<string, object> { { "Student", student } };

            var actualXml = adapter.Merge("CustomerManagement.Resources.CreateStudent.vm", dictionary);
            actualXml.WithoutFormattingShouldEqual(CreateStudentConfirmationXml(id));
        }

        [Test]
        private static string ToXmlString(IEnumerable<Student> students)
        {
            var sb = new StringBuilder();
            sb.Append("<Students>");
            foreach (var student in students)
            {
                sb.Append(StudentToStringBuilder(student));
            }
            sb.Append("</Students>");
            return sb.ToString();
        }

        private static string CreateStudentConfirmationXml(int id)
        {
            return "<confirmation studentId=\"" + id +
                   "\" href=\"http://localhost/CustomerManagementRestServiceDev/CustomerManagementRestService.svc/students/" +
                   id + "\"/>";
        }

        private static string StudentToStringBuilder(Student student)
        {
            var sb = new StringBuilder();
            sb.Append("<Student href=\"http://localhost/CustomerManagementRestServiceDev/CustomerManagementRestService.svc/students/12345\">");
            sb.Append("<FirstName>" + student.FirstName + "</FirstName>");
            sb.Append("<LastName>" + student.LastName + "</LastName>");
            sb.Append("<DateOfBirth>" + student.DateOfBirth + "</DateOfBirth>");
            sb.Append("<PhoneNumber>" + student.PhoneNumber + "</PhoneNumber>");
            sb.Append("<AddressLine1>" + student.AddressLine1 + "</AddressLine1>");
            sb.Append("<AddressLine2>" + student.AddressLine2 + "</AddressLine2>");
            sb.Append("<Grade>" + student.Grade + "</Grade>");
            sb.Append("</Student>");
            return sb.ToString();
        }
    }
}

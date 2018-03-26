using System;
using System.Collections.Generic;
using System.Net;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using CustomerManagement.Exceptions;
using CustomerManagement.Mappers;
using CustomerManagement.Model.Students;
using CustomerManagement.Persistance.Students;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CustomerManagement.IntegrationTests.Persistance.Students
{
    [TestFixture]
    public class StudentRepositoryTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            var source = new XmlConfigurationSource("activeRecord.config");
            ActiveRecordStarter.Initialize(source, typeof(StudentDAO));
        }

        [Test]
        public void ShouldReturnStudentsWithSameFullNameAsGivenFullName()
        {
            var students = new List<Student>(CreateRepository().SearchByName("Joe", "Blog"));
            students.Count.ShouldEqual(1);
            students[0].FirstName.ShouldEqual("Joe");
            students[0].LastName.ShouldEqual("Blog");
        }

        [Test]
        public void ShouldReturnStudentsWithPartialMatchesWhenGivenPartialFirstName()
        {
            var students = new List<Student>(CreateRepository().SearchByName("J", "Blog"));
            students.Count.ShouldEqual(1);
            students[0].FirstName.ShouldEqual("Joe");
            students[0].LastName.ShouldEqual("Blog");
        }

        [Test]
        public void ShouldReturnStudentsWithPartialMatchesWhenGivenPartialLastName()
        {
            var students = new List<Student>(CreateRepository().SearchByName("Joe", "B"));
            students.Count.ShouldEqual(1);
            students[0].FirstName.ShouldEqual("Joe");
            students[0].LastName.ShouldEqual("Blog");
        }

        [Test]
        public void ShouldReturnNoStudentsWhenNoMatchIsFoundForGivenName()
        {
            var students = new List<Student>(CreateRepository().SearchByName("Eliot", "Wood"));
            students.Count.ShouldEqual(0);
        }

        [Test]
        public void ShouldGetStudentWithGivenId()
        {
            var student = CreateRepository().Get("1");

            student.FirstName.ShouldEqual("Joe");
            student.LastName.ShouldEqual("Blog");
        }

        [Test]
        public void ShouldThrowNotFoundRestExceptionWhenGivenInvalidId()
        {
            const string unexpectedIdentity = "12345";
            Common.TestUtilities.Assert.ThrowsNotFoundRestException(() => CreateRepository().Get(unexpectedIdentity));
        }

        [Test]
        public void ShouldCreateStudent()
        {
            var student = CreateRepository().Modify(new Student
                                                {
                                                    FirstName = "Joey",
                                                    LastName = "Tomatoe",
                                                    AddressLine1 = "somewhere in the universe",
                                                    AddressLine2 = "past the sun",
                                                    DateOfBirth = DateTime.Now,
                                                    Grade = "Kindergarden",
                                                    PhoneNumber = "403-987-6543"
                                                });

            student.Id.ShouldNotBeNull();
            student.FirstName.ShouldEqual("Joey");
            student.LastName.ShouldEqual("Tomatoe");

            CreateRepository().Delete(student.Id.ToString());

            Common.TestUtilities.Assert.ThrowsRestExceptionWithHttpStatusCodeOf(HttpStatusCode.NotFound, () => CreateRepository().Get(student.Id.ToString()));
        }

        private static StudentRepository CreateRepository()
        {
            return new StudentRepository(new StudentDAOMapper(), new StudentMapper());
        }
    }
}
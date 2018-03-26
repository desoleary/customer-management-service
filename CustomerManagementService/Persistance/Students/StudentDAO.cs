using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Linq;

namespace CustomerManagement.Persistance.Students
{
    [ActiveRecord("Student")]
    public class StudentDAO : ActiveRecordLinqBase<StudentDAO>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public String FirstName { get; set; }

        [Property]
        public String LastName { get; set; }

        [Property]
        public DateTime DateOfBirth { get; set; }

        [Property]
        public String PhoneNumber { get; set; }

        [Property]
        public String AddressLine1 { get; set; }

        [Property]
        public String AddressLine2 { get; set; }

        [Property]
        public String Grade { get; set; }
    }
}
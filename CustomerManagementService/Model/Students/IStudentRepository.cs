using System.Collections.Generic;

namespace CustomerManagement.Model.Students
{
    public interface IStudentRepository
    {
        IEnumerable<Student> SearchByName(string firstName, string lastName);
        Student Get(string id);
        Student Modify(Student student);
        void Delete(string id);
    }
}
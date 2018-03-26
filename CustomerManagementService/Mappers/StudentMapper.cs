using CustomerManagement.Model.Students;
using CustomerManagement.Persistance.Students;

namespace CustomerManagement.Mappers
{
    public class StudentMapper : IMapper<StudentDAO, Student>
    {
        public Student MapFrom(StudentDAO studentDAO)
        {
            return new Student(studentDAO.Id, studentDAO.FirstName, studentDAO.LastName, studentDAO.DateOfBirth,
                               studentDAO.PhoneNumber, studentDAO.AddressLine1, studentDAO.AddressLine2, studentDAO.Grade);
        }
    }
}

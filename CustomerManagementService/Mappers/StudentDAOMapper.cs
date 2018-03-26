using CustomerManagement.Model.Students;
using CustomerManagement.Persistance.Students;

namespace CustomerManagement.Mappers
{
    public class StudentDAOMapper : IMapper<Student, StudentDAO>
    {
        public StudentDAO MapFrom(Student student)
        {
            return new StudentDAO
                       {
                           Id = (int)student.Id,
                           FirstName = student.FirstName,
                           LastName = student.LastName,
                           DateOfBirth = student.DateOfBirth,
                           PhoneNumber = student.PhoneNumber,
                           AddressLine1 = student.AddressLine1,
                           AddressLine2 = student.AddressLine2,
                           Grade = student.Grade
                       };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CustomerManagement.Exceptions;
using CustomerManagement.Mappers;
using CustomerManagement.Model.Students;

namespace CustomerManagement.Persistance.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMapper<Student, StudentDAO> studentDOAMapper;
        private readonly IMapper<StudentDAO, Student> studentMapper;

        public StudentRepository(IMapper<Student, StudentDAO> studentDOAMapper, IMapper<StudentDAO, Student> studentMapper)
        {
            this.studentDOAMapper = studentDOAMapper;
            this.studentMapper = studentMapper;
        }

        public IEnumerable<Student> SearchByName(string firstName, string lastName)
        {
            return (from student in StudentDAO.Queryable
                    where student.FirstName.Contains(firstName) && student.LastName.Contains(lastName)
                    select new Student(student.Id, student.FirstName, student.LastName, student.DateOfBirth, student.PhoneNumber,
                                       student.AddressLine1, student.AddressLine2, student.Grade)
                   ).ToList();
        }

        public Student Get(string id)
        {
            try
            {
                var student = StudentDAO.Find(Convert.ToInt32(id));
                return studentMapper.MapFrom(student);
            }
            catch(Castle.ActiveRecord.NotFoundException)
            {
                throw new RestException(HttpStatusCode.NotFound);
            }

        }

        public Student Modify(Student student)
        {
            var studentDAO = studentDOAMapper.MapFrom(student);
            studentDAO.SaveAndFlush();
            return studentMapper.MapFrom(studentDAO);
        }

        public void Delete(string id)
        {
            var studentDAO = new StudentDAO {Id = Convert.ToInt32(id)};
            studentDAO.DeleteAndFlush();
        }
    }
}
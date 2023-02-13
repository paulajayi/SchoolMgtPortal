using Synotech.SMP.Core.Interfaces.Student;
using Synotech.SMP.Core.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Core.Services.Student
{
    public class StudentService : IStudentService
    {
        public Task<bool> DeleteStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public List<StudentModel> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Task<StudentModel> GetStudentById(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveStudent(StudentModel student)
        {
            throw new NotImplementedException();
        }

        public StudentModel SearchStudent(string strSearch)
        {
            throw new NotImplementedException();
        }
    }
}

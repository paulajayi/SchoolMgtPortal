using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Synotech.SMP.Core.Models;
using Synotech.SMP.Core.Models.Student;

namespace Synotech.SMP.Core.Interfaces.Student
{
    public interface IStudentService
    {
        List<StudentModel> GetAllStudents();
        Task<bool> SaveStudent(StudentModel student);
        Task<bool> DeleteStudent(int studentId);
        Task<StudentModel> GetStudentById(int studentId);
        StudentModel SearchStudent(string strSearch);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bondarchuk_zhukovskyLab5_class_library_
{
    public interface IEJB_LABRemote
    {
        string GetStudInfo(string fio);
        string AddStudent(string fio, string department, string group);
        string DeleteStudent(Student student);
        string[] GetAllStudents();
        string[] GetAllStudents(string department);
        int GetNumberOfStudents(string group);
    }
}

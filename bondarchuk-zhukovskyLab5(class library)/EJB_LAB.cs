using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bondarchuk_zhukovskyLab5_class_library_
{
    public class EJB_LAB : IEJB_LABRemote
    {
        private List<Student> students = new List<Student>
        {
            new Student("ivanov", "Marketing", "10"),
            new Student("petrov", "Marketing", "10"),
            new Student("sidorov", "Professor", "12"),
            new Student("mishin", "Smith", "12"),
            new Student("vasin", "Programmer", "14")
        };

        public string AddStudent(string fio, string department, string group)
        {
            Student student = new Student(fio, department, group);
            students.Add(student);
            return null;
        }

        public string DeleteStudent(Student student)
        {
            students.Remove(student);
            return null;
        }

        public string[] GetAllStudents()
        {
            string[] arr = new string[students.Count];
            for (int i = 0; i < students.Count; i++)
            {
                arr[i] = students[i].Fio + "  " + students[i].Department + "  " + students[i].Group;
            }

            return arr;
        }

        public string[] GetAllStudents(string department)
        {
            string[] arr;
            List<Student> students2 = new List<Student>();
            foreach (var s in students)
            {
                if (s.Department == department)
                {
                    students2.Add(s);
                }
            }

            arr = new string[students2.Count];
            for (int i = 0; i < students2.Count; i++)
            {
                arr[i] = students2[i].Fio + "  " + students2[i].Department + "  " + students2[i].Group;
            }

            return arr;
        }

        public int GetNumberOfStudents(string group)
        {
            int count = 0;
            foreach (var s in students)
            {
                if (s.Group == group)
                {
                    count++;
                }
            }

            return count;
        }

        public string GetStudInfo(string fio)
        {
            string result = "???";
            foreach (var s in students)
            {
                if (s.Fio == fio)
                {
                    result = s.Department + "  " + s.Group;
                    break;
                }
            }

            return result;
        }
    }
}

namespace bondarchuk_zhukovskyLab5_class_library_
{
    public class Student
    {
        private string fio;
        private string department;
        private string group;

        public Student(string fio, string department, string group)
        {
            this.fio = fio;
            this.department = department;
            this.group = group;
        }

        public string Fio
        {
            get { return fio; }
            set { fio = value; }
        }

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }
    }
}
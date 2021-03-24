using System;
using bondarchuk_zhukovskyLab5_class_library_;

namespace bondarchuk_zhukovskyLab5_client_
{
    class Program
    {
        static void Main()
        {
            IEJB_LABRemote remote = new EJB_LAB();
            string[] arr = remote.GetAllStudents("Programmer");
            foreach (var s in arr)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("info about ivanov: {0}", remote.GetStudInfo("ivanov"));
            Console.ReadLine();
        }
    }
}

using Newtonsoft.Json;
using System.IO;

namespace bondarchuk_zhukovskyLab2_server_.Services
{
    class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            this.PATH = path;
        }

        public decimal LoadData()
        {
            var fileExists = File.Exists(this.PATH);
            if (!fileExists)
            {
                File.CreateText(this.PATH).Dispose();
                return new decimal();
            }
            using (StreamReader reader = File.OpenText(this.PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<decimal>(fileText);
            }
        }

        public void SaveData(decimal sum)
        {
            using (StreamWriter writer = File.CreateText(this.PATH))
            {
                string output = JsonConvert.SerializeObject(sum);
                writer.Write(output);
            }
        }
    }
}

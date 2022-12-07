using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Helpers
{
    public class Files
    {
        public static string filesDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Resources/Files"));

        public string FilePath { get; set; }

        public Files() { }

        public Files(string fileName)
        {
            FilePath = $"{filesDir}/{fileName}";
        }

        public void CreateFile()
        {
            if (!File.Exists(FilePath))
            {
                _ = File.Create(FilePath);
            }
        }

        public T GetFileContent<T>()
        {
            T items = default;

            using (StreamReader r = new StreamReader(FilePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<T>(json);
            }

            return items;
        }

        public void UpdateFile<T>(T list)
        {
            string json = JsonConvert.SerializeObject(list);

            File.WriteAllText(FilePath, json);
        }
    }
}

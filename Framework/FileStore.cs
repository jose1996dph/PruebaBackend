using System;
using System.IO;

namespace Framework
{
    public class FileStore
    {
        public void SaveFile(string base64, string name)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files\\");
            var filePath = Path.Combine(path, name);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(filePath, bytes);
        }
    }
}

using System;
using System.IO;

namespace Framework
{
    public class FileStore
    {
        public void SaveFile(string base64, string name)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\", name);

            var bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(filePath, bytes);
        }
    }
}

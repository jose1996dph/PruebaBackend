using Data.Interfaces;
using Framework;
using System;

namespace Data
{
    public class FileRepository : IFileRepository
    {
        readonly FileStore fileStore;
        public FileRepository()
        {
            fileStore = new FileStore();
        }
        public void Save(string file, string name)
        {
            fileStore.SaveFile(file, name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IFileRepository
    {
        void Save(string file, string name);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PM02LAB2EJ1.Interfaces
{
    public interface IFileService
    {
        bool SavePicture(string filename, Stream data);
        string GetPicturePath();
    }
}

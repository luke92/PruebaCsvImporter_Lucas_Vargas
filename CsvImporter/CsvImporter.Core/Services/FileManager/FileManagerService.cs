using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvImporter.Core.Services.FileManager
{
    public interface IFileManagerService
    {
        StreamReader StreamReader(string path);
    }

    public class FileManagerService : IFileManagerService
    {
        public StreamReader StreamReader(string path)
        {
            try
            {
                return new StreamReader(path);
            }
            catch
            {
                return null;
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CsvImporter.Core.Services.FileManager
{
    public interface IFileManagerService
    {
        StreamReader StreamReader(string path, bool isUrl = false);
    }

    public class FileManagerService : IFileManagerService
    {
        public StreamReader StreamReader(string path, bool isUrl = false)
        {
            try
            {
                if (isUrl)
                {
                    HttpWebRequest request = WebRequest.Create(path) as HttpWebRequest;
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    return new StreamReader(response.GetResponseStream());
                }
                else
                {
                    return new StreamReader(path);
                }               
            }
            catch
            {
                return null;
            }
            
        }
    }
}

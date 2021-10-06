using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvImporter.Core.Helpers.CSV
{
    public static class CSVHelper
    {
        public static string DetectDelimiter(StreamReader reader)
        {
            if (reader == null)
                return null;

            try
            {
                var possibleDelimiters = new List<string> { ",", ";", "\t", "|" };

                var headerLine = reader.ReadLine();

                reader.BaseStream.Position = 0;
                reader.DiscardBufferedData();

                foreach (var possibleDelimiter in possibleDelimiters)
                {
                    if (headerLine.Contains(possibleDelimiter))
                    {
                        return possibleDelimiter;
                    }
                }

                return possibleDelimiters[0];
            }
            catch
            {
                return null;
            }            
        }

        public static bool CanReadHeader(CsvReader csvReader)
        {
            try
            {
                var canRead = csvReader.Read();
                if (canRead)
                {
                    canRead = csvReader.ReadHeader();
                    if (!canRead)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        public static T GetRecord<T>(CsvReader csvReader)
        {
            try
            {
                return csvReader.GetRecord<T>();
            }
            catch
            {
                return default(T);
            }
        }
    }
}

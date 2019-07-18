using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FroniusSolarApi.Repository.Csv
{
    /// <summary>
    /// Handles writing an object to a csv file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvWriter<T>
    {
        //private readonly ILogger _logger;

        //public CsvWriter(ILogger<CsvWriter<T>> logger)
        //{
        //    _logger = logger;
        //}
        private string BuildCsvRow(object data, bool isHeader)
        {
            if (data == null)
                return "";

            var sb = new StringBuilder();
            Type objType = data.GetType();

            foreach (PropertyInfo property in objType.GetProperties())
            {
                object propValue = property.GetValue(data);
                var elems = propValue as IList;

                if (elems != null)
                {
                    foreach (var item in elems)
                    {
                       sb.Append($"{property.Name},");                 
                       sb.Append(BuildCsvRow(item, isHeader));
                    }
                }
                else if (property.Name != "ExtensionData")
                {
                    if (isHeader)
                    {
                        // Append property name for header
                        sb.Append($"{property.Name},");
                    }
                    else
                    {
                        // Append property value
                        sb.Append($"{propValue},");
                    }
                   
                    if (property.PropertyType.Assembly == objType.Assembly)
                    {
                        sb.Append(BuildCsvRow(propValue, isHeader));
                    }
                }
            }

            return Sanitize(sb.ToString());
        }

        /// <summary>
        /// Cleans illegal csv characters from row, it is not a complete solution but its a start
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Sanitize(string input)
        {
            input = input.Replace('ı', 'i')
                .Replace('ç', 'c')
                .Replace('ö', 'o')
                .Replace('ş', 's')
                .Replace('ü', 'u')
                .Replace('ğ', 'g')
                .Replace('İ', 'I')
                .Replace('Ç', 'C')
                .Replace('Ö', 'O')
                .Replace('Ş', 'S')
                .Replace('Ü', 'U')
                .Replace('Ğ', 'G')
                .Trim();

            if (input.Contains(","))
            {
                input = "" + input + "";
            }
            return input;
        }

        /// <summary>
        /// Writes the data object to a csv file location
        /// </summary>
        /// <param name="data"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool WriteCsv(T data, string location)
        {
            if (data == null)
                throw new ArgumentNullException($"Can not write null object {nameof(data)}");

            bool fileExists = false;

            fileExists = File.Exists(location);

            try
            {
                using (StreamWriter sw = new StreamWriter(location, true))
                {
                    // Header
                    if (!fileExists)
                        sw.WriteLine(BuildCsvRow(data, true) + "Timestamp");
                    //Data
                    sw.WriteLine(BuildCsvRow(data, false) + DateTime.Now.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }      
        }
    }
}

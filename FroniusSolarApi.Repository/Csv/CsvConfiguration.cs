using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FroniusSolarApi.Repository.Csv
{
    public class CsvConfiguration
    {
        // The section in app settings where the configuration settings are for csv the repository
        private const string _csvConfigSectionName = "CsvConfiguration";

        public string FileLocation { get; set; }
        public string FileName { get; set; }

        public CsvConfiguration(string fileLocation, string fileName)
        {
            if (String.IsNullOrEmpty(fileLocation))
                throw new ArgumentNullException(nameof(fileLocation));

            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            this.FileLocation = fileLocation;
            this.FileName = fileName;
        }

        public CsvConfiguration(IConfiguration configuration)
        {
            // CsvConfiguration section
            var csvSection = configuration.GetSection(_csvConfigSectionName);

            // Check to see if a csv configuration section exists.
            if (!csvSection.Exists())
                throw new Exception($"Could not find {_csvConfigSectionName} section in appsettings");

            if (!csvSection.GetSection("FileLocation").Exists())
                throw new ArgumentNullException(nameof(FileLocation));

            if (!csvSection.GetSection("FileName").Exists())
                throw new ArgumentNullException(nameof(FileName));


            FileLocation = csvSection.GetSection("FileLocation").Value;
            FileName = csvSection.GetSection("FileName").Value;
        }

        public string GetSaveLocation(string collection)
        {
            return FileLocation + @"/" + collection + "_" + FileName + ".csv";
        }
    }
}
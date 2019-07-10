namespace FroniusSolarApi.Repository.Csv
{
    public class CsvConfiguration
    {
        public string FileLocation { get; set; }
        public string FileName { get; set; }

        public CsvConfiguration(string fileLocation, string fileName)
        {
            this.FileLocation = fileLocation;
            this.FileName = fileName;
        }

        public string GetSaveLocation(string collection)
        {
            return FileLocation + @"\" + collection + "_" + FileName + ".csv";
        }
    }
}
using DeviceDataProcessing.DataAccess.Interfaces;

namespace DeviceDataProcessing.DataAccess
{
    public class FileOperation : IFileOperation
    {
        public FileOperation() { }
        public StreamReader StreamReader(string pathToJson)
        {
            return new StreamReader(pathToJson);
        }
    }
}

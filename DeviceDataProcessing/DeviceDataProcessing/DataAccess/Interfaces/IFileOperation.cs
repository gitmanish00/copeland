using DeviceDataProcessing.Models;

namespace DeviceDataProcessing.DataAccess.Interfaces
{
    public interface IFileOperation
    {
        StreamReader StreamReader(string pathToJson);
    }
}

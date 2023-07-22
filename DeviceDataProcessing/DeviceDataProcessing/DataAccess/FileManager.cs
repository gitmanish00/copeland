using DeviceDataProcessing.DataAccess.Interfaces;
using DeviceDataProcessing.Models;
using Newtonsoft.Json;

namespace DeviceDataProcessing.DataAccess
{
    public class FileManager<T> 
    {
        private IFileOperation _fileOperation;

        public FileManager(IFileOperation fileOperation){
            _fileOperation = fileOperation;
        }
        public T ParseFile(T jsonObject, string pathToJson){
            if (string.IsNullOrEmpty(pathToJson))
                throw new NullReferenceException("Invalid json file path");
            using (var reader = _fileOperation.StreamReader(pathToJson)){
                var json = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(json)){
                    try{
                        jsonObject = JsonConvert.DeserializeObject<T>(json);
                    }
                    catch (JsonSerializationException ex){
                        Console.WriteLine("Wrong json file format. Error message: " + ex.ToString());
                    }

                }
            }
             return jsonObject;
        }

        public void WriteToFile(List<DeviceDataMerged> deviceDataMerged, string fileName){
                    var jsonText = JsonConvert.SerializeObject(deviceDataMerged);
            try
            {
                File.WriteAllText(fileName, jsonText);
            }catch(Exception e)
            {
                Console.Write(e.ToString());
            }
                             
        }
    }
}

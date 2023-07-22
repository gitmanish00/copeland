using DeviceDataProcessing.DataAccess;
using DeviceDataProcessing.DataAccess.Interfaces;
using DeviceDataProcessing.Models;
using DeviceDataProcessingTest.Business;
using DeviceDataProcessingTest.Business.Interfaces;

namespace DeviceDataProcessing.Business
{
    public class ProcessFiles
    {
        private IFileOperation _fileOperation;
        private DeviceDataFoo1? _deviceDataFoo1Parsed = null;
        private DeviceDataFoo2? _deviceDataFoo2Parsed = null;

        public ProcessFiles(IFileOperation fileOperation) {
        _fileOperation = fileOperation;
        }
    
        public void Process(string foo1FilePath, string foo2FilePath, string outputFilePath)
        {
            var _fileManager1 = new FileManager<DeviceDataFoo1>(_fileOperation);
            _deviceDataFoo1Parsed = _fileManager1.ParseFile(new DeviceDataFoo1(), Path.Combine(".",foo1FilePath));
            var obj = GetObjectToMap("DeviceDataFoo1");
            var liDDFMerged = new List<DeviceDataMerged>();
            if (obj != null)
            {
                var deviceDataMerged1 = obj.Map();
                if(deviceDataMerged1 != null)
                liDDFMerged.AddRange(deviceDataMerged1);
            }

            var _fileManager2 = new FileManager<DeviceDataFoo2>(_fileOperation);
            _deviceDataFoo2Parsed = _fileManager2.ParseFile(new DeviceDataFoo2(), Path.Combine(".", foo2FilePath));
            obj = GetObjectToMap("DeviceDataFoo2");

            if (obj != null)
            {
                var deviceDataMerged2 = obj.Map();
                if (deviceDataMerged2 != null)
                    liDDFMerged.AddRange(deviceDataMerged2);
            }
            _fileManager2.WriteToFile(liDDFMerged, Path.Combine(".", outputFilePath));
        }

        private IDeviceDataFooMgr? GetObjectToMap(string objectToCreate)
        {
            switch (objectToCreate) {
                case "DeviceDataFoo1":{
                        DeviceDataFoo1Mgr devDeviceFoo1;
                        if (_deviceDataFoo1Parsed != null)
                             devDeviceFoo1 = new DeviceDataFoo1Mgr(_deviceDataFoo1Parsed);
                        else
                             devDeviceFoo1 = new DeviceDataFoo1Mgr(new DeviceDataFoo1());
                        return devDeviceFoo1;
                    }
                case "DeviceDataFoo2":{
                        DeviceDataFoo2Mgr devDeviceFoo2;
                        if (_deviceDataFoo2Parsed != null)
                             devDeviceFoo2 = new DeviceDataFoo2Mgr(_deviceDataFoo2Parsed);
                        else
                             devDeviceFoo2 = new DeviceDataFoo2Mgr(new DeviceDataFoo2());
                        return devDeviceFoo2;
                    }
                default:
                    return null;
            }
        }
    }
}

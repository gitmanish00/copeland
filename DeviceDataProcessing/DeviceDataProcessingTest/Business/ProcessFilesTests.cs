using DeviceDataProcessing.DataAccess.Interfaces;
using DeviceDataProcessing.DataAccess;
using Moq;
using System.Text;
using DeviceDataProcessing.Business;
using NUnit.Framework;

namespace DeviceDataProcessingTest.Business
{
    public class ProcessFilesTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmptyPath_Returns_NullException()
        {
            var mockFileOperation = new Mock<IFileOperation>();
            var jsonString1 = "{\r\n  \"PartnerId\": 1,\r\n  " +
                "\"PartnerName\": \"Foo1\",\r\n  " +
                "\"Trackers\": [\r\n    {\r\n      \"Id\": 1,\r\n      \"Model\": \"ABC-10\",\r\n      \"ShipmentStartDtm\": \"08-17-2020 10:30:00\",\r\n      \"Sensors\": [\r\n        {\r\n          \"Id\": 100,\r\n          \"Name\": \"Temperature\",\r\n          \"Crumbs\": [\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:35:00\",\r\n              \"Value\": 22.15\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:40:00\",\r\n              \"Value\": 23.15\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:45:00\",\r\n              \"Value\": 24.15\r\n            }\r\n          ]\r\n        },\r\n        {\r\n          \"Id\": 101,\r\n          \"Name\": \"Humidty\",\r\n          \"Crumbs\": [\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:35:00\",\r\n              \"Value\": 80.5\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:40:00\",\r\n              \"Value\": 81.5\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:45:00\",\r\n              \"Value\": 82.5\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }]}";
            byte[] fakeFileBytes1 = Encoding.UTF8.GetBytes(jsonString1);
            MemoryStream fakeMemoryStream1 = new MemoryStream(fakeFileBytes1);

            var jsonString2 = "{\r\n\t\"CompanyId\": 2,\r\n\t\"Company\": \"Foo2\",\r\n\t\"Devices\": [{\r\n\t\t\"DeviceID\": 1,\r\n\t\t\"Name\": \"XYZ-100\",\r\n\t\t\"StartDateTime\": \"08-18-2020 10:30:00\",\r\n\t\t\"SensorData\": [{\r\n\t\t\t\t\"SensorType\": \"TEMP\",\r\n\t\t\t\t\"DateTime\": \"08-18-2020 10:35:00\",\r\n\t\t\t\t\"Value\": 32.15\r\n\t\t\t},\r\n\t\t\t{\r\n\t\t\t\t\"SensorType\": \"TEMP\",\r\n\t\t\t\t\"DateTime\": \"08-18-2020 10:40:00\",\r\n\t\t\t\t\"Value\": 33.15\r\n\t\t\t}, {\r\n\t\t\t\t\"SensorType\": \"TEMP\",\r\n\t\t\t\t\"DateTime\": \"08-18-2020 10:45:00\",\r\n\t\t\t\t\"Value\": 34.15\r\n\t\t\t}, {\r\n\t\t\t\t\"SensorType\": \"HUM\",\r\n\t\t\t\t\"DateTime\": \"08-18-2020 10:35:00\",\r\n\t\t\t\t\"Value\": 90.5\r\n\t\t\t}, {\r\n\t\t\t\t\"DateTime\": \"08-18-2020 10:40:00\",\r\n\t\t\t\t\"Value\": 91.5\r\n\t\t\t}, {\r\n\t\t\t\t\"SensorType\": \"HUM\",\r\n\t\t\t\t\"DateTime\": \"08-18-2020 10:45:00\",\r\n\t\t\t\t\"Value\": 92.5\r\n\t\t\t}\r\n\t\t]\r\n\t}]\r\n}";
            byte[] fakeFileBytes2 = Encoding.UTF8.GetBytes(jsonString2);
            MemoryStream fakeMemoryStream2 = new MemoryStream(fakeFileBytes2);

            mockFileOperation.Setup(fileManager => fileManager.StreamReader(It.Is<string>(x =>x.Contains("Foo1"))))
                   .Returns(() => new StreamReader(fakeMemoryStream1));
            mockFileOperation.Setup(fileManager => fileManager.StreamReader(It.Is<string>(x => x.Contains("Foo2"))))
                  .Returns(() => new StreamReader(fakeMemoryStream2));


            var fileOperation = new FileOperation();
            var process = new ProcessFiles(mockFileOperation.Object);
            process.Process("DeviceDataFoo1.json", "DeviceDataFoo2.json", "DataAccess\\JSON\\output.json");
        }
    }
}

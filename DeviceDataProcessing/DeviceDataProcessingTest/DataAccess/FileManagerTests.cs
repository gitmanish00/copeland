
using DeviceDataProcessing.DataAccess;
using DeviceDataProcessing.DataAccess.Interfaces;
using DeviceDataProcessing.Models;
using Moq;
using NUnit.Framework;
using System.Text;

namespace DeviceDataProcessingTest.DataAccess
{
    public class FileManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }
        #region "Test with DeviceDataFoo1Object"

        [Test]
        public void EmptyPath_Returns_NullException()
        {
            var fileOperation = new Mock<IFileOperation>();
            var sut = new FileManager<DeviceDataFoo1>(fileOperation.Object);
            Assert.Throws<NullReferenceException>(() => sut.ParseFile(new DeviceDataFoo1(),""));
        }

        [Test]
        public void Correct_File_Returns_Value()
        {
            var mockFileOperation = new Mock<IFileOperation>();
            var jsonString = "{\r\n  \"PartnerId\": 1,\r\n  " +
                "\"PartnerName\": \"Foo1\",\r\n  " +
                "\"Trackers\": [\r\n    {\r\n      \"Id\": 1,\r\n      \"Model\": \"ABC-100\",\r\n      \"ShipmentStartDtm\": \"08-17-2020 10:30:00\",\r\n      \"Sensors\": [\r\n        {\r\n          \"Id\": 100,\r\n          \"Name\": \"Temperature\",\r\n          \"Crumbs\": [\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:35:00\",\r\n              \"Value\": 22.15\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:40:00\",\r\n              \"Value\": 23.15\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:45:00\",\r\n              \"Value\": 24.15\r\n            }\r\n          ]\r\n        },\r\n        {\r\n          \"Id\": 101,\r\n          \"Name\": \"Humidty\",\r\n          \"Crumbs\": [\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:35:00\",\r\n              \"Value\": 80.5\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:40:00\",\r\n              \"Value\": 81.5\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:45:00\",\r\n              \"Value\": 82.5\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }]}";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(jsonString);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            mockFileOperation.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                   .Returns(() => new StreamReader(fakeMemoryStream));
            
            var sut = new FileManager<DeviceDataFoo1>(mockFileOperation.Object);
            var returnValue = sut.ParseFile(new DeviceDataFoo1 (),"test.json");
            Assert.IsNotNull(returnValue);
        }

        [Test]
        public void Wrong_File_Format_Returns_Empty()
        {
            var mockFileOperation = new Mock<IFileOperation>();
            var jsonString = "{\r\n  \"PartnerId\": 1,\r\n  " +
                "\"PartnerName\": \"Foo1\",\r\n  " +
                "\"Trackers\": [\r\n    {\r\n      \"Id\": 1,\r\n      \"Model\": \"ABC-100\",\r\n      \"ShipmentStartDtm\": \"08-17-2020 10:30:00\",\r\n      \"Sensors\": [\r\n        {\r\n          \"Id\": 100,\r\n          \"Name\": \"Temperature\",\r\n          \"Crumbs\": [\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:35:00\",\r\n              \"Value\": 22.15\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:40:00\",\r\n              \"Value\": 23.15\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:45:00\",\r\n              \"Value\": 24.15\r\n            }\r\n          ]\r\n        },\r\n        {\r\n          \"Id\": 101,\r\n          \"Name\": \"Humidty\",\r\n          \"Crumbs\": [\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:35:00\",\r\n              \"Value\": 80.5\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:40:00\",\r\n              \"Value\": 81.5\r\n            },\r\n            {\r\n              \"CreatedDtm\": \"08-17-2020 10:45:00\",\r\n              \"Value\": 82.5\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(jsonString);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            mockFileOperation.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                   .Returns(() => new StreamReader(fakeMemoryStream));

            var sut = new FileManager<DeviceDataFoo1>(mockFileOperation.Object);
            var returnValue = sut.ParseFile(new DeviceDataFoo1(), "test.json");
            Assert.IsTrue(returnValue.PartnerName == null);
        }
        #endregion

        #region "Test with DeviceDataFoo2Object"

        [Test]
        public void EmptyPath_Returns_NullException_DDF2()
        {
            var fileOperation = new Mock<IFileOperation>();
            var sut = new FileManager<DeviceDataFoo2>(fileOperation.Object);
            Assert.Throws<NullReferenceException>(() => sut.ParseFile(new DeviceDataFoo2(), ""));
        }

        [Test]
        public void Correct_File_Returns_Value_DDF2()
        {
            var mockFileOperation = new Mock<IFileOperation>();
            var jsonString = "{\r\n  \"CompanyId\": 2,\r\n  \"Company\": \"Foo2\",\r\n  \"Devices\": [\r\n    {\r\n      \"DeviceID\": 1,\r\n      \"Name\": \"XYZ-100\",\r\n      \"StartDateTime\": \"08-18-2020 10:30:00\",\r\n      \"SensorData\": [\r\n        {\r\n          \"SensorType\": \"TEMP\",\r\n          \"DateTime\": \"08-18-2020 10:35:00\",\r\n          \"Value\": 32.15\r\n        },\r\n        {\r\n          \"SensorType\": \"TEMP\",\r\n          \"DateTime\": \"08-18-2020 10:40:00\",\r\n          \"Value\": 33.15\r\n        },\r\n        {\r\n          \"SensorType\": \"TEMP\",\r\n          \"DateTime\": \"08-18-2020 10:45:00\",\r\n          \"Value\": 34.15\r\n        },\r\n        {\r\n          \"SensorType\": \"HUM\",\r\n          \"DateTime\": \"08-18-2020 10:35:00\",\r\n          \"Value\": 90.5\r\n        },\r\n        {\r\n          \"SensorType\": \"HUM\",\r\n          \"DateTime\": \"08-18-2020 10:40:00\",\r\n          \"Value\": 91.5\r\n        },\r\n        {\r\n          \"SensorType\": \"HUM\",\r\n          \"DateTime\": \"08-18-2020 10:45:00\",\r\n          \"Value\": 92.5\r\n        }\r\n      ]\r\n    }]}";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(jsonString);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            mockFileOperation.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                   .Returns(() => new StreamReader(fakeMemoryStream));

            var sut = new FileManager<DeviceDataFoo2>(mockFileOperation.Object);
            var returnValue = sut.ParseFile(new DeviceDataFoo2(), "test.json");
            Assert.IsNotNull(returnValue);
        }

        [Test]
        public void Wrong_File_Format_Returns_Empty_DDF2()
        {
            var mockFileOperation = new Mock<IFileOperation>();
            var jsonString = "{\r\n  \"CompanyId\": 2,\r\n  \"Company\": \"Foo2\",\r\n  \"Devices\": [\r\n    {\r\n      \"DeviceID\": 1,\r\n      \"Name\": \"XYZ-100\",\r\n      \"StartDateTime\": \"08-18-2020 10:30:00\",\r\n      \"SensorData\": [\r\n        {\r\n          \"SensorType\": \"TEMP\",\r\n          \"DateTime\": \"08-18-2020 10:35:00\",\r\n          \"Value\": 32.15\r\n        },\r\n        {\r\n          \"SensorType\": \"TEMP\",\r\n          \"DateTime\": \"08-18-2020 10:40:00\",\r\n          \"Value\": 33.15\r\n        },\r\n        {\r\n          \"SensorType\": \"TEMP\",\r\n          \"DateTime\": \"08-18-2020 10:45:00\",\r\n          \"Value\": 34.15\r\n        },\r\n        {\r\n          \"SensorType\": \"HUM\",\r\n          \"DateTime\": \"08-18-2020 10:35:00\",\r\n          \"Value\": 90.5\r\n        },\r\n        {\r\n          \"SensorType\": \"HUM\",\r\n          \"DateTime\": \"08-18-2020 10:40:00\",\r\n          \"Value\": 91.5\r\n        },\r\n        {\r\n          \"SensorType\": \"HUM\",\r\n          \"DateTime\": \"08-18-2020 10:45:00\",\r\n          \"Value\": 92.5\r\n        }\r\n      ]\r\n    }";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(jsonString);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            mockFileOperation.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                   .Returns(() => new StreamReader(fakeMemoryStream));

            var sut = new FileManager<DeviceDataFoo2>(mockFileOperation.Object);
            var returnValue = sut.ParseFile(new DeviceDataFoo2(), "test.json");
            Assert.IsTrue(returnValue.Company == null);
        }
        #endregion



    }
}
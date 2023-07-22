using DeviceDataProcessing.Models;
using NUnit.Framework;

namespace DeviceDataProcessingTest.Business
{
    public class DeviceDataFoo2MgrTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SensorDataNotDefined_Returns_Avg_null()
        {
            var deviceDataFoo2 = new DeviceDataFoo2();
            deviceDataFoo2.Company = "test";
            deviceDataFoo2.CompanyId = 1;
            var dvc = new Device();
            dvc.DeviceID = 100;
            dvc.StartDateTime = "07-22-2023 12:12:12";
            var ssrData = new List<SensorData>();
            
            var tkr = new Tracker();
            var sensor = new SensorData();
            sensor.SensorType = "";
            sensor.DateTime = "07-22-2023 12:12:12";
            sensor.Value =101.00;
            var sens = new List<Sensor>();
            ssrData.Add(sensor);
            dvc.SensorData = ssrData;
            tkr.Sensors = sens;
            var dvcs = new List<Device>();
            dvcs.Add(dvc);
            deviceDataFoo2.Devices= dvcs;
            var sut = new DeviceDataFoo2Mgr(deviceDataFoo2);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.First().AverageHumidity == null && valueReturned.First().AverageTemperature == null);
        }
        [Test]
        public void Map_With_TemperatureData_Returns_Value()
        {
            var deviceDataFoo2 = new DeviceDataFoo2();
            deviceDataFoo2.Company = "test";
            deviceDataFoo2.CompanyId = 1;
            var dvc = new Device();
            dvc.DeviceID = 100;
            dvc.StartDateTime = "07-22-2023 12:12:12";
            var ssrData = new List<SensorData>();

            var tkr = new Tracker();
            var sensor = new SensorData();
            sensor.SensorType = "TEMP";
            sensor.DateTime = "07-22-2023 12:12:12";
            sensor.Value = 101.00;
            var sens = new List<Sensor>();
            ssrData.Add(sensor);
            dvc.SensorData = ssrData;
            tkr.Sensors = sens;
            var dvcs = new List<Device>();
            dvcs.Add(dvc);
            deviceDataFoo2.Devices = dvcs;
            var sut = new DeviceDataFoo2Mgr(deviceDataFoo2);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.Count == 1 && valueReturned.First().TemperatureCount == 1);
        }

        [Test]
        public void Map_With_HumidityData_Returns_Value()
        {
            var deviceDataFoo2 = new DeviceDataFoo2();
            deviceDataFoo2.Company = "test";
            deviceDataFoo2.CompanyId = 1;
            var dvc = new Device();
            dvc.DeviceID = 100;
            dvc.StartDateTime = "07-22-2023 12:12:12";
            var ssrData = new List<SensorData>();

            var tkr = new Tracker();
            var sensor = new SensorData();
            sensor.SensorType = "HUM";
            sensor.DateTime = "07-22-2023 12:12:12";
            sensor.Value = 101.00;
            var sens = new List<Sensor>();
            ssrData.Add(sensor);
            dvc.SensorData = ssrData;
            tkr.Sensors = sens;
            var dvcs = new List<Device>();
            dvcs.Add(dvc);
            deviceDataFoo2.Devices = dvcs;
            var sut = new DeviceDataFoo2Mgr(deviceDataFoo2);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.Count == 1 && valueReturned.First().HumidityCount == 1);
        }

        [Test]
        public void Map_With_Data_Returns_Value()
        {
            var deviceDataFoo2 = new DeviceDataFoo2();
            deviceDataFoo2.Company = "test";
            deviceDataFoo2.CompanyId = 1;
            var dvc = new Device();
            dvc.DeviceID = 100;
            dvc.StartDateTime = "07-22-2023 12:12:12";
            var ssrData = new List<SensorData>();

            var tkr = new Tracker();
            var sensor = new SensorData();
            sensor.SensorType = "HUM";
            sensor.DateTime = "07-22-2023 12:12:12";
            sensor.Value = 101.00;
            var sens = new List<Sensor>();
            ssrData.Add(sensor);
            dvc.SensorData = ssrData;
            tkr.Sensors = sens;
            var dvcs = new List<Device>();
            dvcs.Add(dvc);
            deviceDataFoo2.Devices = dvcs;
            var sut = new DeviceDataFoo2Mgr(deviceDataFoo2);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.Count == 1);
        }
    }
}

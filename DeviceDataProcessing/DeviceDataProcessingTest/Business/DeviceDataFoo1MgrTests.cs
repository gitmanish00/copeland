using DeviceDataProcessing.Models;
using NUnit.Framework;

namespace DeviceDataProcessingTest.Business
{
    public class DeviceDataFoo1MgrTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SensorDataNotDefined_Returns_Avg_null()
        {
            var deviceDataFoo1 = new DeviceDataFoo1();
            deviceDataFoo1.PartnerName = "test";
            deviceDataFoo1.PartnerId = 1;
            var crumb = new Crumb();
            crumb.Value = 100;
            crumb.CreatedDtm = "07-22-2023 12:12:12";
            var crumbs = new List<Crumb>();
            crumbs.Add(crumb);
            var tkr = new Tracker();
            var sensor = new Sensor();
            sensor.Id = 100;
            sensor.Name = "";
            sensor.Crumbs =crumbs;
            var sens = new List<Sensor>();
            sens.Add(sensor);
            tkr.Sensors = sens;
            var tkrs = new List<Tracker>();
            tkrs.Add(tkr);
                deviceDataFoo1.Trackers= tkrs;
            var sut = new DeviceDataFoo1Mgr(deviceDataFoo1);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.First().AverageHumidity == null && valueReturned.First().AverageTemperature == null);
        }
        [Test]
        public void Map_With_TemperatureData_Returns_Value()
        {
            var deviceDataFoo1 = new DeviceDataFoo1();
            deviceDataFoo1.PartnerName = "test";
            deviceDataFoo1.PartnerId = 1;
            var crumb = new Crumb();
            crumb.Value = 100;
            crumb.CreatedDtm = "07-22-2023 12:12:12";
            var crumbs = new List<Crumb>();
            crumbs.Add(crumb);
            var tkr = new Tracker();
            var sensor = new Sensor();
            sensor.Id = 100;
            sensor.Name = "Temperature";
            sensor.Crumbs = crumbs;
            var sens = new List<Sensor>();
            sens.Add(sensor);
            tkr.Sensors = sens;
            var tkrs = new List<Tracker>();
            tkrs.Add(tkr);
            deviceDataFoo1.Trackers = tkrs;
            var sut = new DeviceDataFoo1Mgr(deviceDataFoo1);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.Count == 1 && valueReturned.First().TemperatureCount == 1);
        }

        [Test]
        public void Map_With_HumidityData_Returns_Value()
        {
            var deviceDataFoo1 = new DeviceDataFoo1();
            deviceDataFoo1.PartnerName = "test";
            deviceDataFoo1.PartnerId = 1;
            var crumb = new Crumb();
            crumb.Value = 100;
            crumb.CreatedDtm = "07-22-2023 12:12:12";
            var crumbs = new List<Crumb>();
            crumbs.Add(crumb);
            var tkr = new Tracker();
            var sensor = new Sensor();
            sensor.Id = 100;
            sensor.Name = "Humidity";
            sensor.Crumbs = crumbs;
            var sens = new List<Sensor>();
            sens.Add(sensor);
            tkr.Sensors = sens;
            var tkrs = new List<Tracker>();
            tkrs.Add(tkr);
            deviceDataFoo1.Trackers = tkrs;
            var sut = new DeviceDataFoo1Mgr(deviceDataFoo1);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.Count == 1 && valueReturned.First().HumidityCount == 1);
        }

        [Test]
        public void Map_With_Data_Returns_Value()
        {
            var deviceDataFoo1 = new DeviceDataFoo1();
            deviceDataFoo1.PartnerName = "test";
            deviceDataFoo1.PartnerId = 1;
            var crumb = new Crumb();
            crumb.Value = 100;
            crumb.CreatedDtm = "07-22-2023 12:12:12";
            var crumbs = new List<Crumb>();
            crumbs.Add(crumb);
            var tkr = new Tracker();
            var sensor = new Sensor();
            sensor.Id = 100;
            sensor.Name = "Temperature";
            sensor.Crumbs = crumbs;
            var sens = new List<Sensor>();
            sens.Add(sensor);
            tkr.Sensors = sens;
            var tkrs = new List<Tracker>();
            tkrs.Add(tkr);
            deviceDataFoo1.Trackers = tkrs;
            var sut = new DeviceDataFoo1Mgr(deviceDataFoo1);
            var valueReturned = sut.Map();
            Assert.IsTrue(valueReturned.Count == 1);
        }
    }
}

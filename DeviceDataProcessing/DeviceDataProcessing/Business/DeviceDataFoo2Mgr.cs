using DeviceDataProcessing.Models;
using DeviceDataProcessingTest.Business.Interfaces;
using System.Globalization;

namespace DeviceDataProcessingTest.Business
{
    public class DeviceDataFoo2Mgr :IDeviceDataFooMgr
    {
        private const string _dateFormat = "MM-dd-yyyy hh:mm:ss";
        private readonly DeviceDataFoo2 _ddf2;
        public DeviceDataFoo2Mgr(DeviceDataFoo2 ddf2) {
            _ddf2 = ddf2;
        }
        public List<DeviceDataMerged> Map()
        {
            var liDdfMerged = new List<DeviceDataMerged>();
            foreach (var device in _ddf2.Devices)
            {

                var ddfMerged = new DeviceDataMerged();
                var sensorType = "TEMP";
                ddfMerged.CompanyId = _ddf2.CompanyId;
                ddfMerged.CompanyName = _ddf2.Company;
                ddfMerged.DeviceId = device.DeviceID;
                ddfMerged.DeviceName = device.Name;
                ddfMerged.AverageTemperature = CalculateAverage(device.SensorData, sensorType);
                ddfMerged.TemperatureCount = device.SensorData.Where(x => x.SensorType == sensorType).Count();
                ddfMerged.FirstReadingDtm = GetReadingDtm(device.SensorData, sensorType, "FIRST");
                ddfMerged.LastReadingDtm = GetReadingDtm(device.SensorData, sensorType, "LAST");

                sensorType = "HUM";
                ddfMerged.AverageHumidity = CalculateAverage(device.SensorData, sensorType);
                ddfMerged.HumidityCount = device.SensorData.Where(x => x.SensorType == sensorType).Count();
                ddfMerged.FirstReadingDtm = GetReadingDtm(device.SensorData, sensorType, "FIRST");
                ddfMerged.LastReadingDtm = GetReadingDtm(device.SensorData, sensorType, "LAST");

                liDdfMerged.Add(ddfMerged);

            }
            return liDdfMerged;
        }
        private double? CalculateAverage(List<SensorData> sensorData, string sensorType)
        {
            if (sensorData == null || sensorData.Count == 0)
                return null;

            double? avg= 0;
            foreach (var sensor in sensorData)
            {
                if (sensor.SensorType != null && sensor.SensorType.ToUpper().Trim() == sensorType)
                    avg += sensor.Value;
            }
            if (avg > 0)
            {
                var cnt = sensorData.Where(x => x.SensorType == sensorType).Count();
                avg = cnt > 0 ? (avg / cnt) : 0;
            }
            return avg;
        }
        private DateTime? GetReadingDtm(List<SensorData> sensorData, string sensorType, string firstOrLast)
        {
            if (sensorData == null || sensorData.Count == 0)
                return null;

            var orderedList = sensorData.Where(x => x.SensorType == sensorType).OrderByDescending(x => DateTime.Parse(x.DateTime)).ToList();
            if(orderedList.Count > 0)
            {
                DateTime readingDate;
                var isParsed = false;

                if (firstOrLast.ToUpper() == "FIRST")
                    isParsed = DateTime.TryParseExact(orderedList.Last().DateTime, _dateFormat,
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None,
                          out readingDate);
                else
                    isParsed = DateTime.TryParseExact(orderedList.First().DateTime, _dateFormat,
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out readingDate);
                if (isParsed)
                    return readingDate;
                else
                    return null;
            }
            return null;
        }
    }
}

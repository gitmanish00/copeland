using DeviceDataProcessing.Models;
using DeviceDataProcessingTest.Business.Interfaces;
using System.Globalization;

namespace DeviceDataProcessingTest.Business
{
    public class DeviceDataFoo1Mgr :IDeviceDataFooMgr
    {
        private const string _dateFormat = "MM-dd-yyyy hh:mm:ss";
        private readonly DeviceDataFoo1 _ddf1;
        public DeviceDataFoo1Mgr(DeviceDataFoo1 ddf1) {
            _ddf1 = ddf1;
        }
        public List<DeviceDataMerged> Map()
        {
            var liDdfMerged = new List<DeviceDataMerged>();
            foreach (var tracker in _ddf1.Trackers)
            {
                foreach (var sensor in tracker.Sensors)
                {
                    var ddfMerged = new DeviceDataMerged();
                    ddfMerged.CompanyId = _ddf1.PartnerId;
                    ddfMerged.CompanyName = _ddf1.PartnerName;
                    ddfMerged.DeviceId = sensor.Id;
                    ddfMerged.DeviceName = sensor.Name;
                    var sensorType = "TEMPERATURE";
                    if (sensor.Name != null && sensor.Name.ToUpper().Trim() == sensorType){
                        ddfMerged.AverageTemperature = CalculateAverage(sensor.Crumbs);
                        ddfMerged.TemperatureCount = sensor.Crumbs.Count;
                        ddfMerged.FirstReadingDtm = GetReadingDtm(sensor.Crumbs, "FIRST");
                        ddfMerged.LastReadingDtm = GetReadingDtm(sensor.Crumbs, "LAST");
                    }
                    sensorType = "HUMIDTY";
                    if (sensor.Name != null && sensor.Name.ToUpper().Trim() == sensorType){
                        ddfMerged.AverageHumidity = CalculateAverage(sensor.Crumbs);
                        ddfMerged.HumidityCount = sensor.Crumbs.Count;
                        ddfMerged.FirstReadingDtm = GetReadingDtm(sensor.Crumbs, "FIRST");
                        ddfMerged.LastReadingDtm = GetReadingDtm(sensor.Crumbs, "LAST");
                    }
                    liDdfMerged.Add(ddfMerged);
                }

            }
            return liDdfMerged;
        }
        private double CalculateAverage(List<Crumb> crumbs)
        {
            if (crumbs == null || crumbs.Count == 0)
                return 0.0;

            var avgTemp = 0.0;
            foreach (var crumb in crumbs)
            {
                avgTemp += crumb.Value;
            }
            if (avgTemp > 0)
            {
                avgTemp = avgTemp / crumbs.Count;
            }
            return avgTemp;
        }
        private DateTime? GetReadingDtm(List<Crumb> crumbs, string firstOrLast)
        {
            if (crumbs == null || crumbs.Count == 0)
                return null;

            var orderedList = crumbs.OrderByDescending(x => DateTime.Parse(x.CreatedDtm)).ToList();
            DateTime readingDate;
            var isParsed = false;
            if (firstOrLast.ToUpper() == "FIRST")
                isParsed = DateTime.TryParseExact(orderedList.Last().CreatedDtm, _dateFormat,
                      CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                      out readingDate);
            else
                isParsed = DateTime.TryParseExact(orderedList.First().CreatedDtm, _dateFormat,
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None,
                           out readingDate);
            if (isParsed)
                return readingDate;
            else
                return null;
        }
    }
}

namespace DeviceDataProcessing.Models
{
    public class DeviceDataFoo2
    {
        public int CompanyId { get; set; }
        public string Company { get; set;}
        public List<Device> Devices { get; set; }

    }
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public string StartDateTime { get; set; }
        public List<SensorData> SensorData { get; set; }
    }

    public class SensorData
    {
        public string SensorType { get; set;}
        public string DateTime { get; set; }
        public double Value { get; set; }

    }
}

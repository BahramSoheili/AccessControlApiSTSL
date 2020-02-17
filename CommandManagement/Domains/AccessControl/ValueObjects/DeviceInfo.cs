using System.Collections.Generic;

namespace CommandManagement.Domains.Devices.ValueObjects
{
    public class DeviceInfo
    {
            public string deviceName { get; set; }
            public string deviceType { get; set; }
            public string deviceLocationName { get; set; }
            public string deviceBuildingName { get; set; }
            public string batteryStatus { get; set; }
            public string deviceStatus { get; set; }
            public string description { get; set; }
            public List<string> allocatedCards { get; set; }
    }
}

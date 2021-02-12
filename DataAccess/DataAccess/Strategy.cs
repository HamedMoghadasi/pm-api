using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.DataAccess
{
    public partial class Strategy
    {
        public int Id { get; set; }
        public string StrategyName { get; set; }
        public string NumberSegment { get; set; }
        public string DateRange { get; set; }
        public string TimeLimit { get; set; }
        public int Priority { get; set; }
        public bool IsEnable { get; set; }
        public string DeviceFirmwareVersion { get; set; }
        public string ServerFirmwareVersion { get; set; }
        public int UpdateSpecificationId { get; set; }

        public virtual UpdateSpecification UpdateSpecification { get; set; }
    }
}

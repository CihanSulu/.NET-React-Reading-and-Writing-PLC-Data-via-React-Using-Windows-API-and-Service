using System;
using System.Collections.Generic;

namespace Windows_Service.Entities
{
    public partial class OPlc
    {
        public int Id { get; set; }

        public string PlcName { get; set; }

        public string PlcIp { get; set; }

        public byte? PlcRack { get; set; }

        public byte? PlcSlot { get; set; }

        public DateTime? Exportdate { get; set; }
    }
}

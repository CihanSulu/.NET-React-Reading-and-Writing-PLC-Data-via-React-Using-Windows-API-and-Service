using System;
using System.Collections.Generic;

namespace Web_API.Entities
{
    public partial class ODataType
    {
        public int DataId { get; set; }

        public byte? DataPlc { get; set; }

        public string? DataName { get; set; }

        public string? DataDescription { get; set; }

        public string? DataType { get; set; }

        public byte? DataPage { get; set; }

        public string? DataAdress { get; set; }

        public DateTime? Exportdate { get; set; }
    }
}

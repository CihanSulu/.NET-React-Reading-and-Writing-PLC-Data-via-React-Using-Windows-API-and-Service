using System;
using System.Collections.Generic;

namespace Windows_Service.Entities
{
    public partial class OData
    {
        public int DataId { get; set; }

        public int? DokumNo { get; set; }

        public int? PageId { get; set; }

        public string DataJson { get; set; }

        public double? GenelAriza { get; set; }

        public double? MekanikAriza { get; set; }

        public double? ElektrikAriza { get; set; }

        public double? IsletmeAriza { get; set; }

        public DateTime? Exportdate { get; set; }
    }
}

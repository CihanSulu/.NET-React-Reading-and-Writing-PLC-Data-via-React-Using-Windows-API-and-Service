﻿using System;
using System.Collections.Generic;

namespace Web_API.Entities
{
    public partial class OError
    {
        public int ErrorId { get; set; }

        public int? PageId { get; set; }

        public double? GenelAriza { get; set; }

        public double? MekanikAriza { get; set; }

        public double? ElektrikAriza { get; set; }

        public double? IsletmeAriza { get; set; }

        public DateTime? Exportdate { get; set; }
    }
}

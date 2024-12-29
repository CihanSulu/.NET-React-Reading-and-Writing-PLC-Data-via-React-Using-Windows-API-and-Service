using System;
using System.Collections.Generic;

namespace Windows_Service.Entities
{
    public partial class OPage
    {
        public int PageId { get; set; }

        public string PageTitle { get; set; }

        public string PageDescription { get; set; }

        public DateTime? Exportdate { get; set; }
    }
}

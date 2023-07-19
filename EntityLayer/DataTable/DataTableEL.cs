using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.DataTable
{
    public class DataTableBaseModel
    {
        public int IN_ORDERCOLUMN { get; set; }
        public int IN_LIMITINDEX { get; set; }
        public string IN_ORDERDIR { get; set; }
        public int IN_STARTINDEX { get; set; }
        public bool IN_IsExport { get; set; }
    }
}

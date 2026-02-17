using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class printCode
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string proName { get; set; }
        public string template1 { get; set; }
        public string template2 { get; set; }
        public string template3 { get; set; }
        public string template4 { get; set; }
        public string proType1 { get; set; }
        public string proType2 { get; set; }

        public string productCode { get; set; }
        public string proQRCode { get; set; }
    }
}

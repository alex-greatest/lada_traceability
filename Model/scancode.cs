using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class scancode : ISCode
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string Grade { get; set; }
        public string PartName { get; set; }

        public string ScanPartCode { get; set; }
        public int checkDigit { get; set; }

    }
}

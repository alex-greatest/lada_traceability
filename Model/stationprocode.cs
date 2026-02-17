using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class stationprocode
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime time { get; set; }
        public string stationName { get; set; }
        public int productType { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public int productResult { get; set; }


    }
}

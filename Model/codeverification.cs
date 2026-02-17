using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class codeverification
    {
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string productName { get; set; } = "Test";
        public string grade { get; set; } = "";
        public string actualCode { get; set; } = "";
        public string delimiter { get; set; } = "-";
        public int mode { get; set; } = 1;
        public int startindex { get; set; } = 0;
        public int codelength { get; set; } = 1;
    }
}

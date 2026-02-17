using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class product
    {
        public product() { }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        public string productName { get; set; }
        public int productID { get; set; }
        public string station1 { get; set; }
        public string station2 { get; set; }
        public string station3 { get; set; }
        public string station4 { get; set; }
        public string station5 { get; set; }
        public string station6 { get; set; }
        public string station7 { get; set; }
        public string station8 { get; set; }
        public string station9 { get; set; }
        public string station10 { get; set; }
        public string station11 { get; set; }
        public string station12 { get; set; }
        public string station13 { get; set; }
        public string station14 { get; set; }
    }
}

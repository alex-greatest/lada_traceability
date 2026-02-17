using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class stationModel
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string time { get; set; }
        public int productType{ get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string data1 { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
        public string data5 { get; set; }
        public string data6 { get; set; }
        public string data7 { get; set; }
        public string data8 { get; set; }
        public string data9 { get; set; }
        public string data10 { get; set; }
        public string data11 { get; set; }
        public string data12 { get; set; }
        public string data13 { get; set; }
        public string data14 { get; set; }
        public string data15 { get; set; }
        public string data16 { get; set; }
        public string data17 { get; set; }
        public string data18 { get; set; }
        public string data19 { get; set; }
        public string data20 { get; set; }
        public string barcode1 { get; set; }
        public string barcode2 { get; set; }
        public string barcode3 { get; set; }
        public string barcode4 { get; set; }
        public string barcode5 { get; set; }
        public string barcode6 { get; set; }
        public string barcode7 { get; set; }
        public string barcode8 { get; set; }
        public int result { get; set; }
    }
}

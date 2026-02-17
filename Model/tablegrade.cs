using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public class tablegrade
    {
        public tablegrade() { }
        public tablegrade(List<string> gradelist) {
            var type = typeof(tablegrade);
            for (int i = 0; i < gradelist.Count; i++)
            {
                string vname = "grade" + (i + 1).ToString("D2");
                var prop = type.GetProperty(vname);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(this, gradelist[i].ToString());
                }
            }
        }
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string grade01 { get; set; } = "";
        public string grade02 { get; set; } = "";
        public string grade03 { get; set; } = "";
        public string grade04 { get; set; } = "";
        public string grade05 { get; set; } = "";
        public string grade06 { get; set; } = "";
        public string grade07 { get; set; } = "";
        public string grade08 { get; set; } = "";
        public string grade09 { get; set; } = "";
        public string grade10 { get; set; } = "";
    }
}

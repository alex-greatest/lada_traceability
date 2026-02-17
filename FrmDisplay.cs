using Org.BouncyCastle.Asn1.X509;
using Review.Model;
using Review.Repository;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace Review
{
    public partial class FrmDisplay : Form
    {
        private string production;
        private string grade;
        private float plan;
        public string info;

        private float actual;
        public float ok;
        public float ng;
        public FrmDisplay()
        {
            InitializeComponent();
            // 找到最左侧的屏幕
            Screen leftMostScreen = Screen.AllScreens.OrderBy(s => s.Bounds.Left).First();

            // 设置窗口位置，使其出现在最左侧屏幕的左上角
            this.Location = leftMostScreen.Bounds.Location;
        }
        public FrmDisplay(string production,string grade ,string leader,string plan, string info)
        {
            InitializeComponent();
            // 订阅事件
            Review.Utils.AppContext.OnProductCountChanged += count =>
            {
                actual = count.ProductCount;
                ok = count.OKProductCount;
                ng = count.NGProductCount;
            };
            // 找到最左侧的屏幕
            Screen leftMostScreen = Screen.AllScreens.OrderBy(s => s.Bounds.Left).First();
            // 设置窗口位置，使其出现在最左侧屏幕的左上角
            //this.Location = leftMostScreen.Bounds.Location;
            this.Location = new Point(-1920, 0);
            this.production = production;
            this.grade = grade;
            labelLeader.Text = leader;
            this.plan = Convert.ToInt32(plan);
            this.info = info;
        }
        public static void setprocount(float count1, float count2) {
            /*ok = Convert.ToInt32(count1);
            ng = Convert.ToInt32(count2);*/
        }

        /// <summary>
        /// 产品达成率char2
        /// </summary>
        public void DoughNut1_Design()
        {
            chart2.Series[0].Points.Clear();
            int idxA = chart2.Series[0].Points.AddY(plan - actual);
            DataPoint pointA = chart2.Series[0].Points[idxA];
            pointA.Label = "";
            //点2
            int idxB = chart2.Series[0].Points.AddY(actual);
            DataPoint pointB = chart2.Series[0].Points[idxB];
            pointB.Label = "";
            // pointB.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            chart2.Series[0].IsValueShownAsLabel = true;
            chart2.Series[0].LabelForeColor = Color.White;

        }

        /// <summary>
        /// 产品不良率char3
        /// </summary>
        public void DoughNut2_Design()
        {
            chart3.Series[0].Points.Clear();
            int idxA = chart3.Series[0].Points.AddY(ng);
            DataPoint pointA = chart3.Series[0].Points[idxA];
            //pointA.LabelBackColor = Color.Red;
            pointA.Color = Color.Red;
            pointA.Label = "";
            // pointA.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //点2
            int idxB = chart3.Series[0].Points.AddY(ok >= 1 ? ok : 1);
            DataPoint pointB = chart3.Series[0].Points[idxB];
            //pointB.LabelBackColor = Color.Red;
            pointB.Color = Color.Green;
            pointB.Label = "";
            // pointB.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            chart3.Series[0].IsValueShownAsLabel = true;
            chart3.Series[0].LabelForeColor = Color.White;
           
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DoughNut1_Design();
            DoughNut2_Design();
            UpdateDateTimeLabels();
        }
        private void UpdateLabel(Label label, string newValue)
        {
            if (label.Text != newValue)
            {
                label.Text = newValue;
            }
        }

        private void UpdateDateTimeLabels()
        {
            var now = DateTime.Now;
            var culture = new System.Globalization.CultureInfo("ru-RU");

            string timeString = $"{MyTools.NumberString(now.Hour)}:{MyTools.NumberString(now.Minute)}";
            string dateString = now.ToString("d", culture);
            string weekString = now.ToString("dddd", culture);

            UpdateLabel(labelTime, timeString);
            UpdateLabel(labelDate, dateString);
            UpdateLabel(labelWeek, weekString);
            UpdateLabel(labelActual, actual.ToString());
            UpdateLabel(labelOK, ok.ToString());
            UpdateLabel(labelNG, ng.ToString());
            UpdateLabel(labelType, production + "--" + grade);
            if(actual != 0)
            UpdateLabel(labelNGP, (Math.Round(ng / actual, 4) * 100).ToString() + "%");
            if(plan != 0)
            UpdateLabel(labelFinishP, (Math.Round(actual / plan , 4) * 100).ToString() + "%");
        }
        private void FrmDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) {
                this.Close();
            }
        }
        private void initialStandardCode() {
            tableLayoutPanel1.RowCount = 0;
            var te = SqlSugarHelper.GetDataList<standardcode>(s => s.ProductName == production && s.Grade == grade);
            foreach (var scode in te) {
                if (te.Count * 2 > tableLayoutPanel1.RowCount) { 
                    // 2. 设置该行的比例（比如占 30%）
                    tableLayoutPanel1.RowCount += 2;

                    float percent = 100F / tableLayoutPanel1.RowCount;

                    // 更新所有行的比例，让它们平分
                    tableLayoutPanel1.RowStyles.Clear();
                    for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                    {
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
                    }
                }

                // 创建一个 Label
                Label lbl = new Label();
                lbl.Text = scode.PartName;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Dock = DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
                lbl.ForeColor = Color.White;

                Label lbl2 = new Label();
                lbl2.Text = scode.PartCode;
                lbl2.TextAlign = ContentAlignment.MiddleCenter;
                lbl2.Dock = DockStyle.Fill;
                lbl2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
                lbl2.ForeColor = Color.White;

                // 添加到新行
                tableLayoutPanel1.Controls.Add(lbl, 0, tableLayoutPanel1.RowCount - 2);  // 第0列
                tableLayoutPanel1.Controls.Add(lbl2, 0, tableLayoutPanel1.RowCount - 1);  // 第0列
            }
            
        }
        private void FrmDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void FrmDisplay_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(production) && !string.IsNullOrEmpty(grade))
            {
                initialStandardCode();
            }
        }
    }
}

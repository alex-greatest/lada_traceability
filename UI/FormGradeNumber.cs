using Review.Model;
using Review.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Gaming.Preview.GamesEnumeration;

namespace Review.UI
{
    public partial class FormGradeNumber : Form
    {
        List<string> gradeList = new List<string>();
        Dictionary<string , string> gradeDic = new Dictionary<string , string>();
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        public FormGradeNumber()
        {
            InitializeComponent();
        }
        public FormGradeNumber(ResourceManager resManager, CultureInfo currentCulture)
        {
            InitializeComponent();
            this.resManager = resManager;
            this.currentCulture = currentCulture;
            UpdateLanguage(currentCulture);
            initGradelist();
            initialProName();
        }
        private void initialProName()
        {
            cbmode.Items.Clear();
            string selectSql = $"select productName from product";
            var s = DBHelper.GetDataList(selectSql, 0);
            if (s != null) cbproname.DataSource = s;
            cbmode.Items.Add("1");
            cbmode.Items.Add("2");
        }
        private void initGradelist() {
            var grade = SqlSugarHelper.GetDataList<tablegrade>(t => t.Id == 1);
            if (grade.Count > 0) {
                var type = typeof(tablegrade);
                for (int i = 1; i < 11; i++)
                {
                    string vname = "grade" + (i).ToString("D2");
                    var prop = type.GetProperty(vname);
                    if (prop != null && prop.CanWrite)
                    {
                        var va = prop.GetValue(grade[0]).ToString();
                        if (!string.IsNullOrWhiteSpace(va)) {
                            gradeList.Add(va);
                        }
                    }
                }
            }
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (currentCulture.Name == "zh-CN")
            {
            }
            else if (currentCulture.Name == "ru-RU")
            {
            }
            // 更新窗体文本
            tabControl1.TabPages[0].Text = resManager.GetString("生产看板ToolStripMenuItem");
            tabControl1.TabPages[1].Text = resManager.GetString("生产看板ToolStripMenuItem");
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            saveOrUpdate();
        }

        private void FormGradeNumber_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveOrUpdate();
        }
        private void saveOrUpdate() {
            if (gradeList.Count > 0)
            {
                var grade = new tablegrade(gradeList);

                var check = SqlSugarHelper.CheckDataHas<tablegrade>(t => t.Id == 1);
                if (check) { SqlSugarHelper.UpdateByCondition<tablegrade>(grade, t => t.Id == 1); }
                else { SqlSugarHelper.InsertEntity(grade); }
            }
            else {
                MessageBox.Show("当前档次号列表为空");
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            gradeList.Remove(cbGradeList.Text);
            cbGradeList.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gradeList.Add(tbGrade.Text);
            tbGrade.Text = "";
        }

        private void cbGradeList_Click(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            cb.Items.Clear();
            foreach (var gt in gradeList) {
                cb.Items.Add(gt);
            }
        }

        private void FormGradeNumber_Click(object sender, EventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Location = new Point(161, 6);
                panel1.Visible = true;
            }
            else { panel1.Visible = false; }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (true)
            {
                var veri = new codeverification() {
                    productName = cbproname.Text,
                    grade = cbgrade2.Text,
                    mode = int.Parse(cbmode.Text),
                    startindex = int.Parse(tbstart.Text),
                    delimiter = tbcut.Text,
                    codelength = tbactualgd.TextLength,
                    actualCode = tbactualgd.Text
                };

                var check = SqlSugarHelper.CheckDataHas<codeverification>(t => t.productName == cbproname.Text && t.grade == cbGradeList.Text);
                if (check) { SqlSugarHelper.UpdateByCondition<codeverification>(veri, t => t.productName == cbproname.Text && t.grade == cbGradeList.Text); }
                else { SqlSugarHelper.InsertEntity(veri); }
            }
            else
            {
                MessageBox.Show("当前档次号列表为空");
            }
        }

        private void cbproname_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbgrade2.Text) && !string.IsNullOrEmpty(cbproname.Text)) {
                var code = SqlSugarHelper.GetDataList<codeverification>(c => c.productName == cbproname.Text && c.grade == cbgrade2.Text);
                if (code.Count == 1)
                {
                    tbactualgd.Text = code[0].actualCode;
                    cbmode.Text = code[0].mode.ToString();
                    tbstart.Text = code[0].startindex.ToString();
                    tbcut.Text = code[0].delimiter;
                }
                else {
                    tbactualgd.Text = "";
                    cbmode.Text = "";
                    tbstart.Text = "";
                    tbcut.Text = "";
                }
            }
        }
    }
}

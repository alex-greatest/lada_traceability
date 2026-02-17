using MySql.Data.MySqlClient;
using System;
using Review.Utils;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Globalization;
using System.Threading;
using System.Resources;
using Review.Model;
using Review.Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Review
{
    public partial class FrmSetPrintCode : Form
    {
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        List<printCode> pcodelist;
        public FrmSetPrintCode(CultureInfo culture , ResourceManager resource)
        {
            InitializeComponent();
            this.resManager = resource;
            this.currentCulture = culture;
            FillcomProduct();
            UpdateLanguage(this.currentCulture);
        }
        string s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16;

        private void FrmSetPrintCode_Activated(object sender, EventArgs e)
        {
            
        }

        private void comProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                pcodelist = SqlSugarHelper.GetDataList<printCode>(p => p.proName == comProduct.Text);
                if (pcodelist.Count == 1)
                {
                    initialCodeText(pcodelist);
                }
            }
            catch (MySqlException ex) {
                MessageBox.Show($"数据库异常   {ex}");
            }
        }

        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            if (currentCulture.Name == "zh-CN")
            {
                s1 = "设置的数据不能为空";
            }
            else if (currentCulture.Name == "ru-RU")
            {
                s1 = "Штрих-код не может быть пустым";
            }
            // 更新窗体文本
            label1.Text = resManager.GetString("label1");
            label2.Text = resManager.GetString("SPClabel2");
            label3.Text = resManager.GetString("SPClabel3");
            save.Text = resManager.GetString("SPCbtnSave");
        }
        private void FillcomProduct()
        {
            try { 
            var PNL = SqlSugarHelper.GetColumnList<product , string>(p => p.productName);
            comProduct.Items.Clear();
            for (int i = 0; i < PNL.Count; i++)
            {
                comProduct.Items.Add(PNL[i]);
            }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"数据库异常   {ex}");
            }
        }
        private void initialCodeText(List<printCode> pcodelist) {
            //初始化  产品代号
            txtPType.Text = pcodelist[0].proType1;
            //初始化  条码前缀
            txtFrontCode.Text = pcodelist[0].proType2;
        }
        private void saveOrUpdatePrintCode() {
            try
            {
                var check = SqlSugarHelper.CheckDataHas<printCode>(p => p.proName == comProduct.Text);
                string newCode = null;
                if (check)
                {
                    var productCode = pcodelist[0].productCode;
                    newCode = utils.GenerateNextCode(productCode , true);
                }
                else
                {
                    newCode = utils.GenerateNextCode("" , true);
                }
                var pc = new printCode()
                {
                    proName = comProduct.Text,
                    proType1 = txtPType.Text,
                    proType2 = txtFrontCode.Text,
                    productCode = newCode,
                    template1 = new string('1', txtPType.Text.Length),
                    template3 = new string('3', txtFrontCode.Text.Length + newCode.Length + 1),
                    template4 = new string('4', txtPType.Text.Length + txtFrontCode.Text.Length + newCode.Length + 2),
                };
                var count = SqlSugarHelper.printCodeUpOrInEntity(pc , check);
                if (count > 0)
                {
                    MessageBox.Show("OK");
                }
                else { 
                    MessageBox.Show("NOK");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"数据库异常   {ex}");
            }
            catch (ArgumentOutOfRangeException ex) { 
                MessageBox.Show($"条码长度异常   {ex}");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!new string[] { comProduct.Text, txtFrontCode.Text, txtPType.Text }.AnyStringEmpty())
                saveOrUpdatePrintCode();
            else {
                MessageBox.Show(s1);
            }
        }
    }
}

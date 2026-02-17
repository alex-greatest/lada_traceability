using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public class ExportDGVToExcel
    {
        private const int OLDOFFICEVESION = -4143;
        private const int NEWOFFICEVESION = 56;
        /// <summary>
        /// DataGridView导出Excel
        /// </summary>
        /// <param name="strCaption">Excel文件中的标题
        /// <param name="myDGV">DataGridView 控件
        /// <returns>0:成功;1:DataGridView中无记录;2:Excel无法启动;100:Cancel;9999:异常错误</returns>
        public int ExportExcel(string strCaption, DataGridView myDGV, SaveFileDialog saveFileDialog)
        {
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Export Excel File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName == "")
                {
                    MessageBox.Show("请输入保存文件名！");
                    saveFileDialog.ShowDialog();
                }
                // 列索引，行索引，总列数，总行数
                int ColIndex = 0, RowIndex = 0;
                int ColCount = myDGV.ColumnCount, RowCount = myDGV.RowCount;
                if (myDGV.RowCount == 0)
                {
                    return 1;
                }
                // 创建Excel对象
                Microsoft.Office.Interop.Excel.Application xlApp = new ApplicationClass();
                if (xlApp == null)
                {
                    return 2;
                }
                try
                {
                    // 创建指定的文化信息实例
                    IFormatProvider formatProvider = new CultureInfo("ru-RU");

                    // 创建Excel工作薄
                    Workbook xlBook = xlApp.Workbooks.Add(true);
                    Worksheet xlSheet = (Worksheet)xlBook.Worksheets[1];
                    //Get excel Version //实测没有这一句也可以，我这边有这个的话会报错。
                    string Version = xlApp.Version;
                    //保存excel文件的格式
                    int FormatNum;
                    /*if (Convert.ToDouble(Version, formatProvider) < 12)
                    {
                        //使用Excel 97-2003
                        FormatNum = OLDOFFICEVESION;
                    }
                    else
                    {*/
                        //使用 excel 2007或更新
                        FormatNum = NEWOFFICEVESION;
                    //}
                    // 设置标题，标题所占的单元格数与DataGridView中的列数相同
                    Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, ColCount]);
                    //range.MergeCells = true;
                    xlApp.ActiveCell.FormulaR1C1 = strCaption;
                    xlApp.ActiveCell.Font.Size = 10;
                    xlApp.ActiveCell.Font.Bold = true;
                    xlApp.ActiveCell.HorizontalAlignment = Constants.xlCenter;
                    // 创建缓存数据
                    object[,] objData = new object[RowCount + 1, ColCount];
                    //获取列标题
                    foreach (DataGridViewColumn col in myDGV.Columns)
                    {
                        objData[RowIndex, ColIndex++] = col.HeaderText;
                    }
                    // 获取数据
                    for (RowIndex = 1; RowIndex <= RowCount; RowIndex++)
                    {
                        for (ColIndex = 0; ColIndex < ColCount; ColIndex++)
                        {
                            //验证DataGridView单元格中的类型,如果是string或是DataTime类型,则在放入缓存时在该内容前加入" ";
                            if (myDGV[ColIndex, RowIndex - 1].ValueType == typeof(string)
                                || myDGV[ColIndex, RowIndex - 1].ValueType == typeof(DateTime))
                            {
                                objData[RowIndex, ColIndex] = "";
                                if (myDGV[ColIndex, RowIndex - 1].Value != null)
                                {
                                    objData[RowIndex, ColIndex] = "" + myDGV[ColIndex, RowIndex - 1].Value.ToString();
                                }
                            }
                            else
                            {
                                objData[RowIndex, ColIndex] = myDGV[ColIndex, RowIndex - 1].Value;
                            }
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    // 写入Excel(xlApp.Cells[RowCount+1, ColCount]);)
                    range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[RowCount + 2, ColCount]);
                    //添加一个格式声明,使得导出数据为10进制，非科学记数法。
                    range.NumberFormatLocal = "@";
                    range.Value2 = objData;
                    xlBook.Saved = true;
                    xlBook.SaveAs(saveFileDialog.FileName, FormatNum);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Err:" + err.Message);
                    return 9999;
                }
                finally
                {
                    xlApp.Quit();
                    GC.Collect(); //强制回收
                }
                return 0;
            }
            return 100;
        }
    }
}
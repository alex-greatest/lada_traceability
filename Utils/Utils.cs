using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Utils
{
    public static class utils
    {
        /// <summary>
        /// 判断字符串数组中的所有字符串是否为 null、空或空白（含空格）
        /// </summary>
        public static bool AllStringsEmpty(this IEnumerable<string> strings)
        {
            return strings.All(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// 判断字符串数组中是否有至少一个非空字符串（不为 null 或空白）
        /// </summary>
        public static bool AnyStringNotEmpty(this IEnumerable<string> strings)
        {
            return strings.Any(s => !string.IsNullOrWhiteSpace(s));
        }
        /// <summary>
        /// 判断字符串数组中是否有至少一个空字符串（为 null 或空白）
        /// </summary>
        public static bool AnyStringEmpty(this IEnumerable<string> strings)
        {
            return strings.Any(s => string.IsNullOrWhiteSpace(s));
        }
        public static string GenerateNextCode(string currentCode , bool init)
        {
            try
            {
                string datePart = "";
                int sequence = 1;
                // 获取当前日期并格式化为yyyyMMdd
                //string today = DateTime.Now.ToString("ddMMyyHHmm");
                string today = DateTime.Now.ToString("dd.MM.yy. HH:mm ");
                if (currentCode.Length == 20)
                {
                    // 提取当前编号中的日期部分
                    datePart = currentCode.Substring(0, 8);
                    // 提取编号的序列部分，并转换为整数
                    sequence = int.Parse(currentCode.Substring(17));
                }
                if (datePart == DateTime.Now.ToString("dd.MM.yy"))
                {
                    // 日期相同，序列号加1
                    sequence = init ? sequence : sequence++;
                }
                else
                {
                    // 日期不同，重置序列号为1
                    sequence = 1;
                }

                // 生成新的编号，序列号保持4位，不足补零
                Console.WriteLine(today + sequence.ToString("D4"));

                return today + sequence.ToString("D4");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.ToString());
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Utils
{
    public static class AppContext
    {
        // 产品统计结构体
        public struct TotalProCount
        {
            public int ProductCount { get;  set; }
            public int OKProductCount { get;  set; }
            public int NGProductCount { get;  set; }

            public void Reset()
            {
                _productCount = 0;
                _okProductCount = 0;
                _ngProductCount = 0;
            }
        }
        private static int _productCount = 0;
        private static int _ngProductCount = 0;
        private static int _okProductCount = 0;
        // 公共只读属性
        public static TotalProCount Total => new TotalProCount
        {
            ProductCount = _productCount,
            OKProductCount = _okProductCount,
            NGProductCount = _ngProductCount
        };
        public static void IncrementProductCount(bool isOk)
        {
            if (isOk)
            {
                _okProductCount++;
            }
            else
            {
                _ngProductCount++;
            }
            _productCount++;

            // 触发事件，把完整统计信息传出去
            OnProductCountChanged?.Invoke(Total);
        }

        // 事件：统计值改变时触发
        public static event Action<TotalProCount> OnProductCountChanged;
    }
}

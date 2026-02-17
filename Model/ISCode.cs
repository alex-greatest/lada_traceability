using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model
{
    public interface ISCode
    {
        string ProductName { get; }
        string PartName { get; }
        string Grade { get; }
    }
}

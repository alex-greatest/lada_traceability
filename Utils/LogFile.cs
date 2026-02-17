using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Utils
{
    public static class LogFile
    {
        public static log4net.ILog Operationlog = log4net.LogManager.GetLogger("OperationLogger");
        public static log4net.ILog Errorlog = log4net.LogManager.GetLogger("ErrorLogger");
        public static log4net.ILog NGPROlog = log4net.LogManager.GetLogger("NGPROLogger");
    }
}

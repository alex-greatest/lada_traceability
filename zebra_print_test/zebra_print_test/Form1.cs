using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace zebra_print_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string printerIp = "192.168.1.45";
            string zplCommand = "^XA^FO100,100^ADN,36,20^FDHello, World!^FS^XZ";
            string zl = "^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR6,6~SD20^JUS^LRN^CI0^XZ , ^XA ,^MMT , ^PW480 ,^LL0320 ,^LS0 ,^BY1,3,64^FT158,169^BCN,,Y,N ,^FD>;123456789012^FS ,^PQ1,0,1,Y^XZ";

            try
            {
                Connection connection = new TcpConnection(printerIp, TcpConnection.DEFAULT_ZPL_TCP_PORT);
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                printer.SendCommand(zl);
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending ZPL to printer: " + ex.Message);
            }
        }
        int[] num = new int[1];
        int n = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            
            try {
                Console.WriteLine(num[n]);
            }
            catch (IndexOutOfRangeException ex) {
                Console.WriteLine(ex.Message);
            }
            n++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            n = 0;
        }
    }
}

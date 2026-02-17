using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class FrmMainTest : Form
    {
        List<Socket> clientList = new List<Socket>();
        public FrmMainTest()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;//关闭跨线程访问，这个虽然可以运行，但是是掩耳盗铃，有问题
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse(txtIP.Text),Int32.Parse(txtPort.Text)));
            server.Listen(10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptSocketClient), server);
        }
        private void AcceptSocketClient(object obj)
        {
            Socket server = obj as Socket;
            while (true)
            {
                Socket client = server.Accept();
                clientList.Add(client);
                AppendToMainBoardText(string.Format("客户端链接上来了：{0}", client.LocalEndPoint));
                ThreadPool.QueueUserWorkItem(ReceiveData, client);
            }
        }

        //处理跨线程访问
        #region
        private void AppendToMainBoardText(string txt)
        {
            if (txtMainBoard.InvokeRequired)
            {
                txtMainBoard.BeginInvoke(new Action<string>((s) =>
                {
                    txtMainBoard.Text = txtMainBoard.Text + "\r\n"  + s;
                }), txt);
            } else
            {
                //txtMainBoard.Text = txt + "\r\n" + txtMainBoard.Text;
               // txtMainBoard.Text = txt + "\r\n" + txtMainBoard.Text;
            }
        }
        #endregion

        private void ReceiveData(object state)
        {
            Socket client = state as Socket;
            byte[] data = new byte[2 * 1024];
            while (true)
            {
                int len = 0;
                try
                {
                    len = client.Receive(data, SocketFlags.None);
                }
                catch
                {
                    //客户端产生异常了
                    AppendToMainBoardText(string.Format("客户端：{0}非正常退出了", client.LocalEndPoint.ToString()));
                    clientList.Remove(client);
                    //关闭连接
                    if(client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        MessageBox.Show("asd");
                    }
                    return;
                }
                string msg = Encoding.Default.GetString(data, 0, len);
                AppendToMainBoardText(string.Format("客户端说:{0}", msg));
            }

        }

        private void btnclient_Click(object sender, EventArgs e)
        {
            /*FrmUser fu = new FrmUser();
            fu.Visible = true;*/
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            foreach(Socket proxSocket in clientList)
            {
                if (proxSocket.Connected)
                {
                    //1、把原始的字符串转换为字节数组
                    string msg = txtMsg.Text;
                    byte []data = Encoding.Default.GetBytes(msg);
                    //2、对原始的字节数组加上协议的头部字节
                    byte[] result = new byte[data.Length + 1];
                    result[0] = 1;
                    //3、把原始的数据放到最终的字节数组中去。
                    Buffer.BlockCopy(data, 0, result, 1, data.Length);
                    proxSocket.Send(result, 0, result.Length, SocketFlags.None);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Socket proxSocket in clientList)
            {
                if (proxSocket.Connected)
                {
                    //1、发送协议字头为2
                    proxSocket.Send(new byte[] {2}, SocketFlags.None);
                }
            }
        }
    }
}

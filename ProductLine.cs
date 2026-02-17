using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    class ProductLine
    {
        private IPAddress myIP;
        private int myPort;
        private string stationName;
        //已经连接工位的IP和Socket集合
        private List<Socket> clientList = new List<Socket>();
        //生产当前产品需要用到的工位集合
        //private List<string> stationList = new List<string>();
        //存储所有工位的通讯链接情况 key 设置的工位名，value （IP:端口号）
        private Dictionary<string, string> dicStation;              //外界传递的参数
        private Dictionary<string, string> dicAllStation;              //外界传递的参数
        //存储所有工位的生产数据以及返回数据
        private Dictionary<string, StationData> dicUsedStationData;   //外界传递的参数
        private Dictionary<string, MasterData> dicUsedMasterData;    //外界传递的参数
        private Dictionary<string, StationData> dicNUsedStationData;   //外界传递的参数
        private Dictionary<string, MasterData> dicNUsedMasterData;       //外界传递的参数

        string msg = null;
        //把一个工位的连接、接收的数据、发送的数据打包到一个结构体变量中
        StationInfo stationInfo = new StationInfo();
        Random rd = new Random();
        #region  结构体构造函数
        public struct StationInfo
        {
            public Socket socket;
            public StationData stationData;
            public MasterData masterData;
        }
        public ProductLine(IPAddress myIP,int myPort, Dictionary<string, string> dicStation,Dictionary<string, StationData> dicUsedStationData,Dictionary<string, MasterData> dicUsedMasterData)
        {
            this.myIP = myIP;
            this.myPort = myPort;
            this.dicStation = dicStation;
            this.dicUsedStationData = dicUsedStationData;
            this.dicUsedMasterData = dicUsedMasterData;
            UsedDataInitialize();
            //show12();
            startLisen();
        }
        public ProductLine(IPAddress myIP, int myPort, Dictionary<string, string> dicStation, Dictionary<string, string> dicAllStation, Dictionary<string, StationData> dicUsedStationData, Dictionary<string, MasterData> dicUsedMasterData, Dictionary<string, StationData> dicNUsedStationData, Dictionary<string, MasterData> dicNUsedMasterData)
        {
            this.myIP = myIP;
            this.myPort = myPort;
            this.dicStation = dicStation;
            this.dicAllStation = dicAllStation;
            this.dicUsedStationData = dicUsedStationData;
            this.dicUsedMasterData = dicUsedMasterData;
            this.dicNUsedStationData = dicNUsedStationData;
            this.dicNUsedMasterData = dicNUsedMasterData;
            UsedDataInitialize();
            NUsedDataInitialize();
            //show12();
            startLisen();
        }

        #endregion
        #region  数据初始化
        private void UsedDataInitialize() //初始化 dicUsedStationData 和 dicUsedStationData 的数据
        {
            string s="";
            foreach (var dic in dicStation)
            {
                MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + dic.Key + ":" + dic.Value);
            }
            List<string> tempKey = new List<string>(dicStation.Keys);
            dicUsedStationData.Clear();
            dicUsedMasterData.Clear();
            for (int i = 0; i < dicStation.Count; i++)
            {
                dicUsedStationData.Add(tempKey[i], new StationData(tempKey[i]));
                dicUsedMasterData.Add(tempKey[i], new MasterData(tempKey[i]));
                dicUsedStationData[tempKey[i]].lastStationName = s;
                dicUsedStationData[tempKey[i]].stationOrder = i + 1;
                s = tempKey[i];
                if (i == 0)
                {
                    dicUsedStationData[tempKey[i]].firstStation = true;
                }
                if (i == dicStation.Count-1)
                {
                    dicUsedStationData[tempKey[i]].endStation = true;
                }

                MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + dicUsedStationData[tempKey[i]].stationName + " "+ dicUsedStationData[tempKey[i]].stationOrder + " " + dicUsedStationData[tempKey[i]].lastStationName);
            }
        }

        private void NUsedDataInitialize()
        {
            dicNUsedStationData.Clear();
            dicNUsedMasterData.Clear();
            foreach (var ds in dicAllStation)
            {
                if (dicStation.ContainsKey(ds.Key)==false)
                {
                    if (ds.Key != "Server")
                    {
                        StationData sd = new StationData(ds.Key);
                        dicNUsedStationData.Add(ds.Key, sd);
                        dicNUsedMasterData.Add(ds.Key, new MasterData(ds.Key));
                        MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + ds.Key + ":" + ds.Value);
                    }
                }
            }
        }
        #endregion

        private void startLisen()  //侦听端口
        {
            //1、创建socket
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //2、绑定端口IP
            try
            {
                server.Bind(new IPEndPoint(myIP, myPort));
                //3、开始侦听
                server.Listen(10);
                //4、开启接收客户端的链接
                ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptSocketClient), server);
            }
            catch (Exception e)
            {
                MyTools.Show(e.ToString()+",请检查网线是否插好,然后重新登录");
                Application.Exit();
            }
        }
        /// <summary>
        /// 接收来自客户端的请求,并创建与之对应的连接。开辟新线程开始接收数据。 Thread.CurrentThread.ManagedThreadId.ToString()
        /// 并判断该请求是否是合法请求，如果不需要该工位的链接，断开该工位的链接并跳出返回
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void AcceptSocketClient(object obj) 
        {
            Socket server = obj as Socket;
            bool need = false;
            int ok = 0;
            //MyTools.ProgrameLog(MyTools.GetMethodName(1) + "/系统开始侦听下位机连接请求:" + Thread.CurrentThread.ManagedThreadId.ToString());
            while (true)
            {
                MyTools.MyWrite(MyTools.GetMethodName(1) + "/系......" + server.LocalEndPoint.ToString() );
                Socket currentSocket = server.Accept();
                IPEndPoint currentIP = (IPEndPoint)currentSocket.RemoteEndPoint;//读取当前链接的远程IP
                MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + currentIP.ToString());
                //如有已经有该工位的链接，断开该工位的链接并跳出返回,应该是不会有这种情况，同一IP同一端口的情况应该没有
                foreach (var skt in clientList)
                {
                    if (skt.RemoteEndPoint.ToString() == currentSocket.RemoteEndPoint.ToString())
                    {
                        currentSocket.Shutdown(SocketShutdown.Both);
                        currentSocket.Close();
                        MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + "该工位已存在" );
                        ok = ok+1;
                    }
                }
                //如果不需要该工位的链接，断开该工位的链接并跳出返回
                foreach (var skt in dicAllStation)
                {
                    if (currentSocket.RemoteEndPoint.ToString() == skt.Value)
                    {
                        need = true;
                        stationName = skt.Key;
                    }
                    //Console.WriteLine("++++++++" + currentSocket.RemoteEndPoint.ToString());
                }
                if (need ==false)
                {
                    currentSocket.Shutdown(SocketShutdown.Both);
                    currentSocket.Close();
                    MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + "非法请求，已经断开连接" );
                    ok=ok+1;
                }
                need = false;
                if(ok==0)
                {
                    clientList.Add(currentSocket);
                    stationInfo.socket = currentSocket;
                    if(dicStation.ContainsKey(stationName))
                    {
                        stationInfo.stationData = dicUsedStationData[stationName]; //dicUsedStationData的数据已经在初始化程序中初始化完成
                        stationInfo.masterData = dicUsedMasterData[stationName];   //dicUsedStationData的数据已经在初始化程序中初始化完成
                    }
                    else
                    {
                        stationInfo.stationData = dicNUsedStationData[stationName]; //dicNUsedStationData的数据已经在初始化程序中初始化完成
                        stationInfo.masterData = dicNUsedMasterData[stationName];   //dicNUsedStationData的数据已经在初始化程序中初始化完成
                    }
                    stationInfo.stationData.localIP = stationInfo.socket.LocalEndPoint.ToString();
                    stationInfo.stationData.remoteIP = stationInfo.socket.RemoteEndPoint.ToString();
                    stationInfo.stationData.connected = true;
                    MyTools.MyWrite(MyTools.GetMethodName(1) + "/系统侦听中......" + MyConvert.CheckNull(stationInfo.masterData.stationName));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveData), stationInfo);
                }
                else
                {
                    MyTools.MyWrite(MyTools.GetMethodName(1) + "/" + ok.ToString());
                    ok = 0;
                }
            }
        }
        private void ReceiveData(object state)
        {
            StationInfo station = (StationInfo)state;
            Socket client =station.socket;
            byte[] data = new byte[1024];
            while (true)
            {
                int len = 0;
                try
                {
                    client.ReceiveTimeout = 5000;//设定的时间内没收到数据，链接自动断开，解决客户端异常退出服务器不知道的情况。
                    len = client.Receive(data,0, data.Length, SocketFlags.None);
                    MyConvert.ConvertReceiveData(data, station.stationData);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(client.RemoteEndPoint);
                    //客户端产生异常了
                    if (stationInfo.socket == client)
                    {
                        stationInfo.stationData.localIP = null;
                        stationInfo.stationData.remoteIP = null;
                        stationInfo.stationData.connected = false;
                    }
                    clientList.Remove(client);
                    //关闭连接
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                    }
                    return;
                }
                msg = Encoding.Default.GetString(data, 0, len);
                SendData(client, station.masterData);
            }
        }
        private void SendData(object state,MasterData masterData)    //单工位发送数据
        {
            Socket proxSocket = state as Socket;
            if (proxSocket.Connected)
            {
                //1、把原始的字符串转换为字节数组
                byte[] data = MyConvert.ConvertSendData(masterData);
                //3、把原始的数据放到最终的字节数组中去。
                //Buffer.BlockCopy(data, 0, result, 1, data.Length); 批量复制数据
                proxSocket.Send(data, 0, data.Length, SocketFlags.None);
                // MessageBox.Show(msg);
             //   System.Console.Write("发送成功" + "\r\n");
            }
        }
    }
}

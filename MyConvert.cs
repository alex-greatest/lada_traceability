using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review
{
    public class MyConvert
    {
        public static void ConvertReceiveData(byte[] data, StationData stationData)
        {
            stationData.Estop = ByteToBool(data[0], 0);
            stationData.heart = ByteToBool(data[0], 1);
            stationData.alarmMaterial = ByteToBool(data[0], 2);
            stationData.alarmQuality = ByteToBool(data[0], 3);
            stationData.alarmEquipment = ByteToBool(data[0], 4);
            stationData.alarmHelp = ByteToBool(data[0], 5);
            stationData.by06 = ByteToBool(data[0], 6);
            stationData.by07 = ByteToBool(data[0], 7);
            stationData.by10 = ByteToBool(data[1], 0);
            stationData.by11 = ByteToBool(data[1], 1);
            stationData.by12 = ByteToBool(data[1], 2);
            stationData.by13 = ByteToBool(data[1], 3);
            stationData.by14 = ByteToBool(data[1], 4);
            stationData.by15 = ByteToBool(data[1], 5);
            stationData.by16 = ByteToBool(data[1], 6);
            stationData.by17 = ByteToBool(data[1], 7);
            stationData.status = BytesToInt162(data, 2);
            stationData.quality = BytesToInt162(data, 4);
            stationData.by1 = BytesToInt162(data, 6);
            stationData.by2 = BytesToInt162(data, 8);
            stationData.RFID = ByteToString(data, 10, 20);
            for (int j = 0; j < 20; j++)
            {
                stationData.result[j] = ByteToFloat(data,j*4+50);
            }
            //Console.Write(data[10].ToString());
            stationData.productCode = ByteToString(data, 130, 40);
            stationData.barcode2 = ByteToString(data, 170, 40);
            stationData.barcode3 = ByteToString(data, 210, 40);
            stationData.barcode4 = ByteToString(data, 250, 40);
            stationData.barcode5 = ByteToString(data, 290, 40);
            stationData.barcode6 = ByteToString(data, 330, 40);
            stationData.barcode7 = ByteToString(data, 370, 40);
            stationData.barcode8 = ByteToString(data, 410, 40);
        }
        public static byte[] ConvertSendData(MasterData masterData)
        {
            byte[] bdata = new byte[330];
            byte[] tdata ;
            bool[] bitArray = new bool[8];
            bitArray[7] = masterData.heart;
            bitArray[6] = masterData.run;
            bitArray[5] = masterData.enable;
            bitArray[4] = masterData.model;
            bitArray[3] = masterData.by04;
            bitArray[2] = masterData.by05;
            bitArray[1] = masterData.by06;
            bitArray[0] = masterData.by07;
            bdata[0] = MyConvert.BoolArrayToByte(bitArray);
            bitArray[0] = masterData.codeSource8;
            bitArray[1] = masterData.codeSource7;
            bitArray[2] = masterData.codeSource6;
            bitArray[3] = masterData.codeSource5;
            bitArray[4] = masterData.codeSource4;
            bitArray[5] = masterData.codeSource3;
            bitArray[6] = masterData.codeSource2;
            bitArray[7] = masterData.codeSource1;
            bdata[1] = MyConvert.BoolArrayToByte(bitArray);
            tdata = MyConvert.Int16ToBytes2(masterData.feedBack);
            bdata[2] = tdata[0];
            bdata[3] = tdata[1];
            tdata = MyConvert.Int16ToBytes2(masterData.type);
            bdata[4] = tdata[0];
            bdata[5] = tdata[1];
            tdata = MyConvert.Int16ToBytes2(masterData.leftOrRight);
            bdata[6] = tdata[0];
            bdata[7] = tdata[1];
            tdata = MyConvert.Int16ToBytes2(masterData.byI2);
            bdata[8] = tdata[0];
            bdata[9] = tdata[1];

            for (int j =0;j<8;j++)
            {
                if (j == 0)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.productCode.ToString()));
                }
                else if(j==1)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode2.ToString()));
                }
                else if (j == 2)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode3.ToString()));
                }
                else if (j == 3)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode4.ToString()));
                }
                else if (j == 4)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode5.ToString()));
                }
                else if (j == 5)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode6.ToString()));
                }
                else if (j == 6)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode7.ToString()));
                }
                else if (j == 7)
                {
                    tdata = MyConvert.StringToByte(CheckNull(masterData.barcode8.ToString()));
                }

                for (int i = 0; i < 40; i++)
                {
                    if (i < tdata.Length)
                    {
                        bdata[10 + j * 40 + i] = tdata[i];
                    }
                    else
                    {
                        bdata[10 + j * 40 + i] =0x20 ;
                    }
                }

            }
            return bdata ;
        }

        #region 将byte数值转为浮点数
        private static float ByteToFloat(byte[] inputData)/// 将4个byte类型的16数据转为浮点数
        {
            return BitConverter.ToSingle(inputData.Reverse().ToArray(), 0);   
        }
        #endregion

        #region 将byte数组中的连续4个数值转为浮点数
        private static float ByteToFloat(byte[] inputData, int offset)/// 将4个byte类型的16数据转为浮点数
        {
            byte[] bdata = new byte[4];
            bdata[0] = inputData[offset + 0];
            bdata[1] = inputData[offset + 1];
            bdata[2] = inputData[offset + 2];
            bdata[3] = inputData[offset + 3];
            return BitConverter.ToSingle(bdata.Reverse().ToArray(), 0);
        }
        #endregion

        #region 将byte数组中的某一个byte值中的某一位数据读取出来转换成布尔型数据
        private static bool ByteToBool(byte inputData,Byte bitPosition)
        {
            return (inputData & (byte)Math.Pow(2, bitPosition)) > 0 ? true : false;
        }
        #endregion

        #region 将byte数组中的连续2个数值转换为Int16类型，适用于(低位在前，高位在后)
        public static Int16 BytesToInt16(byte[] inputData, int offset)
        {
            Int16 value;
            value = (Int16)((inputData[offset] & 0xFF)
                    | ((inputData[offset + 1] & 0xFF) << 8));
            return value;
        }
        #endregion

        #region 将byte数组中的连续2个数值转换为Int16类型，适用于(高位在前，低位在后)
        public static Int16 BytesToInt162(byte[] inputData, int offset)
        {
            Int16 value;
            value = (Int16)(((inputData[offset ] & 0xFF) << 8)
                    | (inputData[offset + 1] & 0xFF));
            return value;
        }
        #endregion

        #region 将byte数组中的连续4个数值转换为Int32，也就是int类型，适用于(低位在前，高位在后)的顺序
        public static int BytesToInt(byte[] inputData, int offset)
        {
            int value;
            value = (int)((inputData[offset] & 0xFF)
                    | ((inputData[offset + 1] & 0xFF) << 8)
                    | ((inputData[offset + 2] & 0xFF) << 16)
                    | ((inputData[offset + 3] & 0xFF) << 24));
            return value;
        }
        #endregion

        #region 将byte数组中的连续4个数值转换为Int32，也就是int类型，适用于(高位在前，低位在后)的顺序
        public static int BytesToInt2(byte[] inputData, int offset)
        {
            int value;
            value = (int)(((inputData[offset] & 0xFF) << 24)
                    | ((inputData[offset + 1] & 0xFF) << 16)
                    | ((inputData[offset + 2] & 0xFF) << 8)
                    | (inputData[offset + 3] & 0xFF));
            return value;
        }
        #endregion

        #region 将byte数组转换为string
        public static string ByteToString(byte[] inputData,int offset,int offlength)
        {
            byte[] bdata = new byte[offlength];
            for(int i = 0; i < offlength; i++)
            {
                bdata[i] = inputData[offset + i];
            }
            string str = System.Text.Encoding.Default.GetString(bdata).Trim();
            return str;
        }

        #endregion

        #region 将int数值转换为占四个字节的byte数组，适用于(低位在前，高位在后)的顺序。
        public static byte[] IntToBytes(int inputData)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((inputData >> 24) & 0xFF);
            src[2] = (byte)((inputData >> 16) & 0xFF);
            src[1] = (byte)((inputData >> 8) & 0xFF);
            src[0] = (byte)(inputData & 0xFF);
            return src;
        }
        #endregion

        #region 将int数值转换为占四个字节的byte数组，适用于(高位在前，低位在后)的顺序。
        public static byte[] intToBytes2(int inputData)
        {
            byte[] src = new byte[4];
            src[0] = (byte)((inputData >> 24) & 0xFF);
            src[1] = (byte)((inputData >> 16) & 0xFF);
            src[2] = (byte)((inputData >> 8) & 0xFF);
            src[3] = (byte)(inputData & 0xFF);
            return src;
        }
        #endregion

        #region 将int16数值转换为占2个字节的byte数组，适用于(低位在前，高位在后)的顺序。
        public static byte[] Int16ToBytes(Int16 inputData)
        {
            byte[] src = new byte[2];
            src[1] = (byte)((inputData >> 8) & 0xFF);
            src[0] = (byte)(inputData & 0xFF);
            return src;
        }
        #endregion

        #region 将int16数值转换为占2个字节的byte数组，适用于(高位在前，低位在后)的顺序。
        public static byte[] Int16ToBytes2(Int16 inputData)
        {
            byte[] src = new byte[2];
            src[0] = (byte)((inputData >> 8) & 0xFF);
            src[1] = (byte)(inputData & 0xFF);
            return src;
        }
        #endregion



        #region string类型转成byte[]：
        public static byte[] StringToByte(string inputData)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(inputData);
            return byteArray;
        }

        #endregion

        #region bool数组类型转成byte
        public static byte BoolArrayToByte(bool[] input)
        {
            byte ret = new byte();
            for (int i = 0; i < input.Length; i += 8)
            {
                int value = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (input[i + j])
                    {
                        value += 1 << (7 - j);
                    }
                }
                ret= (byte)value;
            }
            return ret;
        }

        #endregion

        #region 检测输入的字符串是否为NUll
        public static string CheckNull(string inputData)
        {
            if (inputData == null)
            {
                return "";
            }
            else
            {
                return inputData.Trim();
            }
        }
        #endregion
        #region

        #endregion

        #region

        #endregion


        #region

        #endregion
    }

}

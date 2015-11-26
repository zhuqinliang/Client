using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace e7
{
    public class DeivceE7Impl
    {
        /// <summary>
        /// 当前设备
        /// </summary>
        public Int32 iDevice { get; set; }
        private const Int32 sectorNumber = 6;

        public DeivceE7Impl()
        {

        }
        private String encode(String value)
        {
            char[] arrs = value.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = arrs.Length - 1; i >= 0; i--)
            {
                int temp = Convert.ToInt32((arrs[i]).ToString());
                sb.Append((9 - temp).ToString());
            }
            return sb.ToString();
        }

        public string StrToHex(string mStr) //返回处理后的十六进制字符串
        {
            return BitConverter.ToString(
            ASCIIEncoding.Default.GetBytes(mStr)).Replace("-", " ");
        } /* StrToHex */
        public string HexToStr(string mHex) // 返回十六进制代表的字符串
        {
            mHex = mHex.Replace(" ", "");
            if (mHex.Length <= 0) return "";
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
                if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return ASCIIEncoding.Default.GetString(vBytes);
        }


        public int DisplayLcd(string displayStr) 
        {
            E7Lib.fw_lcd_dispclear(iDevice);
            int result = E7Lib.fw_lcd_dispstr(iDevice, displayStr);
            return result;
        }

        public  string openDevice()
        {

            try
            {
                Int32 tmphdev = 0;
                Int32[] devs = new Int32[100];
                int i = 0;
                do
                {
                    tmphdev = E7Lib.fw_init(100, 0);
                    if (tmphdev > 0)
                    {
                        devs[i] = tmphdev;
                        
                        
                    }
                    else
                    {

                        //TODO:写入程序通知，让用户插入设备
                        //等待设备 1*100秒
                        Thread.Sleep(100);
                       
                    }
                    i++;

                }
                while (i < 25 && tmphdev <0);
                
                iDevice = devs[i-1];
                //打开LCD
                setLCDBright(true);
                //配置卡类型
                int state = E7Lib.fw_config_card(iDevice, 0x41);
                int stateAuth = E7Lib.fw_authentication(iDevice, 0, sectorNumber);
                //E7Lib.fw_beep(iDevice, 10);
                return iDevice.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
           
          
        }

        public string openDeviceAsync()
        {
            throw new NotImplementedException();
        }
        public string closeDevice()
        {
            if (iDevice<=0)
            {
                return "无当前设备";
            }
            try
            {
                //关闭LCD
                setLCDBright(false);
                //清空显示文本
                E7Lib.fw_lcd_dispclear(iDevice);
                //关闭设备
                E7Lib.fw_exit(iDevice);
                
                return "OK";
            } catch (Exception ex)
            {
                return ex.Message;
            }
           
        }

        public string beep(uint sec)
        {
            if (iDevice <= 0)
            {
                return "请先打开设备!";
            }
            int ibstate=E7Lib.fw_beep(iDevice, sec);
            if (0 == ibstate)
            {
                return "调用蜂鸣器成功!";
            }
            else
            {
                return "调用蜂鸣器失败，code："+ ibstate;
            }

        }

        public void initLCDShow()
        {
            E7Lib.fw_lcd_dispclear(iDevice);
            showString2LCD("天天100");
        }
        public void clearLCDShow()
        {
            E7Lib.fw_lcd_dispclear(iDevice);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="switchFlag">true:打开，false:关闭</param>
        public void setLCDBright(bool switchFlag)
        {
            if (iDevice < 0)
            {
                return;
            }
            //点亮或熄灭的标志，15-点亮，0-熄灭
            if (switchFlag)
            {
                E7Lib.fw_lcd_setbright(iDevice, 15);

            }
            else
            {
                E7Lib.fw_lcd_setbright(iDevice, 0);
            }

        }
        public void showString2LCD(string strLCD)
        {
            strLCD.PadLeft(36, ' ');//空格补齐，不然会有乱码
            E7Lib.fw_lcd_dispclear(iDevice);
            E7Lib.fw_lcd_dispstr(iDevice, strLCD);
        }
        public void showStringLCD2Lines(string strLCDLine1, string strLCDLine2)
        {


            E7Lib.fw_lcd_dispclear(iDevice);

            if (iDevice <=0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder(strLCDLine1);
            while (sb.Length < 18)
            {
                sb.Append(" ");
            }
            sb.Append(strLCDLine2);
            while (sb.Length < 36)
            {
                sb.Append(" ");
            }
           
            E7Lib.fw_lcd_dispstr(iDevice, sb.ToString());

            //E7Lib.fw_lcd_dispstr_ex(iDevice, strLCDLine1.PadRight(36,' '), 1, 0, strLCDLine1.Length, 1);
            //E7Lib.fw_lcd_dispstr_ex(iDevice, strLCDLine2.PadRight(36,' '), 2, 0, strLCDLine2.Length, 1);
        }
        /// <summary>
        /// 返回卡号
        /// </summary>
        /// <returns></returns>
        public string findCard()
        {
            uint _snr = 0;
            int iFindCardResult = E7Lib.fw_card(iDevice, 1, ref _snr);
            if (0 == iFindCardResult)
            {
                return _snr.ToString();
            }
            else
            {
                return "";
            }
        }
        public string readCard()
        {

            StringBuilder str = new StringBuilder();
            int au = E7Lib.fw_authentication(iDevice, 0, sectorNumber);
            int result = E7Lib.fw_read_hex(iDevice, sectorNumber * 4,
                   str);
            if (str.Length > 32)
            {
                str = str.Remove(32, 1);
            }

            if (0 == result)
            {
                 
                return HexToStr(str.ToString());
            }
            else
            {
                return "";
            }
        }
        public bool writeCard(string strWrite)
        {
            //byte[] strByte = System.Text.Encoding.Default.GetBytes(strWrite);
            ////如果有中文，这里需要改
            if (strWrite.Length < 16)
            {

                strWrite = strWrite.PadRight(16, ' ');
            }

            string strHexwrite = StrToHex(strWrite).Replace(" ","");
            int result = E7Lib.fw_write_hex(iDevice, sectorNumber * 4,
                   strHexwrite);
            if (0 == result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string readCardID()
        {
            uint icard = 0;
            int result = E7Lib.fw_card(iDevice, 1,
                   ref icard);
            Byte[] pass = new Byte[6] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            int iLoad = E7Lib.fw_load_key(iDevice, 0, sectorNumber, ref pass[0]);
            int ipass = E7Lib.fw_authentication(iDevice,0, sectorNumber);
            if (0 == result)
            {
                return icard.ToString();
            }
            else
            {
                return "";
            }
        }
        public void initPassState(byte sec = 30)
        {
            if (iDevice<=0)
            {
                return;
            }
            try
            {
                E7Lib.fw_PassIn(iDevice, sec);

            }
            catch
            {
              
            }
           
        }
        public string readKeyInPass()
        {
            if (iDevice <=0)
            {
                return "";
            }
          
            int state = -1;
            StringBuilder temp = new StringBuilder();
            initPassState(120);
            do
            {
                //state:0 完成输入
                //161：输入密码长度过长，最长密码为25个数字
                //162：，取消密码输入
                //163：未处于密码输入状态
                //164：，用户输入密码还未完成
                //165：，用户密码输入操作超时
                Byte[] tempPass =new Byte[6];//6位密码
                
                Byte lenpass = 0;
                state = E7Lib.fw_PassGet(iDevice, ref lenpass, ref tempPass[0]);
                int statePassKey = E7Lib.fw_CheckKeyValue(iDevice, ref lenpass, ref tempPass[0]);
                //cancelPassState();
                if (lenpass > 6)
                {
                    //等待用户按确认
                    continue;
                }
                if (statePassKey == 0&& state!=0&& lenpass>0&& lenpass!= temp.Length)
                {
#if DEBUG
                    temp.Append(((char)tempPass[lenpass - 1]).ToString());
#else
                      temp.Append("*");
#endif


                    showStringLCD2Lines("请输入密码！", GetDisplayPsw(temp.ToString()));
                }
                if (state == 0 || state == 162)
                {
                    //输入完成或者取消，跳出循环
                    break;
                }
            } while (state!= 161&&
            state != 162 &&
            state != 165 &&
            state != 0 );
            if (0== state)
            {
                showStringLCD2Lines("输入完成!", "");
                return temp.ToString();
            }
            return "";
           
        }

        public void cancelPassState()
        {
            if (iDevice < 0)
            {
                return; 
            }
            E7Lib.fw_PassCancel(iDevice);


        }
        public bool validatePass(string cardPass)
        {
            Byte[] keya = new Byte[6] { 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe };
            string stNewPassHex = StrToHex(cardPass).Replace(" ", "");
            Byte[] pass = passToHex(cardPass);
            int iLoad = E7Lib.fw_load_key(iDevice, 0, sectorNumber, ref keya[0]);
            int iResult = E7Lib.fw_authentication(iDevice, 0, sectorNumber);


            if (0 == iResult)
            {
                return true;
            }
            
            return false;
        }
        public void changePass(string strNewPass)
        {
            Byte[] keya = new Byte[6] { 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe };
            Byte[] ctrlbyte = new Byte[4] { 0xff, 0x7, 0x80, 0x69 };
            Byte[] keyb = new Byte[6] { 0xfe, 0xfe, 0xfe, 0xfe, 0xfe, 0xfe };
            Byte[] bNewPassHex = passToHex(strNewPass);

           
            int sauth = E7Lib.fw_authentication(iDevice, 0, sectorNumber);
            //fw_change3
            int state = E7Lib.fw_changeb3(iDevice,sectorNumber, ref keya[0], ref ctrlbyte[0], 0, ref keyb[0]);
        }

        private  Byte[] passToHex(string strPass)
        {
            Byte[] bNewPassHex = new Byte[6] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

             

            

            for (int i = 0; i <= strPass.Length - 1; i++)
            {
                string pTemp = strPass.Substring(i, 1);
                string pTempHex = StrToHex(pTemp);
                bNewPassHex[i] = (byte)Convert.ToInt32(pTempHex);
            }

            return bNewPassHex;

        }

        private string GetDisplayPsw(string p_InputPSW)
        {
            string temp = string.Empty;
            for (int i = 0; i < p_InputPSW.Length; i++)
            {
                temp += "*";
            }
            return temp;
        }
    }
}

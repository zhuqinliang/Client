using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace e7
{
    public class E7Device
    {
        DeivceE7Impl E7 = new DeivceE7Impl();

        public bool OpenDevice()
        {
            bool result = false;
            try
            {
                string id = E7.openDevice();
                if (id != "0")
                {
                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }

        public void CloseDevice()
        {
            try
            {
                E7.closeDevice();
            }
            catch
            {
            }
        }

        public bool FindCard()
        {
            bool result = false;
            try
            {
                string cardnum = E7.readCardID();
                if (!string.IsNullOrEmpty(cardnum))
                {
                    result = true;
                }
            }
            catch
            {

            }

            return result;
        }

        /// <summary>
        /// 读取逻辑卡号,空表示还未开卡
        /// </summary>
        /// <returns></returns>
        public string ReadLogicCardId()
        {
            string cardid = string.Empty;

            try
            {
                cardid = E7.readCard();
                cardid = Encode(cardid);
            }
            catch
            {

            }
            return cardid;
        }

        public bool WriteLogicCardId(string p_CardId)
        {
            bool result = false;

            try
            {
                p_CardId=Encode(p_CardId);
                result = E7.writeCard(p_CardId);
            }
            catch
            {

            }

            return result;
        }

        public void Beep()
        {
            try
            {
                E7.beep(6);
            }
            catch
            {

            }
        }

        public string ReadUserPSW()
        {
           return E7.readKeyInPass();
        }

        public string Encode(string p_CardId)
        {
            char[] charArray = p_CardId.ToCharArray();
            StringBuilder sb = new StringBuilder(charArray.Length);
            foreach (char str in charArray)
            {
                sb.Append(9 - int.Parse(str.ToString()));
            }
            charArray = sb.ToString().ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public void DisplayLcd(string p_Text)
        {
            E7.DisplayLcd(p_Text);
        }

        public void DisplayLcd(string p_Line1Text, string p_Line2Text)
        {
            E7.showStringLCD2Lines(p_Line1Text,p_Line2Text);
        }

        CardIdCreater m_CardIdCreater = new CardIdCreater();
        public string CreateNewCardId()
        {
           return m_CardIdCreater.CreateCardId(DateTime.Now);
        }
    }
}

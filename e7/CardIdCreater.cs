using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace e7
{
    public class CardIdCreater
    {
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }  

        public string CreateCardId(DateTime p_DateTime)
        {
            long time = ConvertDateTimeToInt(p_DateTime);
            Random random = new Random(int.Parse(time.ToString().Substring(5, 8)));

            int randomnumber = GetRandomNumber(m_RandomNumbers, 0, 1, 1000, random);

            string cardid = time.ToString() + randomnumber.ToString("000");

            return cardid;
        }


        static List<int> m_RandomNumbers = new List<int>();
        private int GetRandomNumber(List<int> p_RandomNumbers, int p_RandomNumber, int p_MinValue, int p_MaxValue, Random p_Random)
        {
            int tempnumber =p_Random.Next(p_MinValue, p_MaxValue);

            if (m_RandomNumbers.Contains(tempnumber))
            {
                tempnumber = GetRandomNumber(p_RandomNumbers, tempnumber, p_MinValue, p_MaxValue, p_Random);
            }

            p_RandomNumbers.Add(tempnumber);

            return tempnumber;
        }
    }
}

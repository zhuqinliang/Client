
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace e7
{
    public class DeviceAsyncHandler
    {
        DeivceE7Impl E7 = new DeivceE7Impl();
        private delegate string openDviceDelegate();
        private string initDevice()
        {
            try
            {
                //Thread.Sleep(10000);
                string id = E7.openDevice();
                E7.beep(10);
                E7.initLCDShow();
                return id;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
           

          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Idl">显示的主窗体，用于回调函数显示值</param>
        /// <returns></returns>
        
        public string openDevice(Control Idl)
        {
            try
            {
                openDviceDelegate openDvice = initDevice;
                IAsyncResult result = openDvice.BeginInvoke(openDeviceCallback, Idl);
            }
            catch
            {

            }
           
            //string idDevice = openDvice.EndInvoke(result);
            //return idDevice;
            return "";
        }

        private void openDeviceCallback(IAsyncResult r)
        {
            AsyncResult aResult = (AsyncResult)r;
            openDviceDelegate d = (openDviceDelegate)aResult.AsyncDelegate;
            string id = d.EndInvoke(r);
            //openDviceDelegate d = ((openDviceDelegate)r.).EndInvoke(r);
            E7.iDevice = Convert.ToInt32(id);
            ((Control)r.AsyncState).Invoke(new Action(()=>
            {
                MessageBox.Show(id);
            }));
           
            Console.WriteLine("操作完成!"+r.AsyncState);


        }
      
    }
}

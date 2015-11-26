using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 留作以后多设备接口
/// </summary>
namespace e7
{
    public interface IDeviceTool
    {
        string openDevice();
        /// <summary>
        /// 异步打开设备
        /// </summary>
        /// <returns></returns>
        string openDeviceAsync();
        
    }
}

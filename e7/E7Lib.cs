using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace e7
{
    public class E7Lib
    {
        [DllImport("E7umf.dll", EntryPoint = "fw_config_card")]
        public static extern Int32 fw_config_card(Int32 icdev, Byte cardtype);

        [DllImport("E7umf.DLL", EntryPoint = "fw_init")]

        public static extern Int32 fw_init(Int16 port, Int32 baud);
        [DllImport("E7umf.DLL", EntryPoint = "fw_exit")]
        public static extern Int32 fw_exit(Int32 icdev);
        [DllImport("E7umf.Dll", EntryPoint = "fw_request")]
        public static extern Int32 fw_request(Int32 icdev, Byte _Mode, ref UInt32 TagType);
        [DllImport("E7umf.DLL", EntryPoint = "fw_anticoll")]
        public static extern Int32 fw_anticoll(Int32 icdev, Byte _Bcnt, ref UInt32 _Snr);
        [DllImport("E7umf.DLL", EntryPoint = "fw_select")]
        public static extern Int32 fw_select(Int32 icdev, UInt32 _Snr, ref Byte _Size);
        [DllImport("E7umf.DLL", EntryPoint = "fw_card")]
        public static extern Int32 fw_card(Int32 icdev, Byte _Mode, ref UInt32 _Snr);
        [DllImport("E7umf.DLL", EntryPoint = "fw_load_key")]
        public static extern Int32 fw_load_key(Int32 icdev, Byte _Mode, Byte _SecNr, ref Byte _NKey);
        [DllImport("E7umf.DLL", EntryPoint = "fw_authentication")]
        public static extern Int32 fw_authentication(Int32 icdev, Byte _Mode, Byte _SecNr);

        [DllImport("E7umf.DLL", EntryPoint = "fw_authentication_pass")]
        public static extern Int32 fw_authentication_pass(Int32 icdev, Byte _Mode, Byte Addr, ref Byte _passbuff);

        [DllImport("E7umf.DLL", EntryPoint = "fw_read")]
        public static extern Int32 fw_read(Int32 icdev, Byte _Adr, ref Byte _Data);
        [DllImport("E7umf.dll", EntryPoint = "fw_read_hex")]
        public static extern Int16 fw_read_hex(Int32 icdev, Byte _Adr,  StringBuilder _Data);

        [DllImport("E7umf.DLL", EntryPoint = "fw_write")]
        public static extern Int32 fw_write(Int32 icdev, Byte _Adr, ref Byte _Data);
        [DllImport("E7umf.dll", EntryPoint = "fw_write_hex")]
        public static extern Int16 fw_write_hex(Int32 icdev, Byte _Adr, string _Data);

        [DllImport("E7umf.DLL", EntryPoint = "fw_halt")]
        public static extern Int32 fw_halt(Int32 icdev);
        [DllImport("E7umf.DLL", EntryPoint = "fw_changeb3")]
        public static extern Int32 fw_changeb3(Int32 icdev, Byte _SecNr, ref Byte _KeyA, ref Byte _CtrlW, Byte _Bk,
                ref Byte _KeyB);
        [DllImport("E7umf.DLL", EntryPoint = "fw_initval")]
        public static extern Int32 fw_initval(Int32 icdev, Byte _Adr, UInt32 _Value);
        [DllImport("E7umf.DLL", EntryPoint = "fw_increment")]
        public static extern Int32 fw_increment(Int32 icdev, Byte _Adr, UInt32 _Value);
        [DllImport("E7umf.DLL", EntryPoint = "fw_readval")]
        public static extern Int32 fw_readval(Int32 icdev, Byte _Adr, ref UInt32 _Value);
        [DllImport("E7umf.DLL", EntryPoint = "fw_decrement")]
        public static extern Int32 fw_decrement(Int32 icdev, Byte _Adr, UInt32 _Value);
        [DllImport("E7umf.DLL", EntryPoint = "fw_restore")]
        public static extern Int32 fw_restore(Int32 icdev, Byte _Adr);
        [DllImport("E7umf.DLL", EntryPoint = "fw_transfer")]
        public static extern Int32 fw_transfer(Int32 icdev, Byte _Adr);
        [DllImport("E7umf.DLL", EntryPoint = "fw_beep")]
        public static extern Int32 fw_beep(Int32 icdev, UInt32 _Msec);
        [DllImport("E7umf.DLL", EntryPoint = "fw_getver")]
        public static extern Int32 fw_getver(Int32 icdev, ref Byte buff);
        [DllImport("E7umf.DLL", EntryPoint = "fw_reset")]
        public static extern Int32 fw_reset(IntPtr icdev, UInt16 _Msec);
        [DllImport("E7umf.dll", EntryPoint = "fw_cpureset")]
        public static extern Int16 fw_cpureset(Int32 icdev, ref Byte rlen, ref Byte rdata);
        [DllImport("E7umf.dll", EntryPoint = "fw_pro_reset")]
        public static extern Int16 fw_pro_reset(Int32 icdev, ref Byte rlen, ref Byte rdata);
        [DllImport("E7umf.dll", EntryPoint = "fw_pro_commandlink")]
        public static extern Int16 fw_pro_commandlink(Int32 icdev, Byte slen, ref Byte sdata,
            ref Byte rlen, ref Byte rdata, Byte ftt, Byte fFG);
        [DllImport("E7umf.dll", EntryPoint = "fw_cpuapdu")]
        public static extern Int16 fw_cpuapdu(IntPtr icdev, Byte slen, ref Byte sdata, ref Byte rlen,
            ref Byte rdata);

        //lcd
        [DllImport("E7umf.dll", EntryPoint = "fw_lcd_dispclear")]
        public static extern Int32 fw_lcd_dispclear(Int32 icdev);


        [DllImport("E7umf.dll", EntryPoint = "fw_lcd_dispstr")]
        public static extern Int32 fw_lcd_dispstr(Int32 icdev, String pText);


        [DllImport("E7umf.dll", EntryPoint = "fw_lcd_dispstr_ex")]
        public static extern Int32 fw_lcd_dispstr_ex(Int32 icdev, String pTex,Int32 line,Int32 offset ,
            Int32 len,Int32 flag);

        [DllImport("E7umf.dll", EntryPoint = "fw_lcd_setbright")]
        public static extern Int32 fw_lcd_setbright(Int32 icdev, Byte bright);

        //key board
        [DllImport("E7umf.dll", EntryPoint = "fw_PassIn")]
        public static extern Int32 fw_PassIn(Int32 icdev, Byte ctime);
        [DllImport("E7umf.dll", EntryPoint = "fw_PassGet")]
        public static extern Int32 fw_PassGet(Int32 icdev, ref Byte rlen, ref Byte cpass);
        [DllImport("E7umf.dll", EntryPoint = "fw_PassCancel")]
        public static extern Int32 fw_PassCancel(Int32 icdev);
        [DllImport("E7umf.dll", EntryPoint = "fw_CheckKeyValue")]
        public static extern Int32 fw_CheckKeyValue(Int32 icdev, ref Byte rlen, ref Byte rData);


        [DllImport("E7umf.DLL", EntryPoint = "hex_a")]
        public static extern void hex_a(ref Byte hex, ref Byte a, Int32 len);
        [DllImport("E7umf.dll", EntryPoint = "a_hex")]
        public static extern void a_hex(ref Byte a, ref Byte hex, Int32 alen);

        public const Byte VK_F1 = 0x70;
        public const Byte VK_F2 = 0x71;
        public const Byte VK_F3 = 0x72;
    }
}

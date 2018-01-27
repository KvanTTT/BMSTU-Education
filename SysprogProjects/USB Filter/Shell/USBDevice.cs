using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Shell
{
    internal sealed class Native
    {
        // from winuser.h
        internal const int WM_DEVICECHANGE = 0x0219;

        // from winbase.h
        internal const int INVALID_HANDLE_VALUE = -1;
        internal const int GENERIC_READ = unchecked((int)0x80000000);
        internal const int FILE_SHARE_READ = 0x00000001;
        internal const int FILE_SHARE_WRITE = 0x00000002;
        internal const int OPEN_EXISTING = 3;

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool GetVolumeNameForVolumeMountPoint(
            string volumeName,
            StringBuilder uniqueVolumeName,
            int uniqueNameBufferCapacity);

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern bool DeviceIoControl(IntPtr hDevice, int dwIoControlCode, IntPtr lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr hObject);

        // from winerror.h
        internal const int ERROR_NO_MORE_ITEMS = 259;
        internal const int ERROR_INSUFFICIENT_BUFFER = 122;
        internal const int ERROR_INVALID_DATA = 13;

        // from winioctl.h
        internal const string GUID_DEVINTERFACE_VOLUME = "53f5630d-b6bf-11d0-94f2-00a0c91efb8b";
        internal const string GUID_DEVINTERFACE_DISK = "53f56307-b6bf-11d0-94f2-00a0c91efb8b";
        internal const int IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS = 0x00560000;

        [StructLayout(LayoutKind.Sequential)]
        internal struct DISK_EXTENT
        {
            internal int DiskNumber;
            internal long StartingOffset;
            internal long ExtentLength;
        }

        // from cfg.h
        internal enum PNP_VETO_TYPE
        {
            Ok,

            TypeUnknown,
            LegacyDevice,
            PendingClose,
            WindowsApp,
            WindowsService,
            OutstandingOpen,
            Device,
            Driver,
            IllegalDeviceRequest,
            InsufficientPower,
            NonDisableable,
            LegacyDriver,
        }

        // from cfgmgr32.h
        [DllImport("setupapi.dll")]
        internal static extern int CM_Get_Parent(
            ref int pdnDevInst,
            int dnDevInst,
            int ulFlags);

        [DllImport("setupapi.dll")]
        internal static extern int CM_Get_Device_ID(
            int dnDevInst,
            StringBuilder buffer,
            int bufferLen,
            int ulFlags);

        [DllImport("setupapi.dll")]
        internal static extern int CM_Request_Device_Eject(
            int dnDevInst,
            out PNP_VETO_TYPE pVetoType,
            StringBuilder pszVetoName,
            int ulNameLength,
            int ulFlags
            );

        [DllImport("setupapi.dll", EntryPoint = "CM_Request_Device_Eject")]
        internal static extern int CM_Request_Device_Eject_NoUi(
            int dnDevInst,
            IntPtr pVetoType,
            StringBuilder pszVetoName,
            int ulNameLength,
            int ulFlags
            );

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        internal static extern Boolean SetupDiCallClassInstaller(
                                                  UInt32 InstallFunction,
                                                  IntPtr DeviceInfoSet,
                                                  ref SP_DEVINFO_DATA DeviceInfoData
                                              );

        [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr MemSet(IntPtr dest, int c, IntPtr count);

        // from setupapi.h
        internal const int DIGCF_PRESENT = (0x00000002);
        internal const uint DIGCF_ALLCLASSES = (0x00000004);
        internal const int DIGCF_DEVICEINTERFACE = (0x00000010);

        internal const int SPDRP_DEVICEDESC = 0x00000000;
        internal const int SPDRP_CAPABILITIES = 0x0000000F;
        internal const int SPDRP_CLASS = 0x00000007;
        internal const int SPDRP_CLASSGUID = 0x00000008;
        internal const int SPDRP_FRIENDLYNAME = 0x0000000C;
        internal const int SPDRP_LOWERFILTERS = 0x00000012;

        internal static Guid GUID_CLASS_USB = new Guid("36FC9E60-C465-11CF-8056-444553540000");

        internal const string REGSTR_KEY_USB = "USB";

        internal const uint DICS_FLAG_GLOBAL = 0x00000001;
        internal const uint DIREG_DEV = 0x00000001;

        public const int DIF_PROPERTYCHANGE = 18;
        public const int DICS_PROPCHANGE = 3;
        public const int DICS_FLAG_CONFIGSPECIFIC = 2;

        internal const int ERROR_SUCCESS = 0;
        /*[StructLayout(LayoutKind.Sequential)]
        internal class SP_DEVINFO_DATA
        {
            internal int cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
            internal Guid classGuid = Guid.Empty; // temp
            internal int devInst = 0; // dumy
            internal int reserved = 0;
        }*/

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            internal int cbSize;
            internal short devicePath;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class SP_DEVICE_INTERFACE_DATA
        {
            internal int cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA));
            internal Guid interfaceClassGuid = Guid.Empty; // temp
            internal int flags = 0;
            internal int reserved = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_CLASSINSTALL_HEADER
        {
            internal uint cbSize;
            internal uint InstallFunction;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_PROPCHANGE_PARAMS
        {
            internal SP_CLASSINSTALL_HEADER ClassInstallHeader;
            internal uint StateChange;
            internal uint Scope;
            internal uint HwProfile;

        };


        [DllImport("setupapi.dll")]
        internal static extern IntPtr SetupDiGetClassDevs(
            ref Guid classGuid,
            int enumerator,
            IntPtr hwndParent,
            int flags);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool SetupDiEnumDeviceInterfaces(
            IntPtr deviceInfoSet,
            SP_DEVINFO_DATA deviceInfoData,
            ref Guid interfaceClassGuid,
            int memberIndex,
            SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll")]
        internal static extern bool SetupDiOpenDeviceInfo(
            IntPtr deviceInfoSet,
            string deviceInstanceId,
            IntPtr hwndParent,
            int openFlags,
            SP_DEVINFO_DATA deviceInfoData
            );

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr deviceInfoSet,
            SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
            IntPtr deviceInterfaceDetailData,
            int deviceInterfaceDetailDataSize,
            ref int requiredSize,
            SP_DEVINFO_DATA deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr deviceInfoSet,
            SP_DEVINFO_DATA deviceInfoData,
            int property,
            out int propertyRegDataType,
            IntPtr propertyBuffer,
            int propertyBufferSize,
            out int requiredSize
            );

        [DllImport("setupapi.dll")]
        internal static extern uint SetupDiDestroyDeviceInfoList(
            IntPtr deviceInfoSet);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData,
            uint Property, out UInt32 PropertyRegDataType, byte[] PropertyBuffer, uint PropertyBufferSize,
            out UInt32 RequiredSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiSetDeviceRegistryProperty(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData,
            uint Property, byte[] PropertyBuffer, uint PropertyBufferSize);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern UIntPtr SetupDiCreateDevRegKey(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, uint Scope, uint HwProfile,
            uint KeyType, IntPtr InfHandle, IntPtr InfSectionName);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);


        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, [MarshalAs(UnmanagedType.LPTStr)] string Enumerator,
            IntPtr hwndParent, UInt32 Flags);


        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetupDiSetClassInstallParams(IntPtr DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, IntPtr ClassInstallParams, int ClassInstallParamsSize);

        [Flags]
        public enum RegOption
        {
            NonVolatile = 0x0,
            Volatile = 0x1,
            CreateLink = 0x2,
            BackupRestore = 0x4,
            OpenLink = 0x8
        }

        [Flags]
        public enum RegSAM
        {
            QueryValue = 0x0001,
            SetValue = 0x0002,
            CreateSubKey = 0x0004,
            EnumerateSubKeys = 0x0008,
            Notify = 0x0010,
            CreateLink = 0x0020,
            WOW64_32Key = 0x0200,
            WOW64_64Key = 0x0100,
            WOW64_Res = 0x0300,
            Read = 0x00020019,
            Write = 0x00020006,
            Execute = 0x00020019,
            AllAccess = 0x000f003f
        }

        public enum RegResult
        {
            CreatedNewKey = 0x00000001,
            OpenedExistingKey = 0x00000002
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCreateKeyEx(
                    UIntPtr hKey,
                    string lpSubKey,
                    int Reserved,
                    string lpClass,
                    RegOption dwOptions,
                    RegSAM samDesired,
                    IntPtr lpSecurityAttributes,
                    out UIntPtr phkResult,
                    out RegResult lpdwDisposition);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int RegOpenKeyEx(
          UIntPtr hKey,
          string subKey,
          RegOption ulOptions,
          RegSAM samDesired,
          out UIntPtr hkResult);

        [DllImport("advapi32.dll", EntryPoint = "RegDeleteKey", SetLastError = true)]
        public static extern int RegDeleteKey(
            UIntPtr hKey, // Handle to an open registry key
            string lpSubKey); // The name of the key to be deleted.

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCloseKey(UIntPtr hKey);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegSetValueEx(
            IntPtr hKey,
            [MarshalAs(UnmanagedType.LPStr)] string lpValueName,
            int Reserved,
            Microsoft.Win32.RegistryValueKind dwType,
            [MarshalAs(UnmanagedType.LPStr)] string lpData,
            int cbData);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegSetValueEx(
            IntPtr hKey,
            [MarshalAs(UnmanagedType.LPStr)] string lpValueName,
            int Reserved,
            Microsoft.Win32.RegistryValueKind dwType,
            byte[] lpData,
            int cbData);


        private Native()
        {
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SP_DEVINFO_DATA
    {
        public UInt32 cbSize;
        public Guid ClassGuid;
        public UInt32 DevInst;
        public IntPtr Reserved;
    }

    public struct USBDevice
    {
        public IntPtr DevInfoSet;
        public SP_DEVINFO_DATA DevInfoData;
        public string Caption;

        public USBDevice(IntPtr DevInfoSet, SP_DEVINFO_DATA DevInfoData, string Caption)
        {
            this.DevInfoSet = DevInfoSet;
            this.DevInfoData = DevInfoData;
            this.Caption = Caption;
        }
    };



    public static class USBUtils
    {
        public static byte[] GetDeviceRegistryProperty(IntPtr DevInfo, SP_DEVINFO_DATA DevInfoData, uint Property, ref uint BufferSize)
        {
            UInt32 PropertyRegDataType;
            Native.SetupDiGetDeviceRegistryProperty(DevInfo, ref DevInfoData, Native.SPDRP_DEVICEDESC, out PropertyRegDataType, null, 0, out BufferSize);

            byte[] PropertyBuffer = new byte[BufferSize];
            bool success = Native.SetupDiGetDeviceRegistryProperty(DevInfo,
                                                            ref DevInfoData,
                                                            Property,
                                                            out PropertyRegDataType,
                                                            PropertyBuffer,
                                                            BufferSize,
                                                            out BufferSize);

            if (success) return PropertyBuffer;
            else return null;
        }

        public static List<USBDevice> GetUSBDevices()
        {
            List<USBDevice> USBDevices= new List<USBDevice>();

            IntPtr DevInfo;
            SP_DEVINFO_DATA DevInfoData;

            DevInfo = Native.SetupDiGetClassDevs(ref Native.GUID_CLASS_USB, Native.REGSTR_KEY_USB, IntPtr.Zero, Native.DIGCF_PRESENT);

            uint BUFFER_SIZE = 1024;
            byte[] PropertyBuffer = new byte[BUFFER_SIZE];
            uint RequiredSize;
            uint RegType;
            uint i = 0;
            DevInfoData = new SP_DEVINFO_DATA();
            DevInfoData.cbSize = (uint)Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
            while (Native.SetupDiEnumDeviceInfo(DevInfo, i, ref DevInfoData) == true)
            {
                Native.SetupDiGetDeviceRegistryProperty(DevInfo, ref DevInfoData,
                    Native.SPDRP_DEVICEDESC, out RegType, PropertyBuffer, BUFFER_SIZE, out RequiredSize);

                string Str;
                unsafe
                {
                    sbyte* Array = stackalloc sbyte[(int)RequiredSize-1];
                    for (int j = 0; j < RequiredSize-1; j++)
                        Array[j] = (sbyte)PropertyBuffer[j];
                    Str = new string(Array, 0, (int)RequiredSize-2, Encoding.Unicode);
                }
                if (Str != "USB Root Hub")
                {
                    USBDevices.Add(new USBDevice(DevInfo, DevInfoData, Str));
                }

                i++;
            }
            return USBDevices;
        }


        public static bool Restart(USBDevice Device)
        {
            bool result;
            Native.SP_PROPCHANGE_PARAMS prms = new Native.SP_PROPCHANGE_PARAMS();
            unsafe
            {                
                int size = Marshal.SizeOf(typeof(Native.SP_PROPCHANGE_PARAMS));

                prms.ClassInstallHeader.cbSize = (uint)Marshal.SizeOf(typeof(Native.SP_CLASSINSTALL_HEADER));
                prms.ClassInstallHeader.InstallFunction = Native.DIF_PROPERTYCHANGE;
                prms.StateChange = Native.DICS_PROPCHANGE;
                prms.Scope = Native.DICS_FLAG_CONFIGSPECIFIC;
                prms.HwProfile = 0;

                result = Native.SetupDiSetClassInstallParams(Device.DevInfoSet, ref Device.DevInfoData,
                     (IntPtr)(&prms), Marshal.SizeOf(typeof(Native.SP_PROPCHANGE_PARAMS)));
                if (!result) return false;

                return Native.SetupDiCallClassInstaller(Native.DIF_PROPERTYCHANGE, Device.DevInfoSet, ref Device.DevInfoData);
            }
        }

        public static bool Eject(USBDevice Device)
        {
            return Native.CM_Request_Device_Eject_NoUi((int)Device.DevInfoData.DevInst, IntPtr.Zero, null, 0, 0) == 0 ? true : false;
        }
    }
}

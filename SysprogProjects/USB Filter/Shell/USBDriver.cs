using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace Shell
{
    static class USBDriver
    {
      /*  public void FindInDir(DirectoryInfo dir, string pattern, bool recursive, ref List<string> FileNames)
        {
            foreach (FileInfo file in dir.GetFiles(pattern))
            {
                FileNames.Add(file.FullName);
            }
            if (recursive)
            {
                foreach (DirectoryInfo subdir in dir.GetDirectories())
                {
                    this.FindInDir(subdir, pattern, recursive, ref FileNames);
                }
            }
        }
        public List<string> FindFiles(string dir, string pattern)
        {
            List<string> FileNames = new List<string>();
            this.FindInDir(new DirectoryInfo(dir), pattern, true, ref FileNames);
            return FileNames;
        }*/
        //static bool IsDriverInstalled(

        static bool StrCmp(byte[] Bytes, uint ByteSize, string Str)
        {
            int i;
            for (i = 0; i < Str.Length; i++)
            {
                if (2*i >= ByteSize)
                    return false;
                if (Bytes[2*i] != (byte)Str[i])
                    return false;
            }
            return true;
        }

        static byte[] BytesFromString(string Str)
        {
            byte[] Result = new byte[Str.Length*2];
            for (int i = 0; i < Str.Length; i++)
            {
                Result[2*i] = (byte)Str[i];
                Result[2*i+1] = (byte)0;
            }
            return Result;
        }

        static bool CreateFilterParamsKey(USBDevice Device, out UIntPtr hKey)
        {
            UIntPtr PrevKey = Native.SetupDiCreateDevRegKey(Device.DevInfoSet, ref Device.DevInfoData,
                Native.DICS_FLAG_GLOBAL, 0, Native.DIREG_DEV, IntPtr.Zero, IntPtr.Zero);

            hKey = UIntPtr.Zero;
            if (PrevKey == null)
                return false;

            Native.RegResult lpdwDisposition;
            if (Native.RegCreateKeyEx(PrevKey, "KvntUsbDrvrParams", 0, null, Native.RegOption.NonVolatile, Native.RegSAM.AllAccess, IntPtr.Zero,
                out hKey, out lpdwDisposition) != Native.ERROR_SUCCESS)
            {
                Native.RegCloseKey(PrevKey);
                return false;
            }
            Native.RegCloseKey(PrevKey);
            return true;
        }

        static bool OpenFilterParamsKey(USBDevice Device, out UIntPtr hKey)
        {
            UIntPtr PrevKey = Native.SetupDiCreateDevRegKey(Device.DevInfoSet, ref Device.DevInfoData,
            Native.DICS_FLAG_GLOBAL, 0, Native.DIREG_DEV, IntPtr.Zero, IntPtr.Zero);

            hKey = UIntPtr.Zero;
            if (PrevKey == null)
                return false;

            if (Native.RegOpenKeyEx(PrevKey, "KvntUsbDrvrParams", Native.RegOption.NonVolatile, Native.RegSAM.AllAccess, out hKey) != Native.ERROR_SUCCESS)
            {
                Native.RegCloseKey(PrevKey);
                return false;
            }
            Native.RegCloseKey(PrevKey);
            return true;
        }

        static bool RegDeleteFilterParams(UIntPtr hKey)
        {
            if (Native.RegDeleteKey(hKey, "") != Native.ERROR_SUCCESS)
            {
                Native.RegCloseKey(hKey);
                return false;
            }
            Native.RegCloseKey(hKey);
            return true;
        }

        static bool RegWriteFilterParams(UIntPtr hKey, uint MaxLogSize, string LogFileName)
        {
            byte[] Bytes = new byte[]{ (byte)((MaxLogSize >> 24) & 0xFF), (byte)((MaxLogSize >> 16) & 0xFF), 
                                       (byte)((MaxLogSize >> 8) & 0xFF), (byte)((MaxLogSize) & 0xFF) };
           // byte[] Bytes = new byte[] { (byte)200, (byte)0, (byte)00, (byte)0 };
            Array.Reverse(Bytes);
            //string Str = new string(Bytes);
            if (Native.RegSetValueEx((IntPtr)(int)hKey, "MaxLogSize", 0, Microsoft.Win32.RegistryValueKind.DWord, Bytes, sizeof(uint)) != 0)
                return false;

            LogFileName = "\\??\\" + LogFileName;

            //Bytes = BytesFromString(LogFileName);
            if (Native.RegSetValueEx((IntPtr)(int)hKey, "LogFileName", 0, Microsoft.Win32.RegistryValueKind.String, LogFileName, LogFileName.Length) != 0)
                return false;

            return true;
        }

        public static bool AnyFilterInstalled(List<USBDevice> Devices, string DriverName)
        {
            byte[] Property = null;
            bool FilterExists = false;
            uint PropertySize = 0;

            for (int i = 0; i < Devices.Count; i++)
            {
                Property = USBUtils.GetDeviceRegistryProperty(Devices[i].DevInfoSet,
                                                     Devices[i].DevInfoData,
                                                     Native.SPDRP_LOWERFILTERS,
                                                     ref PropertySize);

                if (Property != null && StrCmp(Property, PropertySize, DriverName))
                {
                    FilterExists = true;
                    break;
                }
            }

            return FilterExists;
        }


        public static bool UninstallFilter(USBDevice Device, string DriverName, uint FilterType)       
        {
            byte[] Property = null;
            uint PropertySize = 0;
            bool success;

            Property = USBUtils.GetDeviceRegistryProperty(Device.DevInfoSet, Device.DevInfoData, FilterType, ref PropertySize);

            if (Property == null || (Property[0] == 0))
            {               
                return false;
            }
            else
            {
                if (!StrCmp(Property, PropertySize, DriverName))
                {
                    return false;
                }
                else
                {
                    /*Property = BytesFromString(DriverName);
                    PropertySize = (uint)Property.Length;*/
                    for (int i = 0; i < Property.Length; i++)
                        Property[i] = 0;

                    success = Native.SetupDiSetDeviceRegistryProperty(Device.DevInfoSet, ref Device.DevInfoData,
                                                               FilterType,
                                                               Property,
                                                               PropertySize + 1);
                    if (!success)
                    {
                        return false;
                    }
                }
            }

            UIntPtr hKey;
            OpenFilterParamsKey(Device, out hKey);

            if (!RegDeleteFilterParams(hKey))
            {
                return false;
            }

            return true;
        }

        public static bool InstallFilter(USBDevice Device, string DriverName, uint FilterType, uint Flags, string LogPath) 
        {
                byte[] Property = null;
                uint PropertySize = 0;
                bool success;
  
                Property = USBUtils.GetDeviceRegistryProperty(Device.DevInfoSet, Device.DevInfoData, FilterType, ref PropertySize);

                if (Property == null || (Property[0] == 0))
                {
                    Property = BytesFromString(DriverName);
                    PropertySize = (uint)Property.Length;

                    int counter = 0;
                    success = false;

                    do
                    {
                        success = Native.SetupDiSetDeviceRegistryProperty(Device.DevInfoSet, ref Device.DevInfoData,
                                                                       FilterType, Property, PropertySize);
                        counter++;
                    }
                    while ((!success) && (counter < 100));


                    if (!success)
                    {
                        return false;
                    }
                }
                else
                {
                    if (StrCmp(Property, PropertySize, DriverName))
                    {
                        //return false; // драйвер установлен
                    }

                    Property = BytesFromString(DriverName);
                    PropertySize = (uint)Property.Length;

                    success = Native.SetupDiSetDeviceRegistryProperty(Device.DevInfoSet, ref Device.DevInfoData,
                                                               FilterType,
                                                               Property,
                                                               PropertySize);
                    if (!success)
                    {
                        return false;
                    }
                }


                UIntPtr hKey;
                CreateFilterParamsKey(Device, out hKey);

                if (!RegWriteFilterParams(hKey, Flags, LogPath))
                {                    
                    Native.RegCloseKey(hKey);
                    return false;
                }

                Native.RegCloseKey(hKey);

                return true;
        }

        public static bool IsFilterInstalled(USBDevice Device, string DriverName)
        {
            byte[] Property = null;
            uint PropertySize = 0;

            Property = USBUtils.GetDeviceRegistryProperty(Device.DevInfoSet,
                                                 Device.DevInfoData,
                                                 Native.SPDRP_LOWERFILTERS,
                                                 ref PropertySize);

            if (Property != null && StrCmp(Property, PropertySize, DriverName))
                return true;
            else
                return false;
        }


        public static void InstallKey(string DriverFileName)
        {
            string ShortFileName = DriverFileName.Substring(DriverFileName.LastIndexOf(@"\") + 1);
            string DriverName = Path.GetFileNameWithoutExtension(ShortFileName);

            File.Copy(DriverFileName, Environment.SystemDirectory + @"\drivers\" + ShortFileName, true);

            RegistryKey Key = Registry.LocalMachine.CreateSubKey(@"System\CurrentControlSet\Services\" + DriverName);
            Key.SetValue("Type", 1);
            Key.SetValue("ErrorControl", 1);
            Key.SetValue("Start", 3);
            Key.SetValue("ImagePath", @"System32\drivers\" + ShortFileName);
            Key.SetValue("DisplayName", DriverName);
            Key.Close();
        }        

        public static void UnistallKey(string DriverName)
        {
            File.Delete(Environment.SystemDirectory + @"\" + DriverName + ".sys");
            Registry.LocalMachine.DeleteSubKeyTree(@"System\CurrentControlSet\Services\" + DriverName);
        }

        public static bool IsKeyInstalled(string DriverName)
        {
            if (!File.Exists(Environment.SystemDirectory + @"\Drivers\" + DriverName + ".sys")) return false;
            if (Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\" +
                DriverName) == null) return false;
            return true;
        }



        public static bool IsFilterKeyInstalled(USBDevice Device, string DriverName)
        {
            return IsKeyInstalled(DriverName) && IsFilterInstalled(Device, DriverName);
        }
    }
}

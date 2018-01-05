using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Text.RegularExpressions;
using System.Net;
using System.Security.Cryptography;

namespace ComputerStat
{
    public class Hardware
    {
        public StringBuilder InformString
        {
            get;
            private set;
        }

        public string HashCode
        {
            get;
            private set;
        }

        public Hardware()
        {
            CollectInfo();
            CalcHash();
        }

        public bool HardSnap
        {
            private set;
            get;
        }

        public bool SoftSnap
        {
            private set;
            get;
        }


        void AddHardDiskInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InformString.Append(queryObj["Model"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    //InformString.Append(queryObj["SerialNumber"]);
                    //InformString.Append((char)13);
                    //InformString.Append((char)10);
                    InformString.Append(queryObj["Signature"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["Size"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    break;
                }
            }
            catch
            {
                //InformString.Append(Dns.GetHostName());
            }
        }

        void AddProcessorInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InformString.Append(queryObj["Caption"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["Manufacturer"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["Name"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["ProcessorId"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["Revision"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                }
            }
            catch
            {
                
            }
        }

        void AddMotherboardInfo()
        {
            try
            {
                ObjectQuery MotherboardQuery = new System.Management.ObjectQuery("select Product, Caption, Description, SerialNumber, Manufacturer, Version from Win32_BaseBoard");
                ManagementObjectSearcher MotherboardSearcher = new ManagementObjectSearcher(MotherboardQuery);
                ManagementObjectCollection MotherboardCollection = MotherboardSearcher.Get();
                foreach (ManagementObject MotherboardInfo in MotherboardCollection)
                {
                    InformString.Append(MotherboardInfo["Manufacturer"].ToString());
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(MotherboardInfo["Product"].ToString());
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(MotherboardInfo["SerialNumber"].ToString());
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(MotherboardInfo["Version"].ToString());
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                }
            }
            catch
            {
            }
        }

        void AddVideocardInfo()
        {
            try
            {
                ObjectQuery VideocardQuery = new System.Management.ObjectQuery("select Caption, Description, DeviceID from Win32_VideoController");
                ManagementObjectSearcher VideocardSearcher = new ManagementObjectSearcher(VideocardQuery);
                ManagementObjectCollection VideocardCollection = VideocardSearcher.Get();
                foreach (ManagementObject VideocardInfo in VideocardCollection)
                    InformString.Append(VideocardInfo["Caption"].ToString());
                InformString.Append((char)13);
                InformString.Append((char)10);
            }
            catch
            {
            }
        }

        void AddNetworkAdapterInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkAdapter");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InformString.Append(queryObj["AdapterType"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["Description"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["DeviceID"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                    InformString.Append(queryObj["MACAddress"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                }
            }
            catch
            {
                
            }
        }

        void AddPhysicalMediaInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PhysicalMedia");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InformString.Append(queryObj["SerialNumber"]);
                    InformString.Append(queryObj["Tag"]);
                    InformString.Append((char)13);
                    InformString.Append((char)10);
                }
            }
            catch
            {
                
            }
        }

        void AddSoundcardInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_SoundDevice");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InformString.Append(queryObj["DeviceID"]);
                    InformString.Append(queryObj["Manufacturer"]);
                    InformString.Append(queryObj["ProductName"]);
                }
            }
            catch
            {
                
            }
        }

        void AddUSBInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_USBController");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InformString.Append(queryObj["Caption"]);
                    InformString.Append(queryObj["DeviceID"]);

                }
            }
            catch
            {
                
            }
        }
   
        void AddLogicDiskInfo()
        {
            try
            {
                ObjectQuery DiskQuery = new System.Management.ObjectQuery("select FreeSpace, FileSystem, Size, Name, VolumeName, VolumeSerialNumber from Win32_LogicalDisk where DriveType = 3");
                ManagementObjectSearcher DiskSearcher = new ManagementObjectSearcher(DiskQuery);
                ManagementObjectCollection DiskCollection = DiskSearcher.Get();
                foreach (ManagementObject DiskInfo in DiskCollection)
                {
                    InformString.Append(DiskInfo["Name"].ToString());
                    InformString.Append(DiskInfo["Size"].ToString());
                    InformString.Append(DiskInfo["VolumeName"].ToString());
                    InformString.Append(DiskInfo["FileSystem"].ToString());
                    InformString.Append(DiskInfo["FreeSpace"].ToString());
                    InformString.Append(DiskInfo["VolumeSerialNumber"].ToString());
                }
            }
            catch
            {

            }
        }

        void AddBIOSInfo()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_BIOS");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["BIOSVersion"] == null)
                        InformString.Append(queryObj["BIOSVersion"]);
                    else
                    {
                        String[] arrBIOSVersion = (String[])(queryObj["BIOSVersion"]);
                        foreach (String arrValue in arrBIOSVersion)
                        {
                            InformString.Append(arrValue);
                        }
                    }
                    InformString.Append(queryObj["BuildNumber"]);
                    InformString.Append(queryObj["Caption"]);
                    InformString.Append(queryObj["CodeSet"]);
                    InformString.Append(queryObj["CurrentLanguage"]);
                    InformString.Append(queryObj["Description"]);
                    InformString.Append(queryObj["IdentificationCode"]);
                    InformString.Append(queryObj["InstallableLanguages"]);
                    InformString.Append(queryObj["InstallDate"]);
                    InformString.Append(queryObj["LanguageEdition"]);

                    if (queryObj["ListOfLanguages"] == null)
                        InformString.Append(queryObj["ListOfLanguages"]);
                    else
                    {
                        String[] arrListOfLanguages = (String[])(queryObj["ListOfLanguages"]);
                        foreach (String arrValue in arrListOfLanguages)
                        {
                            InformString.Append(arrValue);
                        }
                    }
                    InformString.Append(queryObj["Manufacturer"]);
                    InformString.Append(queryObj["Name"]);
                    InformString.Append(queryObj["OtherTargetOS"]);
                    InformString.Append(queryObj["PrimaryBIOS"]);
                    InformString.Append(queryObj["ReleaseDate"]);
                    InformString.Append(queryObj["SerialNumber"]);
                    InformString.Append(queryObj["SMBIOSBIOSVersion"]);
                    InformString.Append(queryObj["SMBIOSMajorVersion"]);
                    InformString.Append(queryObj["SMBIOSMinorVersion"]);
                    InformString.Append(queryObj["SMBIOSPresent"]);
                    InformString.Append(queryObj["SoftwareElementID"]);
                    InformString.Append(queryObj["SoftwareElementState"]);
                    InformString.Append(queryObj["Status"]);
                    InformString.Append(queryObj["TargetOperatingSystem"]);
                    InformString.Append(queryObj["Version"]);
                }
            }
            catch
            {
               
            }
        }


        public void CollectInfo()
        {
            InformString = new StringBuilder(1024);
            AddHardDiskInfo();
            AddProcessorInfo();
            AddMotherboardInfo();
            AddVideocardInfo();
            //AddPhysicalMediaInfo();
            //AddNetworkAdapterInfo();            
            // AddSoundcardInfo();
            // AddUSBInfo();
            // AddLogicDiskInfo();
        }

        public void CalcHash()
        {
            StringBuilder s = new StringBuilder();
            foreach (byte b in SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(InformString.ToString())))
                s.Append(b.ToString("x2"));

            HashCode = s.ToString();
        }
    }
}

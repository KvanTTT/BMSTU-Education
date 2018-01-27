using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Shell
{
    public class Volume
    {
        public readonly ulong BlockSize;
        public readonly ulong Capacity;
        public readonly string DeviceID;
        public readonly char DriveLetter;
        public readonly uint DriveType;
        public readonly string FileSystem;
        public readonly ulong FreeSpace;
        public readonly string Label;
        public readonly uint SerialNumber;

        public Volume(ulong BlockSize,
                      ulong Capacity,
                      string DeviceID,
                      char DriveLetter,
                      uint DriveType,
                      string FileSystem,
                      ulong FreeSpace,
                      string Label,
                      uint SerialNumber)
        {
            this.BlockSize     = BlockSize;
            this.Capacity     = Capacity;
            this.DeviceID      = DeviceID;
            this.DriveLetter   = DriveLetter;
            this.DriveType     = DriveType;
            this.FileSystem    = FileSystem;
            this.FreeSpace     = FreeSpace;
            this.Label = Label;
            this.SerialNumber  = SerialNumber;
        }

        public uint Format(string FileSystem, uint ClusterSize, string Label, bool Quick)
        {
            ManagementObject classInstance =
                   new ManagementObject("root\\CIMV2",
                   "Win32_Volume.DeviceID='" + DeviceID + "'",
                   null);

            ManagementBaseObject inParams =
                classInstance.GetMethodParameters("Format");

            inParams["ClusterSize"] = ClusterSize;
            inParams["FileSystem"] = FileSystem;
            inParams["Label"] = Label;
            inParams["QuickFormat"] = Quick;

            ManagementBaseObject outParams =
                classInstance.InvokeMethod("Format", inParams, null);

            return (uint)outParams["ReturnValue"];
        }
    }
}
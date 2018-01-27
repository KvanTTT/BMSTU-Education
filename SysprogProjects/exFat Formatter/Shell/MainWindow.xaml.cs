using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Management;
using System.Windows.Interop;

namespace Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Volume> LogicalVolumes;
        bool Flag;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        const int WM_DEVICECHANGE = 0x0219;
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DEVICECHANGE)
                UpdateVolumesList();
            return IntPtr.Zero;
        }

        public MainWindow()
        {
            string[] Params = Environment.GetCommandLineArgs();
            string LogStr = "";
            if (IsDriverInstall("ext2fs"))
            {                
                if (Params.Contains("/uninstall"))
                {
                    UninstallFileAndKey("ext2fs");
                    UninstallAutostart("ext2fs");
                    DateTime Now = DateTime.Now;
                    LogStr = "[" + Now.Hour + '.' + Now.Minute + '.' + Now.Second + "] Driver ext2fs has been removed" + Environment.NewLine;
                }
                else
                    if (Params.Contains("/start"))
                    {                        
                        DateTime Now = DateTime.Now;
                        LogStr += "[" + Now.Hour + '.' + Now.Minute + '.' + Now.Second + "] Service ext2fs starting..." + Environment.NewLine;
                        LogStr += ExecuteCommandSync("net start ext2fs") + Environment.NewLine;
                        this.WindowState = WindowState.Minimized;
                    }
            }

            InitializeComponent();

            if (Params.Contains("/start"))
                this.WindowState = WindowState.Minimized;

            textBox1.Text += LogStr;
            if (IsDriverInstall("ext2fs"))
                button1.Content = "Uninstall Driver";
            else
            {
                button1.Content = "Install Driver";
                btnFormat.IsEnabled = false;
            }

            UpdateVolumesList();            
        }

        public string ExecuteCommandSync(string command)
        {
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.RedirectStandardInput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            procStartInfo.Verb = "runas";
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();

            DateTime Now = DateTime.Now;
            return "[" + Now.Hour + '.' + Now.Minute + '.' + Now.Second + "] " + result + Environment.NewLine;
        }

        //Ext2Fs
        private void InstallFileAndKey(string DriverFileName)
        {
            string ShortFileName = DriverFileName.Substring(DriverFileName.LastIndexOf(@"\") + 1);
            string DriverName = System.IO.Path.GetFileNameWithoutExtension(ShortFileName);

            File.Copy(DriverFileName, Environment.SystemDirectory + @"\drivers\" + ShortFileName, true);
            DateTime Now = DateTime.Now;
            textBox1.Text += "[" + Now.Hour + '.' + Now.Minute + '.' + Now.Second + "] " + ShortFileName + @" has been succsessfully moved to " + Environment.SystemDirectory + 
                @"\drivers\" + ShortFileName + Environment.NewLine;

            RegistryKey Key = Registry.LocalMachine.CreateSubKey(@"System\CurrentControlSet\Services\" + DriverName);
            Key.SetValue("Type", 2);
            Key.SetValue("ErrorControl", 1);
            Key.SetValue("Start", 3);
            Key.SetValue("Group", "File System");
            Key.Close();
            textBox1.Text += "[" + Now.Hour + '.' + Now.Minute + '.' + Now.Second + "] Registry key has been created" + Environment.NewLine;
        }

        private void UninstallFileAndKey(string DriverName)
        {                     
            File.Delete(Environment.SystemDirectory + @"\Drivers\" + DriverName + ".sys");
            Registry.LocalMachine.DeleteSubKeyTree(@"System\CurrentControlSet\Services\" + DriverName);           
        }

        private bool IsDriverInstall(string DriverName)
        {
            if (!File.Exists(Environment.SystemDirectory + @"\Drivers\" + DriverName + ".sys")) return false;
            if (Registry.LocalMachine.OpenSubKey(@"System\CurrentControlSet\Services\" +
                DriverName) == null) return false;
            return true;
        }

        private void AutostartInstall()
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            Key.SetValue("ext2fs manager", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("vshost.", "") + " /start");
            Key.Close();
        }

        private void AutostartUninstall()
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            Key.SetValue("ext2fs manager", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("vshost.", "") + " /uninstall");
            Key.Close();
        }

        private void UninstallAutostart(string DriverName)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            Key.DeleteValue("ext2fs manager");
            Key.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDriverInstall("ext2fs"))
            {
                InstallFileAndKey(@"..\..\..\Driver\objchk_wxp_x86\i386\ext2fs.sys");
                AutostartInstall();
                if (MessageBox.Show("Restarting required. Maybe now?", "ext2fs install", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    Reboot.halt(true, true);
            }
            else
            {
                AutostartUninstall();
                if (MessageBox.Show("Restarting required. Maybe now?", "ext2fs Uninstall", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    Reboot.halt(true, true);
            }
        }

        List<Volume> GetLogicalVolumes()
        {
            
            ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher("root\\CIMV2", 
                    "SELECT * FROM Win32_Volume");
            searcher.Options.Rewindable = false;
            searcher.Options.ReturnImmediately = false;
            searcher.Options.EnumerateDeep = false;

            List<Volume> Volumes = new List<Volume>(2);
            try
            {
                foreach (ManagementObject queryObj in searcher.Get())
                    if ((uint)queryObj["DriveType"] == 3 || (uint)queryObj["DriveType"] == 2)
                        Volumes.Add(new Volume((ulong)queryObj["BlockSize"], (ulong)queryObj["Capacity"], (string)queryObj["DeviceID"],
                            (char)((string)queryObj["DriveLetter"])[0], (uint)queryObj["DriveType"], (string)queryObj["FileSystem"],
                            (ulong)queryObj["FreeSpace"], (string)queryObj["Label"], (uint)queryObj["SerialNumber"]));
            }
            catch (Exception Ex)
            {
            }
            
            return Volumes;
        }        

        public void UpdateVolumesList()
        {
            // вот этот Flag - танцы с бубном. Но без этого глючит
            if (Flag == false)
            {
                Flag = true;
                LogicalVolumes = GetLogicalVolumes();
                lbxDrives.Items.Clear();
                int i = 0;
                foreach (Volume LogicalVolume in LogicalVolumes)
                {
                    lbxDrives.Items.Add(LogicalVolume.DriveLetter + ": " + LogicalVolume.Label);
                    i++;
                }
                Flag = false;
            }
        }

        private const int KB = 1024;
        private const int MB = KB * 1000;
        private const int GB = MB * 1000;
        private string FormatByteCount(ulong bytes)
        {
            string format = null;

            if (bytes < KB)
            {
                format = String.Format("{0} Bytes", bytes);
            }
            else if (bytes < MB)
            {
                bytes = bytes / KB;
                format = String.Format("{0} KB", bytes.ToString("N"));
            }
            else if (bytes < GB)
            {
                double dree = bytes / MB;
                format = String.Format("{0} MB", dree.ToString("N1"));
            }
            else
            {
                double gree = bytes / GB;
                format = String.Format("{0} GB", gree.ToString("N1"));
            }

            return format;
        }

        private void lbxDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxDrives.SelectedIndex >= 0 && lbxDrives.SelectedIndex < lbxDrives.Items.Count)
            {
                lblCapacity.Content = FormatByteCount(LogicalVolumes[lbxDrives.SelectedIndex].Capacity);
                lblFileSystem.Content = LogicalVolumes[lbxDrives.SelectedIndex].FileSystem.ToString();
                lblClusterSize.Content = LogicalVolumes[lbxDrives.SelectedIndex].BlockSize.ToString();
                lblFreeSpace.Content = FormatByteCount(LogicalVolumes[lbxDrives.SelectedIndex].FreeSpace);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (lbxDrives.SelectedIndex >= 0 && lbxDrives.SelectedIndex < lbxDrives.Items.Count)
            {
                if (MessageBox.Show("Are you really want to format drive " + LogicalVolumes[lbxDrives.SelectedIndex].DriveLetter + ": " + 
                    LogicalVolumes[lbxDrives.SelectedIndex].Label + " to exFAT?", 
                    "exFAT formatter", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    LogicalVolumes[lbxDrives.SelectedIndex].Format("exFAT", Convert.ToUInt32(cmbClusterSize.SelectionBoxItem),
                        LogicalVolumes[lbxDrives.SelectedIndex].Label, (bool)ckbQuick.IsChecked);
                    DateTime Now = DateTime.Now;
                    textBox1.Text += "[" + Now.Hour + '.' + Now.Minute + '.' + Now.Second + "] Disk " +
                        LogicalVolumes[lbxDrives.SelectedIndex].DriveLetter + ": has been formatted" + Environment.NewLine;
                }
            }
        }
    }
}

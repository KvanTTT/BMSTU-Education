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
using System.Management;
using System.Windows.Interop;
using System.Threading;
using System.Diagnostics;

namespace Shell
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
		private static readonly string CR = Environment.NewLine;
        string DriverName = "KvntUsbDrvr";

        List<USBDevice> USBDevices;


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == Native.WM_DEVICECHANGE)
                UpdateDevicesList();
            return IntPtr.Zero;
        }

        public void UpdateDevicesList()
        {
            lbUSBDevices.Items.Clear();

            USBDevices = USBUtils.GetUSBDevices();
            int i = 0;
            foreach (USBDevice Device in USBDevices)
            {
                lbUSBDevices.Items.Add(Device.Caption + " " + i.ToString());
                i++;
            }
        }

		public Window1 ()
		{
			InitializeComponent();

            UpdateDevicesList();
		}


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (lbUSBDevices.SelectedIndex >= 0 && lbUSBDevices.SelectedIndex < USBDevices.Count)
            {
                if (USBDriver.AnyFilterInstalled(USBDevices, DriverName))
                {
                    USBDriver.UninstallFilter(USBDevices[lbUSBDevices.SelectedIndex], DriverName, Native.SPDRP_LOWERFILTERS);
                    USBDriver.UnistallKey(DriverName);
                    USBUtils.Restart(USBDevices[lbUSBDevices.SelectedIndex]);
                    button2.Content = "Install";
                }
                else
                {
                    if (System.IO.File.Exists(tbLogDir.Text + lbUSBDevices.Items[lbUSBDevices.SelectedIndex].ToString() + @".log"))
                        System.IO.File.Delete(tbLogDir.Text + lbUSBDevices.Items[lbUSBDevices.SelectedIndex].ToString() + @".log");
                    USBDriver.InstallKey(@"..\..\..\Driver\objchk_wxp_x86\i386\" + DriverName + ".sys");
                    USBDriver.InstallFilter(USBDevices[lbUSBDevices.SelectedIndex], DriverName, Native.SPDRP_LOWERFILTERS,
                        10000000, tbLogDir.Text + lbUSBDevices.Items[lbUSBDevices.SelectedIndex].ToString() + @".log");
                    USBUtils.Restart(USBDevices[lbUSBDevices.SelectedIndex]);
                    button2.Content = "Remove";
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (lbUSBDevices.SelectedIndex >= 0 && lbUSBDevices.SelectedIndex < USBDevices.Count)
            {
                USBUtils.Restart(USBDevices[lbUSBDevices.SelectedIndex]);
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            
        }

        private void lbUSBDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbUSBDevices.SelectedIndex >= 0 && lbUSBDevices.SelectedIndex < USBDevices.Count)
            {
                if (USBDriver.IsFilterKeyInstalled(USBDevices[lbUSBDevices.SelectedIndex], DriverName))
                    button2.Content = "Remove";
                else
                    button2.Content = "Install";
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (lbUSBDevices.SelectedIndex >= 0 && lbUSBDevices.SelectedIndex < USBDevices.Count)
            {
                USBUtils.Eject(USBDevices[lbUSBDevices.SelectedIndex]);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (lbUSBDevices.SelectedIndex >= 0 && lbUSBDevices.SelectedIndex < USBDevices.Count)
                System.Diagnostics.Process.Start(Environment.SystemDirectory + @"\Notepad.exe",
                    tbLogDir.Text + lbUSBDevices.Items[lbUSBDevices.SelectedIndex].ToString() + @".log");
        }
    }
}

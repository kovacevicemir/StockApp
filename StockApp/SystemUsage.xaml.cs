using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for SystemUsage.xaml
    /// </summary>
    public partial class SystemUsage : Window
    {
        PerformanceCounter myAppCPU = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName, true);
        PerformanceCounter mem = new PerformanceCounter("Memory", "% Committed Bytes In Use");


        public SystemUsage()
        {
            InitializeComponent();
            //Initialize a timer
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            //Check the CPU every 3 seconds
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            //Start the Timer
            dispatcherTimer.Start();

            this.Title = "App Usage";
            Uri iconUri = new Uri("pack://application:,,,/dollar.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                cboDrive.Items.Add(di.Name);
                cboDrive.SelectedIndex = 0;
            }

            cboDrive_SelectedIndexChanged();

        }

        //Every 3 seconds the timer ticks
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Write the result to the content of a label (CPULabel)
            CpuBox.Text = $" % {myAppCPU.NextValue()}";
            float value = mem.NextValue();
            RamBox.Text = value.ToString();

        }

        // Display information about the selected drive.
        private void cboDrive_SelectedIndexChanged()
        {
            string drive_letter = cboDrive.Text.Substring(0, 1);
            DriveInfo di = new DriveInfo(drive_letter);
            float aspace = di.AvailableFreeSpace / (1024 * 1024 * 1024);
            HddBox.Text = aspace.ToString() + " GB";
                
        }


    }
}

using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Win32;
using System.Globalization;
using MyCartographyObj;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for AboutBox.xaml
    /// </summary>
    public partial class AboutBox : Window
    {

        public AboutBox()
        {
            InitializeComponent();
            GetInformations();
        }
        public AboutBox(MyPersonalMapData personneConnectee)
        {
            InitializeComponent();
            GetInformations();
            TextBlockInfoPersonneConnectee.Text = personneConnectee.ToString();          
        }
        private void MainHeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetInformations()
        {
            SystemInfoWindowOperatingSystem.Content = GetOs();
            SystemInfoWindowNetFrameworkVersion.Content = GetNetFramworkVersion();
            SystemInfoWindowWindowsUserName.Content = Environment.UserName;
            SystemInfoWindowDomainName.Content = Environment.UserDomainName;            
            SystemInfoWindowProcessor.Content = GetProcessor();
            SystemInfoWindowLanIp.Content = GetLanIpAddress();            
            SystemInfoWindowRubyVersion.Content = GetDate();
        }

        public static string GetOs()
        {
            var searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (var os in searcher.Get())
            {
                return os["Caption"].ToString();
            }
            return string.Empty;
        }

        private static string GetNetFramworkVersion()
        {
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                var releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    return CheckFor45DotVersion(releaseKey);
                }
            }
        }       
        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393273)
            {
                return "4.6 RC or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            return "Please install .Net Framework Version 4.5 or later!";
        }       
        private static string GetProcessor()
        {
            var query = "SELECT Name FROM Win32_Processor";
            var searcher = new ManagementObjectSearcher(query);
            foreach (var wniPart in searcher.Get())
            {
                return wniPart.Properties["Name"].Value.ToString();
            }

            return string.Empty;
        }
        private static string GetLanIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }        

        private static string GetDate()
        {
            DateTime date = DateTime.Now;
            CultureInfo dateFormatFr = new CultureInfo("fr-FR");
            return date.ToString(dateFormatFr);
        }
    }
}


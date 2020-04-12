using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.UserControls
{
    /// <summary>
    /// Interaction logic for ConnectButton.xaml
    /// </summary>
    public partial class ConnectButton : UserControl
    {
        public ConnectButton()
        {
            InitializeComponent();
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ConnectionViewModel)this.DataContext;
            //If user chose not to use default settings, a dialog will open for entering port and IP.
            if (checkbox.IsChecked == false)
            {
                if ((string)Connect.Content == "Connect")
                {
                    Settings dialog = new Settings();
                    dialog.ShowDialog();
                    if (dialog.IP == null || dialog.Port == null) //? if exited from dialog ?
                        return;
                    vm.VM_Connect(dialog.IP, Int32.Parse(dialog.Port));
                }
                else
                    vm.VM_Disconnect();
            }
            //Using default settings.
            else if ((string)Connect.Content == "Connect")
            {
                int defaultPort = Int32.Parse(ConfigurationManager.AppSettings["port"].ToString());
                string defaultIP = ConfigurationManager.AppSettings["ip"].ToString();
                vm.VM_Connect(defaultIP, defaultPort);
            }
            else
                vm.VM_Disconnect();
        }
    }
}

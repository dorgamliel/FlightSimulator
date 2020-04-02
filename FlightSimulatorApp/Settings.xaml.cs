using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        string ip;
        string port;
        public Settings()
        {
            InitializeComponent();
        }
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            //Check all illegal inputs and prints a message accordingly.
            if (String.IsNullOrEmpty(b_ip.Text) || String.IsNullOrEmpty(b_port.Text))
            {
                blankArea.Text = "Please fill both settings.";
            } else if (Regex.Matches(b_port.Text, @"[^0-9]").Count > 0 || Regex.Matches(b_ip.Text, @"[^0-9.]").Count > 0 ||
                Regex.Matches(b_ip.Text, @"[.]").Count != 3)
            {
                blankArea.Text = "Illegal input.";
            } else if (Int64.Parse(b_port.Text) < 0 || Int64.Parse(b_port.Text) > 65535) {
                blankArea.Text = "Port number should be 0-65535.";
            } else
            {
                IP = b_ip.Text;
                Port = b_port.Text;
                this.Close();
            }
        }
    }
}

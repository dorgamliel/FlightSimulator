using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class Message
    {
        private bool status = false;
        public bool isEnabled
        {
            get { return status; }
            set { status = value; }
        }
        public string Content
        {
            get { return "test message dddddddddddddddddd"; }
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            isEnabled = false;
            Console.WriteLine(isEnabled);
        }
       
    }
}

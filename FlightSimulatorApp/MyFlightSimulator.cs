using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class MyFlightSimulator : IFlightSimulator
    {
        private ITelnetClient client;
        public event PropertyChangedEventHandler propertyChanged;
        public double heading
        {
            get;
            set;
        }
        public double vericalSpeed
        {
            get;
            set;
        }
        public double groundSpeed
        {
            get;
            set;
        }
        public double airSpeed
        {
            get;
            set;
        }
        public double GPSAlt
        {
            get;
            set;
        }
        public double roll
        {
            get;
            set;
        }
        public double pitch
        {
            get;
            set;
        }
        public double AltimeterAlt
        {
            get;
            set;
        }
        public double latitude
        {
            get;
            set;
        }
        public double longitude
        {
            get;
            set;
        }

        public void connect(string ip, int port)
        {
            client = new MyTelnetClient();
            client.connect(ip, port);
        }

        public void disconnect()
        {
            client.disconnect();
        }

        public void start()
        {
            new Thread(delegate()
            {
                while (true)
                {
                    client.write("get /orientation/heading-deg");
                    heading = Double.Parse(client.read());
                    client.write("get /velocities/vertical-speed-fps");
                    vericalSpeed = Double.Parse(client.read());
                    //client.write("get /instrumentation/heading-indicator");
                    //groundSpeed = Double.Parse(client.read());
                    client.write("get /velocities/airspeed-kt");
                    airSpeed = Double.Parse(client.read());
                    //client.write("get /position/altitiude-ft");
                    //GPSAlt = Double.Parse(client.read());
                    client.write("get /orientation/roll-deg");
                    roll = Double.Parse(client.read());
                    client.write("get /orientation/pitch-deg");
                    pitch = Double.Parse(client.read());
                    //client.write("get /position/altitiude-ft");
                    //AltimeterAlt = Double.Parse(client.read());
                    client.write("get /position/latitude-deg");
                    latitude = Double.Parse(client.read());
                    client.write("get /position/longitude-deg");
                    longitude = Double.Parse(client.read());
                }
            }).Start();
        }

        public void setThrottle(double val)
        {
            client.write("set /controls/engines/current-engine/throttle " + val.ToString());
            client.read();
        }

        public void setAileron(double val)
        {
            client.write("set /controls/flight/aileron " + val.ToString());
            client.read();
        }

        public void setRudder(double val)
        {
            client.write("set /controls/flight/rudder " + val.ToString());
            client.read();
        }

        public void setElevator(double val)
        {
            client.write("set /controls/flight/elevator " + val.ToString());
            client.read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private double heading;
        private double verticalSpeed;
        private double groundSpeed;
        private double airSpeed;
        private double gpsAlt;
        private double roll;
        private double pitch;
        private double altimeterAlt;
        private double latitude;
        private double longitude;
        private Tuple<double, double> location;

        public MyFlightSimulator(ITelnetClient client)
        {
            this.client = client;
        }
        public double Heading
        {
            get { return heading; }
            set
            {
                heading = value;
                NotifyPropertyChanged("Heading");
            }
        }
        public double VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }
        public double GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }
        public double AirSpeed
        {
            get { return airSpeed; }
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public double GPSAlt
        {
            get { return gpsAlt; }
            set
            {
                gpsAlt = value;
                NotifyPropertyChanged("GPSAlt");
            }
        }
        public double Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public double Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public double AltimeterAlt
        {
            get { return altimeterAlt; }
            set
            {
                altimeterAlt = value;
                NotifyPropertyChanged("AltimeterAlt");
            }
        }
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        public Tuple<double, double> Location
        {
            get { return location; }
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
            new Thread(delegate ()
            {
                while (true)
                {
                    client.write("get /orientation/heading-deg");
                    heading = Double.Parse(client.read());
                    client.write("get /velocities/vertical-speed-fps");
                    verticalSpeed = Double.Parse(client.read());
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

        public void NotifyPropertyChanged(string propName)
        {
            if (this.propertyChanged != null)
            {
                this.propertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}

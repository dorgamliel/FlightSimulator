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
        private Mutex mtx = new Mutex();
        public event PropertyChangedEventHandler PropertyChanged;
        private string message;
        private string heading;
        private string verticalSpeed;
        private string groundSpeed;
        private string airSpeed;
        private string gpsAlt;
        private string roll;
        private string pitch;
        private string altimeterAlt;
        private string latitude;
        private string longitude;
        private double throttle;
        private double aileron;
        private double rudder;
        private double elevator;
        private bool connected = true;
        

        public MyFlightSimulator(ITelnetClient client)
        {
            this.client = client;
        }
        public string Heading
        {
            get { return heading; }
            set
            {
                heading = value;
                NotifyPropertyChanged("Heading");
            }
        }
        public string VerticalSpeed
        {
            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                NotifyPropertyChanged("VerticalSpeed");
            }
        }
        public string GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }
        public string AirSpeed
        {
            get { return airSpeed; }
            set
            {
                airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public string GPSAlt
        {
            get { return gpsAlt; }
            set
            {
                gpsAlt = value;
                NotifyPropertyChanged("GPSAlt");
            }
        }
        public string Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public string Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public string AltimeterAlt
        {
            get { return altimeterAlt; }
            set
            {
                altimeterAlt = value;
                NotifyPropertyChanged("AltimeterAlt");
            }
        }
        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }
        public string Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }
        public double Throttle
        {
            get { return throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
                
            }
        }
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public bool MessageInd
        {
            get { return connected; }
            set
            {
                connected = value;
                NotifyPropertyChanged("MessageInd");
            }
        }

        public string Message
        { 
            get { return message; }
            set
            {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public void connect(string ip, int port)
        {
            client = new MyTelnetClient();
            try
            {
                client.connect(ip, port);
                MessageInd = true;
                Message = "Connected to server.";
            } catch (Exception e)
            {
                MessageInd = true;
                Message = "Unable to connect to server.";
            }
            
        }

        public void disconnect()
        {
            client.disconnect();
            MessageInd = true;
            Message = "Disconnected from server.";
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (true)
                {
                    mtx.WaitOne();
                    client.write("get /orientation/heading-deg");
                    Heading = (client.read());
                    client.write("get /velocities/vertical-speed-fps");
                    VerticalSpeed = (client.read());
                    client.write("get /instrumentation/heading-indicator");
                    GroundSpeed = (client.read());
                    client.write("get /velocities/airspeed-kt");
                    AirSpeed = (client.read());
                    client.write("get /position/altitiude-ft");
                    GPSAlt = (client.read());
                    client.write("get /orientation/roll-deg");
                    Roll = (client.read());
                    client.write("get /orientation/pitch-deg");
                    Pitch = (client.read());
                    client.write("get /position/altitiude-ft");
                    AltimeterAlt = (client.read());
                    client.write("get /position/latitude-deg");
                    Latitude = (client.read());
                    client.write("get /position/longitude-deg");
                    Longitude = (client.read());
                    /*
                     * DO WE NEED TO READ JOYSTICK AND RUDDER VALUES?
                    client.write("get /controls/engines/current-engine/throttle");
                    throttle = Double.Parse(client.read());
                    client.write("get /controls/flight/aileron");
                    aileron = Double.Parse(client.read());
                    client.write("get /controls/flight/rudder");
                    rudder = Double.Parse(client.read());
                    client.write("get /controls/flight/elevator");
                    elevator = Double.Parse(client.read());
                    */
                    mtx.ReleaseMutex();
                    Thread.Sleep(250);
                }
            }).Start();
        }

        public void setThrottle(double val)
        {
            mtx.WaitOne();
            client.write("set /controls/engines/current-engine/throttle " + val.ToString());
            client.read();
            mtx.ReleaseMutex();
        }

        public void setAileron(double val)
        {
            mtx.WaitOne();
            client.write("set /controls/flight/aileron " + val.ToString());
            client.read();
            mtx.ReleaseMutex();
        }

        public void setRudder(double val)
        {
            mtx.WaitOne();
            client.write("set /controls/flight/rudder " + val.ToString());
            client.read();
            mtx.ReleaseMutex();
        }

        public void setElevator(double val)
        {
            mtx.WaitOne();
            client.write("set /controls/flight/elevator " + val.ToString());
            client.read();
            mtx.ReleaseMutex();
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}

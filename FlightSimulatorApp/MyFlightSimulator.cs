using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    enum PropName { THROTTLE, AILERON, RUDDER, ELEVATOR}
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
        private string latitude = "0";
        private string longitude = "0";
        private Location location;
        private double throttle;
        private double aileron;
        private double rudder;
        private double elevator;
        private bool connected = false;
        public MyFlightSimulator(ITelnetClient client)
        {
            this.client = client;
            resetDashboard();
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
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                NotifyPropertyChanged("Location");
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
        public bool Connected
        {
            get { return connected; }
            set 
            { 
                connected = value;
                NotifyPropertyChanged("Connected");
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
                Connected = true;
            } catch (Exception e)
            {
                MessageInd = true;
                Connected = false;
                Message = "Unable to connect to server.";
            }
            
        }

        public void disconnect()
        {
            client.disconnect();
            MessageInd = true;
            Connected = false;
            resetDashboard();
            Message = "Disconnected from server.";
        }

        public void start()
        {
            new Thread(delegate ()
            {
                try
                {
                    while (Connected)
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
                        mtx.ReleaseMutex();
                        Thread.Sleep(250);
                    }
                }
                catch (InvalidOperationException e)
                {
                    MessageInd = true;
                    Message = "Disconnected from server.";
                    disconnect();
                    mtx.ReleaseMutex();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    disconnect();
                    mtx.ReleaseMutex();
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                    disconnect();
                    mtx.ReleaseMutex();
                }
                catch (TimeoutException e)
                {
                    //SHOW A TIMEOUT ERROR SIGNAL TO USER
                    Console.WriteLine(e);
                    disconnect();
                    mtx.ReleaseMutex();
                }
            }).Start();
        }

        public void setProp(double val, string propName)
        {
            try
            {
                propName = propName.ToUpper();
                PropName prop = (PropName)Enum.Parse(typeof(PropName), propName);
                switch (prop)
                {
                    case PropName.THROTTLE:
                        mtx.WaitOne();
                        client.write("set /controls/engines/current-engine/throttle " + val.ToString());
                        client.read();
                        mtx.ReleaseMutex();
                        break;
                    case PropName.AILERON:
                        mtx.WaitOne();
                        client.write("set /controls/flight/aileron " + val.ToString());
                        client.read();
                        mtx.ReleaseMutex();
                        break;
                    case PropName.RUDDER:
                        mtx.WaitOne();
                        client.write("set /controls/flight/rudder " + val.ToString());
                        client.read();
                        mtx.ReleaseMutex();
                        break;
                    case PropName.ELEVATOR:
                        mtx.WaitOne();
                        client.write("set /controls/flight/elevator " + val.ToString());
                        client.read();
                        mtx.ReleaseMutex();
                        break;
                    default:
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                disconnect();
                mtx.ReleaseMutex();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                disconnect();
                mtx.ReleaseMutex();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                disconnect();
                mtx.ReleaseMutex();
            }
            catch (TimeoutException e)
            {
                //SHOW A TIMEOUT ERROR SIGNAL TO USER
                Console.WriteLine(e);
                disconnect();
                mtx.ReleaseMutex();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                disconnect();
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public void resetDashboard()
        {
            Heading = "0";
            VerticalSpeed = "0";
            GroundSpeed = "0";
            AirSpeed = "0";
            GPSAlt = "0";
            Roll = "0";
            Pitch = "0";
            AltimeterAlt = "0";
        }

    }
}

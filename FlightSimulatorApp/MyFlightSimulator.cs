﻿using System;
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
        private double throttle;
        private double aileron;
        private double rudder;
        private double elevator;
        private bool connected = true;
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
                    //client.write("get /orientation/heading-deg");
                    //heading = Double.Parse(client.read());
                    //client.write("get /velocities/vertical-speed-fps");
                    //verticalSpeed = Double.Parse(client.read());
                    //client.write("get /instrumentation/heading-indicator");
                    //groundSpeed = Double.Parse(client.read());
                    //client.write("get /velocities/airspeed-kt");
                    //airSpeed = Double.Parse(client.read());
                    //client.write("get /position/altitiude-ft");
                    //GPSAlt = Double.Parse(client.read());
                    //client.write("get /orientation/roll-deg");
                    //roll = Double.Parse(client.read());
                    //client.write("get /orientation/pitch-deg");
                    //pitch = Double.Parse(client.read());
                    //client.write("get /position/altitiude-ft");
                    //AltimeterAlt = Double.Parse(client.read());
                    client.write("get /position/latitude-deg");
                    latitude = Double.Parse(client.read());
                    //client.write("get /position/longitude-deg");
                    //longitude = Double.Parse(client.read());
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

﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.IO;
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
        private bool messageInd = false;
        private Queue<String> setCommands;
        private string ip;
        private int port;

        public MyFlightSimulator(ITelnetClient client)
        {
            this.client = client;
            this.setCommands = new Queue<string>();
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
                try
                {
                    if (Double.Parse(value) > 85)
                    {
                        latitude = "85";
                    }
                    else if (Double.Parse(value) < -85)
                    {
                        latitude = "-85";
                    }
                    else
                    {
                        latitude = value;
                    }
                } catch(Exception)
                {
                    latitude = "0";
                }
                NotifyPropertyChanged("Latitude");
            }
        }
        public string Longitude
        {
            get { return longitude; }
            set
            {
                try
                {
                    if (Double.Parse(value) > 180)
                    {
                        longitude = "180";
                    }
                    else if (Double.Parse(value) < -180)
                    {
                        longitude = "-180";
                    }
                    else
                    {
                        longitude = value;
                    }
                }
                catch (Exception)
                {
                    longitude = "0";
                }
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
                setCommands.Enqueue("set /controls/engines/current-engine/throttle " + value.ToString());
                NotifyPropertyChanged("Throttle");
            }
        }
        public double Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                setCommands.Enqueue("set /controls/flight/aileron " + value.ToString());
                NotifyPropertyChanged("Aileron");
                
            }
        }
        public double Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                setCommands.Enqueue("set /controls/flight/rudder " + value.ToString());
                NotifyPropertyChanged("Rudder");
            }
        }
        public double Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                setCommands.Enqueue("set /controls/flight/elevator " + value.ToString());
                NotifyPropertyChanged("Elevator");
            }
        }
        public bool MessageInd
        {
            get { return messageInd; }
            set
            {
                messageInd = value;
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
            if (!Connected)
            {
                return;
            }
            client.disconnect();
            resetDashboard();
            this.setCommands.Clear();
            MessageInd = true;
            Connected = false;
            Message = "Disconnected from server.";
        }

        public void start()
        {
            new Thread(delegate ()
            {
                StartClient();
                try
                {
                    while (Connected)
                    {
                        mtx.WaitOne();
                        client.write("get /instrumentation/heading-indicator/indicated-heading-deg");
                        Heading = (client.read());
                        client.write("get /instrumentation/gps/indicated-vertical-speed");
                        VerticalSpeed = (client.read());
                        client.write("get /instrumentation/gps/indicated-ground-speed-kt");
                        GroundSpeed = (client.read());
                        client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                        AirSpeed = (client.read());
                        client.write("get /instrumentation/gps/indicated-altitude-ft");
                        GPSAlt = (client.read());
                        client.write("get /instrumentation/attitude-indicator/internal-roll-deg");
                        Roll = (client.read());
                        client.write("get /instrumentation/attitude-indicator/internal-pitch-deg");
                        Pitch = (client.read());
                        client.write("get /instrumentation/altimeter/indicated-altitude-ft");
                        AltimeterAlt = (client.read());
                        client.write("get /position/latitude-deg");
                        Latitude = (client.read());
                        client.write("get /position/longitude-deg");
                        Longitude = (client.read());
                        NotifyPropertyChanged("Location");
                        mtx.ReleaseMutex();
                        Thread.Sleep(250);
                    }
                }
                catch (InvalidOperationException e)
                {
                    if (Connected)
                    {
                        disconnect();
                        Message = "Server terminated unexpectedly.";
                        MessageInd = true;
                    }
                    mtx.ReleaseMutex();
                }
                catch (ArgumentNullException e)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (SocketException e)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (IOException e)
                {
                    disconnect();
                    if (e.Message.Contains("connected party did not properly respond after a period of time"))
                    {
                        Message = "Server timed out. Disconnected";
                    }
                    else
                    {
                        Message = "Server terminated unexpectedly.";
                    }
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
            }).Start();
        }

        public void StartClient()
        {
            new Thread(delegate ()
            {
                while (Connected)
                {
                    mtx.WaitOne();
                    if (this.setCommands.Count == 0)
                    {
                        mtx.ReleaseMutex();
                        continue;
                    }
                    try
                    {
                        String command = this.setCommands.Dequeue();
                        client.write(command);
                        client.read();
                        mtx.ReleaseMutex();
                    }
                    catch (InvalidOperationException e)
                    {
                        disconnect();
                        Message = "Server terminated unexpectedly.";
                        MessageInd = true;
                        mtx.ReleaseMutex();
                    }
                    catch (ArgumentNullException e)
                    {
                        disconnect();
                        Message = "Server terminated unexpectedly.";
                        MessageInd = true;
                        mtx.ReleaseMutex();
                    }
                    catch (SocketException e)
                    {
                        disconnect();
                        Message = "Server terminated unexpectedly.";
                        MessageInd = true;
                        mtx.ReleaseMutex();
                    }
                    catch (TimeoutException e)
                    {
                        disconnect();
                        Message = "Server terminated unexpectedly.";
                        MessageInd = true;
                        mtx.ReleaseMutex();
                    }
                    catch (ArgumentException e)
                    {
                        disconnect();
                        Message = "Server terminated unexpectedly.";
                        MessageInd = true;
                        mtx.ReleaseMutex();
                    }
                    catch (IOException e)
                    {
                        disconnect();
                        if (e.Message.Contains("connected party did not properly respond after a period of time"))
                        {
                            Message = "Server timed out. Disconnected";
                        }
                        else
                        {
                            Message = "Server terminated unexpectedly.";
                        }
                        MessageInd = true;
                        mtx.ReleaseMutex();
                    }
                }




                /*
                mtx.WaitOne();
                if (!Connected)
                {
                    mtx.ReleaseMutex();
                    return;
                }

                try
                {

                    propName = propName.ToUpper();
                    PropName prop = (PropName)Enum.Parse(typeof(PropName), propName);
                    switch (prop)
                    {
                        case PropName.THROTTLE:
                            client.write("set /controls/engines/current-engine/throttle " + val.ToString());
                            client.read();
                            mtx.ReleaseMutex();
                            break;
                        case PropName.AILERON:
                            client.write("set /controls/flight/aileron " + val.ToString());
                            client.read();
                            mtx.ReleaseMutex();
                            break;
                        case PropName.RUDDER:
                            client.write("set /controls/flight/rudder " + val.ToString());
                            client.read();
                            mtx.ReleaseMutex();
                            break;
                        case PropName.ELEVATOR:
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
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (ArgumentNullException e)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (SocketException e)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (TimeoutException e)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (ArgumentException e)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                catch (IOException)
                {
                    disconnect();
                    Message = "Server terminated unexpectedly.";
                    MessageInd = true;
                    mtx.ReleaseMutex();
                }
                */
            }).Start();
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

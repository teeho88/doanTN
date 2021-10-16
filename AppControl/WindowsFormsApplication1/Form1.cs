using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using Anime;
using System.Diagnostics;
using System.Security;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Adr_text.Text = "---.---.---.---";
            RunCMD("netsh interface ipv4 set address name=\"Wi-Fi\" static 192.168.1.100 255.255.255.0 192.168.1.1");
            Thread waitAP = new Thread(new ThreadStart(() => {
                while (true)
                {
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    if (myIP.Contains("192."))
                    {
                        MyMarshalToForm("Adr_text", myIP);
                        MyMarshalToForm("En_Adr_Port","");
                        break;
                    }
                    else
                    {
                        MyMarshalToForm("Adr_text", "---.---.---.---");
                        Thread.Sleep(100);
                    }
                }
            }));
            waitAP.Start();
            Ani = new MainWindow();
            Graph = new Form2();
        }

        private void RunCMD(string cmdCommand)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = "cmd.exe";
            proc.Verb = "runas";
            proc.Arguments = "/c " + cmdCommand;
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            Process p = new Process();
            p.StartInfo = proc;
            p.Start();
            p.WaitForExit(100);
        }

        TcpListener serverListener;
        Socket TCPSocket;
        UdpClient UDPSocket;
        IPEndPoint remoteEndpoint;
        Thread startSocket;
        private void Creat_S_Click(object sender, EventArgs e)
        {
            if (CreateS_BT.Text == "Open server")
            {
                CreateS_BT.Text = "Close server";
                start = true;
                if (TCPchooseBT.Checked)
                {
                    serverListener = new TcpListener(IPAddress.Any, int.Parse(Port_text.Text));
                    serverListener.Start();
                    MyMarshalToForm("AddItemToListBox", ">> TCP server Started");
                    R_button.Enabled = true;
                }
                if (UDPchooseBT.Checked)
                {
                    UDPSocket = new UdpClient(int.Parse(Port_text.Text));
                    remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                    R_button.Enabled = false;
                    MyMarshalToForm("AddItemToListBox", ">> UDP server Started");
                }                
                MyMarshalToForm("AddItemToListBox", ">> Waiting for connection from client");
                tick = 0;
                startSocket = new Thread(new ThreadStart(CreateSocket));
                startSocket.Start();
                TCPchooseBT.Enabled = false;
                UDPchooseBT.Enabled = false;
            }
            else
            {
                if (client_ready) SendCommand('e');
                CreateS_BT.Text = "Open server";
                start = false;
                client_ready = false;
                Thread.Sleep(100);
                if (TCPchooseBT.Checked)
                {
                    serverListener.Stop();
                }
                if(UDPchooseBT.Checked)
                {
                    UDPSocket.Close();
                    R_button.Enabled = true;
                }
                MyMarshalToForm("AddItemToListBox", "-----------Server stop!----------");
                Mode = 'Q';
                TCPchooseBT.Enabled = true;
                UDPchooseBT.Enabled = true;
            }
        }

        bool client_ready = false;
        private void CreateSocket()
        {
            while (start)
            {
                client_ready = true;
                if (TCPchooseBT.Checked)
                {
                    try
                    {
                        TCPSocket = serverListener.AcceptSocket();
                    }
                    catch (Exception)
                    {
                        client_ready = false;
                        break;
                    }
                    MyMarshalToForm("AddItemToListBox", "Connection received from " + TCPSocket.RemoteEndPoint);
                }
                if(UDPchooseBT.Checked)
                {
                    Read_client = new Thread(new ThreadStart(ReadUDPStream));
                    Read_client.Start();
                }
                while (client_ready);
            }
        }

        Thread Read_client;
        private void R_button_Click(object sender, EventArgs e)
        {
            if (CreateS_BT.Text == "Close server")
            {
                listBox1.Items.Add(">> Listen to client!");
                Read_client = new Thread(new ThreadStart(ReadTCPStream));
                Read_client.Start();
            }
            else MessageBox.Show("Open server!");
        }

        private float[] data = new float[6];
        bool start = false;
        private void ReadTCPStream()
        {
            try
            {
                while (!client_ready) ;
                NetworkStream stream = new NetworkStream(TCPSocket);
                StreamReader reader = new StreamReader(stream);
                string str = null;
                int timeout = 0;
                while (CreateS_BT.Text == "Close server" && client_ready)
                {
                    str = reader.ReadLine();
                    if (str == null)
                    {
                        if (timeout > 5) break;
                        timeout++;
                        continue;
                    }
                    string[] dataChoose, dataStr;
                    try
                    {
                        switch (Mode)
                        {
                            case 'Q':
                                dataChoose = str.Split('!');
                                dataStr = dataChoose[1].Split(',');
                                if (dataStr.Length != 5) continue;
                                for (int i = 0; i < 5; i++) data[i] = float.Parse(dataStr[i]);
                                MyMarshalToForm("AddItemToListBox", "X: " + data[0]);
                                MyMarshalToForm("AddItemToListBox", "Y: " + data[1]);
                                MyMarshalToForm("AddItemToListBox", "Pitch: " + data[2]);
                                MyMarshalToForm("AddItemToListBox", "Roll: " + data[3]);
                                MyMarshalToForm("AddItemToListBox", "Head: " + data[4]);
                                // Extern kalman filter

                                break;
                            case 'M':
                                dataChoose = str.Split('!');
                                dataStr = dataChoose[1].Remove(dataChoose[1].Length - 2).Split(',');
                                if (dataStr.Length != 6) continue;
                                for (int i = 0; i < 6; i++) data[i] = float.Parse(dataStr[i]);
                                MyMarshalToForm("AddItemToListBox", "ax: " + data[0]);
                                MyMarshalToForm("AddItemToListBox", "ay: " + data[1]);
                                MyMarshalToForm("AddItemToListBox", "az: " + data[2]);
                                MyMarshalToForm("AddItemToListBox", "gx: " + data[3]);
                                MyMarshalToForm("AddItemToListBox", "gy: " + data[4]);
                                MyMarshalToForm("AddItemToListBox", "gz: " + data[5]);
                                break;
                            default: MyMarshalToForm("AddItemToListBox", str); break;
                        }
                    }
                    catch { MyMarshalToForm("AddItemToListBox", "Mode is now wrong!"); }
                    timeout = 0;
                }
                stream.Close();
                TCPSocket.Close();
                client_ready = false;
                MyMarshalToForm("AddItemToListBox", "Client is disconnected!");
            }
            catch (IOException)
            {
                MyMarshalToForm("AddItemToListBox", "An error occured during waiting for client's data!");
            }
            catch (TimeoutException)
            {
                MyMarshalToForm("AddItemToListBox", "Client stopped sending data!");
                MyMarshalToForm("ChangeCreateS_BT", "Open server");
                TCPchooseBT.Enabled = true;
                UDPchooseBT.Enabled = true;
            }
            catch (ObjectDisposedException)
            {
                MyMarshalToForm("AddItemToListBox", "Connecting is not avalable!");
                MyMarshalToForm("ChangeCreateS_BT", "Open server");
                TCPchooseBT.Enabled = true;
                UDPchooseBT.Enabled = true;
            }
        }

        private void ReadUDPStream()
        {
            try
            {
                while (!client_ready) ;
                string str = null;
                int timeout = 0;
                while (CreateS_BT.Text == "Close server" && client_ready)
                {
                    try
                    {
                        byte[] buffer = UDPSocket.Receive(ref remoteEndpoint);
                        str = Encoding.ASCII.GetString(buffer);
                    }
                    catch
                    {
                        str = null;
                        MyMarshalToForm("AddItemToListBox", "Remote endpoint stops!");
                    }
                    if (str == null)
                    {
                        if (timeout > 5) break;
                        timeout++;
                        continue;
                    }
                    string[] dataChoose, dataStr;
                    try
                    {
                        switch (Mode)
                        {
                            case 'Q':
                                dataChoose = str.Split('!');
                                dataStr = dataChoose[1].Split(',');
                                if (dataStr.Length != 5) continue;
                                for (int i = 0; i < 5; i++) data[i] = float.Parse(dataStr[i]);
                                MyMarshalToForm("AddItemToListBox", "X: " + data[0]);
                                MyMarshalToForm("AddItemToListBox", "Y: " + data[1]);
                                MyMarshalToForm("AddItemToListBox", "Pitch: " + data[2]);
                                MyMarshalToForm("AddItemToListBox", "Roll: " + data[3]);
                                MyMarshalToForm("AddItemToListBox", "Head: " + data[4]);
                                // Extern kalman filter

                                break;
                            case 'M':
                                dataChoose = str.Split('!');
                                dataStr = dataChoose[1].Remove(dataChoose[1].Length - 2).Split(',');
                                if (dataStr.Length != 6) continue;
                                for (int i = 0; i < 6; i++) data[i] = float.Parse(dataStr[i]);
                                MyMarshalToForm("AddItemToListBox", "ax: " + data[0]);
                                MyMarshalToForm("AddItemToListBox", "ay: " + data[1]);
                                MyMarshalToForm("AddItemToListBox", "az: " + data[2]);
                                MyMarshalToForm("AddItemToListBox", "gx: " + data[3]);
                                MyMarshalToForm("AddItemToListBox", "gy: " + data[4]);
                                MyMarshalToForm("AddItemToListBox", "gz: " + data[5]);
                                break;
                            default: MyMarshalToForm("AddItemToListBox", str); break;
                        }
                    }
                    catch
                    {
                        MyMarshalToForm("AddItemToListBox", "Mode is now wrong!");
                    }
                    timeout = 0;
                }
            }
            catch (IOException)
            {
                MyMarshalToForm("AddItemToListBox", "An error occured during waiting for client's data!");
            }
            catch (TimeoutException)
            {
                MyMarshalToForm("AddItemToListBox", "Client stopped sending data!");
                MyMarshalToForm("ChangeCreateS_BT", "Open server");
                TCPchooseBT.Enabled = true;
                UDPchooseBT.Enabled = true;
            }
            catch (ObjectDisposedException)
            {
                MyMarshalToForm("AddItemToListBox", "Connecting is not avalable!");
                MyMarshalToForm("ChangeCreateS_BT", "Open server");
                TCPchooseBT.Enabled = true;
                UDPchooseBT.Enabled = true;
            }
        }

        //**********************THREAD***********************

        private delegate void MarshalToForm(String action, String textToAdd);
        private void MyMarshalToForm(String action, String textToDisplay)
        {
            try
            {
                object[] args = { action, textToDisplay };
                MarshalToForm MarshalToFormDelegate = new MarshalToForm(AccessForm);

                //  Execute AccessForm, passing the parameters in args.

                base.Invoke(MarshalToFormDelegate, args);
            }
            catch (Exception) { };
        }

        private void AccessForm(String action, String formText)
        {
           
          switch (action)
          {
             case "AddItemToListBox":
                    listBox1.Items.Add(formText);
                    if (autoScrollChB.Checked)
                    {
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        listBox1.SelectedIndex = -1;
                    }
                    break;
             case "ClearList":
                    listBox1.Items.Clear();
                    break;
             case "ChangeCreateS_BT":
                    CreateS_BT.Text = formText;
                    break;
             case "Adr_text":
                    Adr_text.Text = formText;
                    break;
                case "En_Adr_Port":
                    Adr_text.Enabled = true;
                    Port_text.Enabled = true;
                    break;
                default: break;
          }
        }

        //**********************THREAD***************************

        private void CL_lbox_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        char Mode = 'Q';
        private void Send_BT_Click(object sender, EventArgs e)
        {
            string cmd = D_text.Text.ToUpper();
            byte[] sendBytes = Encoding.ASCII.GetBytes("cmd:" + cmd);
            listBox1.Items.Add(">> Command: " + cmd);
            Mode = cmd[0];
            if (UDPchooseBT.Checked && remoteEndpoint.ToString() == "0.0.0.0:0") return;
            if (client_ready)
            {
                try
                {
                    if (TCPchooseBT.Checked)
                    {
                        TCPSocket.Send(sendBytes);
                    }

                    if (UDPchooseBT.Checked)
                    {
                        UDPSocket.Send(sendBytes, sendBytes.Length, remoteEndpoint);
                    }
                }
                catch (ObjectDisposedException)
                {
                    listBox1.Items.Add(">> Overload command!");
                }
            }
            else listBox1.Items.Add(">> Client has not been ready yet!");
        }

        Form2 Graph;
        Stopwatch GraphStopW;
        private void Gr_button_Click(object sender, EventArgs e)
        {
            Graph = new Form2();
            if (D_text.Text.Length > 0)
            {
                if (Mode == 'Q')
                {
                    if (D_text.Text.Length > 1)
                    {
                        string Val_str = "";
                        switch (D_text.Text.ToUpper()[1])
                        {
                            case 'P': Val_str = "Pitch"; break;
                            case 'R': Val_str = "Roll"; break;
                            case 'Y': Val_str = "Yaw"; break;
                            default: break;
                        }
                        Graph.Xmin = 0;
                        Graph.Xmax = 80000;
                        Graph.Ymin = -5;
                        Graph.Ymax = 5;
                        Graph.GraphTitle = "Đo góc";
                        Graph.XTitle = "thời gian (ms)";
                        Graph.YTitle = Val_str + " (degree)";
                        Graph.Notation = Val_str;
                    }
                    else
                    {
                        Graph.Xmin = -100;
                        Graph.Xmax = 100;
                        Graph.Ymin = -100;
                        Graph.Ymax = 100;
                        Graph.GraphTitle = "Vị trí";
                        Graph.XTitle = "X(m)";
                        Graph.YTitle = "Y(m)";
                        Graph.Notation = "Đường di chuyển";
                    }
                }
                else
                {
                    Graph.Xmin = 0;
                    Graph.Xmax = 80000;
                    Graph.Ymin = -5;
                    Graph.Ymax = 5;
                    Graph.GraphTitle = "Đo giá trị";
                    Graph.XTitle = "thời gian (ms)";
                    Graph.YTitle = "Đại lượng";
                    Graph.Notation = "Giá trị";
                }
                Graph.Show();
                tick = 0;
                if (timer1.Enabled == false)
                {
                    timer1.Enabled = true;
                }
                GraphStopW = Stopwatch.StartNew();
            }
            else MessageBox.Show("Typing measure mode in Command");
        }

        MainWindow Ani;
        private void Ani_BT_Click(object sender, EventArgs e)
        {
            if (Mode == 'Q')
            {
                Ani = new MainWindow();
                Ani.Show();
                if (timer1.Enabled == false)
                {
                    timer1.Enabled = true;
                }
            }
            else MessageBox.Show("Not in quaternion mode!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client_ready)
            {
                SendCommand('e');
                client_ready = false;
            }
            else MessageBox.Show("No client has started yet!");
        }

        private void SendCommand(char c)
        {
            if (client_ready)
            {
                try
                {
                    byte[] sendBytes = new byte[5];
                    for (int i = 0; i < sendBytes.Length; i++) sendBytes[i] = (byte)c;
                    if (TCPchooseBT.Checked)
                    {
                        TCPSocket.Send(sendBytes);
                    }
                    if (UDPchooseBT.Checked)
                    {
                        UDPSocket.Send(sendBytes, sendBytes.Length, remoteEndpoint);
                    }
                }
                catch(ObjectDisposedException)
                {
                    listBox1.Items.Add(">> Overload command!");
                }
                catch (Exception) {;}
            }
        }

        int tick = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case 'Q':
                    if (Graph.IsOpen)
                    {
                        try
                        {
                            if (D_text.Text.Length == 1)
                            {
                                Graph.Draw(data[0], data[1]);
                            }
                            if (D_text.Text.Length > 1)
                            {
                                int i = 2;
                                switch(D_text.Text.ToUpper()[1])
                                {
                                    case 'P': i = 2; break;
                                    case 'R': i = 3; break;
                                    case 'Y': i = 4; break;
                                    default: break;
                                }
                                Graph.Draw(tick, data[i]);
                            }
                        }
                        catch (Exception) {; }
                        tick = (int)GraphStopW.ElapsedMilliseconds;
                    }
                    if (Ani.IsOpen)
                    {
                        Ani.AxisPitch = data[2];
                        Ani.AxisRoll = data[3];
                        Ani.AxisYaw = data[4];
                    };
                break;
                case 'M':
                    if (Graph.IsOpen && D_text.Text.Length > 1)
                    {
                        try
                        {
                            Graph.Draw(tick, data[int.Parse(D_text.Text[1].ToString())]);
                        }
                        catch (Exception) {; }
                        tick = (int)GraphStopW.ElapsedMilliseconds;
                    }
                break;
                default: break;
            }
            if (!Graph.IsOpen)
            {
                if (!Ani.IsOpen)
                {
                    timer1.Enabled = false;
                }
                if(GraphStopW != null) GraphStopW.Stop();
            }
        }

        private void Adr_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string[] ip = Adr_text.Text.Split('.');
                RunCMD("netsh interface ipv4 set address name=\"Wi-Fi\" static "+Adr_text.Text+" 255.255.255.0 "+ip[0]+"."+ip[1]+"."+ip[2]+".1");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client_ready) SendCommand('e');
            if (Graph != null) Graph.Close();
            if (Ani != null) Ani.Close();
            try
            {
                if (serverListener != null) serverListener.Stop();
                if (Read_client != null) if (Read_client.ThreadState == System.Threading.ThreadState.Running) Read_client.Abort();
                if (startSocket != null) if (startSocket.ThreadState == System.Threading.ThreadState.Running) startSocket.Abort();
            }
            catch (Exception f) { MessageBox.Show("Error: " + f.ToString()); }
            RunCMD("netsh interface ipv4 set address name=\"Wi-Fi\" source=dhcp");
        }


        ////////////////////////////////// end class ////////////////////////////////////
    }

}


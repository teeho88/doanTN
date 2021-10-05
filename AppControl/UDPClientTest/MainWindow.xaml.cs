using System;
using System.Collections.Generic;
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
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UDPClientTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            T_BT = "Test";
        }

        Socket client;
        IPEndPoint hostEP;
        private void Test_BT_Click(object sender, RoutedEventArgs e)
        {
            if (T_BT == "Test")
            {
                T_BT = "KT";
                hostEP = new IPEndPoint(IPAddress.Parse(IP.Text), int.Parse(Port.Text));
                client = new Socket(SocketType.Dgram, ProtocolType.Udp);
                Thread test_ = new Thread(new ThreadStart(send));
                test_.Start();
            }
            else T_BT = "Test";
        }

        private void send()
        {
            while (T_BT == "KT")
            {
                string t = "123456789123456789\r\n";
                client.SendTo(Encoding.ASCII.GetBytes(t), hostEP);
                Tx = "1";
                Thread.Sleep(1);
            }
        }

        private string t_BT;
        private string tx;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Tx
        {
            get
            {
                return tx;
            }

            set
            {
                tx = value;
                OnPropertyChanged();
            }
        }

        public string T_BT
        {
            get
            {
                return t_BT;
            }

            set
            {
                t_BT = value;
                OnPropertyChanged();
            }
        }
    }
}

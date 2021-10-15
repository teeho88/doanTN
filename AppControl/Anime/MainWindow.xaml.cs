using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Anime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private float axisYaw;
        private float axisRoll;
        private float axisPitch;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpen = true;
        }

        public float AxisYaw
        {
            get
            {
                return axisYaw;
            }

            set
            {
                axisYaw = value;
                OnPropertyChanged("AxisYaw");
            }
        }

        public float AxisRoll
        {
            get
            {
                return axisRoll;
            }

            set
            {
                axisRoll = value;
                OnPropertyChanged("AxisRoll");
            }
        }

        public float AxisPitch
        {
            get
            {
                return axisPitch;
            }

            set
            {
                axisPitch = value;
                OnPropertyChanged("AxisPitch");
            }
        }

        private bool isOpen = false;
        public bool IsOpen
        {
            get
            {
                return isOpen;
            }

            set
            {
                isOpen = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IsOpen = false;
        }
    }
}

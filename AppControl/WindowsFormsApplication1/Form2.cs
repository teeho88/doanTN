using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public bool IsOpen { get; set; }
        
        public string GraphTitle { get; set; }
        public string XTitle { get; set; }
        public string YTitle { get; set; }
        public string Notation { get; set; }
        public int Xmin { get; set; }
        public int Xmax { get; set; }
        public int Ymin { get; set; }
        public int Ymax { get; set; }
        private GraphPane myPane;
        public GraphPane GraphContent
        {
            get { return myPane; }
            set { myPane = value; }
        }

        public Form2()
        {
            InitializeComponent();
            GraphTitle = ""; XTitle = ""; YTitle = ""; Notation = "";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            IsOpen = true;
            // Khởi tạo ZedGraph
            myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = GraphTitle;
            myPane.XAxis.Title.Text = XTitle;
            myPane.YAxis.Title.Text = YTitle;

            RollingPointPairList list = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve(Notation, list, Color.Red, SymbolType.None);

            myPane.XAxis.Scale.Min = Xmin;
            myPane.XAxis.Scale.Max = Xmax;
            myPane.YAxis.Scale.Min = Ymin;
            myPane.YAxis.Scale.Max = Ymax;

            myPane.AxisChange();
        }

        // Vẽ đồ thị
        public void Draw(double x, double y)
        {
            try
            {
                if (zedGraphControl1.GraphPane.CurveList.Count <= 0) return;

                LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;

                if (curve == null) return;

                IPointListEdit list = curve.Points as IPointListEdit;
                if (list == null) return;
                list.Add(x, y); // Thêm điểm trên đồ thị
                Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
                Scale yScale = zedGraphControl1.GraphPane.YAxis.Scale;
                // Tự động Scale theo trục x
                if (x > xScale.Max - xScale.MajorStep)
                {
                    xScale.Max = x + xScale.MajorStep;
                    xScale.Min = xScale.Max - 30;
                }
                // Tự động Scale theo trục y
                if (y > yScale.Max - yScale.MajorStep)
                {
                    yScale.Max = y + yScale.MajorStep;
                }
                else if (y < yScale.Min + yScale.MajorStep)
                {
                    yScale.Min = y - yScale.MajorStep;
                }
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
                zedGraphControl1.Refresh();
            }
            catch(Exception){ };
        }

        // Xóa đồ thị, với ZedGraph thì phải khai báo lại như ở hàm Form_Load, nếu không sẽ không hiển thị
        public void ClearZedGraph()
        {
            zedGraphControl1.GraphPane.CurveList.Clear(); // Xóa đường
            zedGraphControl1.GraphPane.GraphObjList.Clear(); // Xóa đối tượng

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = GraphTitle;
            myPane.XAxis.Title.Text = XTitle;
            myPane.YAxis.Title.Text = YTitle;

            RollingPointPairList list = new RollingPointPairList(60000);
            LineItem curve = myPane.AddCurve(Notation, list, Color.Red, SymbolType.None);

            myPane.XAxis.Scale.Min = Xmin;
            myPane.XAxis.Scale.Max = Xmax;
            myPane.YAxis.Scale.Min = Ymin;
            myPane.YAxis.Scale.Max = Ymax;

            zedGraphControl1.AxisChange();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            zedGraphControl1.Height = Height - 42;
            zedGraphControl1.Width = Width - 18;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsOpen = false;
        }
    }
}

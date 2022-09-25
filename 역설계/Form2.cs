using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace 역설계
{
    public partial class Form2 : Form
    {
        OleDbConnection conn = null;
        OleDbCommand comm = null;
        OleDbCommand comm2 = null;
        OleDbCommand comm3 = null;
        OleDbDataReader reader = null;

        string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\82105\Desktop\SoilMoisture1.accdb";
        string Time = DateTime.Now.ToString();

        SerialPort sPort;
        private static double xCount = 24;
        List<sensorData> myData = new List<sensorData>();
        public Form2()
        {
            InitializeComponent();
            chartsetting();
        }

        private void chartsetting()
        {
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add("draw");
            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            chart1.ChartAreas["draw"].AxisX.Maximum = xCount;
            chart1.ChartAreas["draw"].AxisX.Interval = 2;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].AxisY.Minimum = 0;
            chart1.ChartAreas["draw"].AxisY.Maximum = 1050;
            chart1.ChartAreas["draw"].AxisY.Interval = 200;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chart1.ChartAreas["draw"].BackColor = Color.Navy;

            chart1.ChartAreas["draw"].CursorX.AutoScroll = true;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chart1.ChartAreas["draw"].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue;

            chart1.Series.Clear();
            chart1.Series.Add("s[0]");
            chart1.Series["s[0]"].ChartType = SeriesChartType.Line;
            chart1.Series["s[0]"].Color = Color.Yellow;
            chart1.Series["s[0]"].BorderWidth = 3;
            if (chart1.Legends.Count > 0)
                chart1.Legends.RemoveAt(0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            sPort = new SerialPort(cb.SelectedItem.ToString());
            sPort.Open();
            sPort.DataReceived += SPort_DataReceived;
        }

        private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sPort.IsOpen)
            {
                //string s = sPort.ReadLine();
                //listBox1.Invoke(new MethodInvoker(delegate { listBox1.Items.Add(s); }));
                //byte[] buffer8 = new byte[8];
                //int totalRead = 0;
                //int readCnt = 0;
                //while (totalRead < 8)
                //{
                //    readCnt = sPort.Read(buffer8, totalRead, 8 - totalRead);
                //    totalRead += readCnt;
                //}
                //Int32[] soil = new int[2];
                //soil[0] = BitConverter.ToInt32(buffer8, 0);
                //soil[1] = BitConverter.ToInt32(buffer8, 4);
                string d = sPort.ReadLine();
                string d1 = sPort.ReadLine();
                int.TryParse(d, out int s);
                int.TryParse(d1, out int s1);

                this.BeginInvoke((new Action(delegate { showValue(s); })));

                //this.BeginInvoke((new Action(delegate { showValue(soil); })));

            }


        }

        private void showValue(int s)
        {
            double sv1 = Math.Round(100 - (((double)s - 300) / 723) * 100,1);

            sensorData data = new sensorData(
               DateTime.Now.ToShortDateString(),
               DateTime.Now.ToString("HH:mm:ss"), (int)sv1);

            myData.Add(data);
            string item = DateTime.Now.ToString() + "\t" + s + "\t" + sv1 + "%";
            listBox1.Items.Add(item);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            chart1.Series["s[0]"].Points.Add(s);
            chart1.ChartAreas["draw"].AxisX.Minimum = 0;
            chart1.ChartAreas["draw"].AxisX.Maximum = (myData.Count >= xCount) ? myData.Count : xCount;


            string query = "INSERT INTO SoilSensorDB (SenSorNum, SDate, SenSorValue, Moisture) VALUES (0,'" + Time + "','" + s + "','" + sv1 + "')";


            conn = new OleDbConnection(connStr);
            conn.Open();
            comm3 = new OleDbCommand(query, conn);
            comm3.ExecuteNonQuery();
            conn.Close();
            conn = null;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("COM3");
        }
    }
}

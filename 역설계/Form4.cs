using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 역설계
{
    public partial class Form4 : Form
    {
        Thread watchThread = null;
        List<sensorData> myData = new List<sensorData>();
        OleDbConnection conn = null;
        OleDbCommand comm = null;
        OleDbCommand comm2 = null;
        OleDbCommand comm3 = null;
        OleDbDataReader reader = null;
        
        string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\82105\Desktop\SoilMoisture1.accdb";
        string Time = DateTime.Now.ToString();
        string Moisture = "3%";
        public Form4()
        {
            InitializeComponent();
           
        }

        
        

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new OleDbConnection(connStr);
            conn.Open();

            listBox1.Items.Clear();

            //string sql = "SELECT * FROM SoilSensorDB";
            string date = DateTime.Now.ToString();

            listBox1.Items.Add(date);
            string sq12 = "SELECT * FROM SoilSensorDB WHERE SenSorNum=0";
            comm2 = new OleDbCommand(sq12, conn);
            reader = comm2.ExecuteReader();


            while (reader.Read())
            {
                string x = "";
                x += reader["SDate"] + "\t";
                x += reader["SEnsorValue"] + "\t";
                x += reader["Moisture"];

                listBox1.Items.Add(x);

            }
            reader.Close();
        }
    }
}

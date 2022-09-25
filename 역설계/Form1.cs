using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 역설계
{
    public partial class Form1 : Form
    {
        /*OleDbConnection conn = null;
        OleDbCommand comm = null;
        OleDbCommand comm2 = null;
        OleDbCommand comm3 = null;
        OleDbDataReader reader = null;

        string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\82105\Desktop\SoilMoisture1.accdb";
        string Time = DateTime.Now.ToString();
        string Moisture = "55%";*/

        public Form1()
        {
            
            InitializeComponent();
            /*
            string query = "INSERT INTO SoilSensorDB(SenSorNum, SDate, Time, SenSorValue, Moisture) VALUES (0, '"+ Time +"', '"+sv1+"', '"+Moisture + "')";

            conn = new OleDbConnection(connStr);
            conn.Open();
            comm3 = new OleDbCommand(query, conn);
            comm3.ExecuteNonQuery();
            */
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 화분1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.Show();
        }
    }
}

﻿using System;
using System.Windows.Forms;

namespace 역설계
{
    internal class sensorData
    {
        public DateTime dt;
        public int value;
        private string v1;
        private string v2;
        private int v3;

        public sensorData(DateTime dt, int value)
        {
            this.dt = dt;
            this.value = value;
        }

        public sensorData(string v1, string v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }
}
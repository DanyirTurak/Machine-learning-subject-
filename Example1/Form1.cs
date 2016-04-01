using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Timer t = new Timer();
            t.Interval = 1000;
            t.Tick += ChangeProgressBarValue;
            t.Start();
        }

        private void ChangeProgressBarValue(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }


    public class MyButton : Button
    {

    }
}

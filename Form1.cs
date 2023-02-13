using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show(VersionManager.GetCurrentVersion());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button was clicked version 1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button was clicked version 2");
        }
    }
}

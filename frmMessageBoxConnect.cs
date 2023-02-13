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
    public partial class frmMessageBoxConnect : Form
    {
        public frmMessageBoxConnect(string ex)
        {
            InitializeComponent();
            txtDetailError.Text = ex;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

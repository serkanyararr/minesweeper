using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MayinTarlasi
{
    public partial class frmOyunOnun : Form
    {
        public frmOyunOnun()
        {
            InitializeComponent();
        }
        public frmOyunOnun(int ss)
        {
            InitializeComponent();
            ssure(ss);
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ssure(int s)
        {
            lblSure.Text = "Süre: " + s.ToString() + " saniye";
            lblTarih.Text = "Tarih: " + DateTime.Now.Date;
        }
    }
}

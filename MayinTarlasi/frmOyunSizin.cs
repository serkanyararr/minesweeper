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
    public partial class frmOyunSizin : Form
    {
        public frmOyunSizin()
        {
            InitializeComponent();
        }
        public frmOyunSizin(int s)
        {
            InitializeComponent();
            sure(s);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           //http://go.microsoft.com/fwlink/?LinkID=130276
        }

        private void frmOyunSizin_Load(object sender, EventArgs e)
        {
            
        }
        public void sure(int s)
        {
            lblSure.Text = "Süre: "+ s.ToString() + " saniye";
            lblTarih.Text = "Tarih: " + DateTime.Now.Date;
        }
    }
}

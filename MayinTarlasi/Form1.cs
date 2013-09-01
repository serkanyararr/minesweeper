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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Button[,] butonlar;
        int[,] indeks = new int[9, 9]; //Buton indexleri
        int[,] sayilar = new int[9, 9]; //Bomba olmayan butonlardaki sayılar
        Random rnd = new Random();
        int yerlestirilenBombaSayisi = 0;
        int toplamBombaSayisi = 10;
        short sure = 0;
        int acilanAlanSayisi = 0;    

        private void Form1_Load(object sender, EventArgs e)
        {
            Button[,] btn = { 
                            {button1,button2,button3,button4,button5,button6,button7,button8,button9 }, 
                            {button10,button11,button12,button13,button14,button15,button16,button17,button18 },
                            {button19,button20,button21,button22,button23,button24,button25,button26,button27 },
                            {button28,button29,button30,button31,button32,button33,button34,button35,button36 },
                            {button37,button38,button39,button40,button41,button42,button43,button44,button45 },
                            {button46,button47,button48,button49,button50,button51,button52,button53,button54 },
                            {button55,button56,button57,button58,button59,button60,button61,button62,button63 },
                            {button64,button65,button66,button67,button68,button69,button70,button71,button72 },
                          {button73,button74,button75,button76,button77,button78,button79,button80,button81 } };
            butonlar = btn;
            lblMayin.Text = toplamBombaSayisi.ToString();
            OyunKur();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure++;
            lblSure.Text = sure.ToString();
        }
        //------------------Metotlar----------------
        #region Metot 

        private void OyunKur()
        {
            sure = 0;
            lblSure.Text = sure.ToString();
            if(timer1.Enabled==true)
            { timer1.Enabled = false; }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // 1: Boşluk
                    // 0: Bomba
                    //-1: Sayılar
                    butonlar[i, j].Text = "";
                    butonlar[i, j].ImageKey = "btn.PNG"; // Index=0
                    butonlar[i, j].ImageList = ımageList1;
                    indeks[i, j] = 1;
                    sayilar[i, j] = 0;
                }
            }
            bombalarıIndexle();
            sayilariYerlestir();
        }

        private void oyunBittiKontrol()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (butonlar[i, j].ImageKey != "btn.PNG")
                    {
                        acilanAlanSayisi++;
                    }
                }
            }
            if (acilanAlanSayisi == 71)
            {
                acilanAlanSayisi = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (indeks[i, j] == 0)
                        { butonlar[i, j].ImageKey = "win.PNG"; }
                    }
                }
                timer1.Enabled = false;
                frmOyunSizin foyunsizin = new frmOyunSizin(Convert.ToInt32(lblSure.Text));
                foyunsizin.ShowDialog();
                OyunKur();
            }
            else
            {
                acilanAlanSayisi = 0;
            }
        }

        private void bombaylaBitti(int e, int b)
        {
            timer1.Enabled = false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i == e && j == b)
                    { butonlar[i, j].ImageKey = "mine.PNG"; }
                    else if (indeks[i, j] == 0)
                    { butonlar[i, j].ImageKey = "mine2.PNG"; }
                }
            }
        }

        private void bombaKontrol(int i, int j)
        {
            if (timer1.Enabled == false)            {                timer1.Enabled = true;            }
            try
            {
                if (indeks[i, j] == 1)
                {
                    bosAc(i, j);
                }
                else if (indeks[i, j] == 0)
                {
                    butonlar[i, j].ImageKey = "mine.PNG"; //3
                    butonlar[i, j].ImageList = ımageList1;
                    bombaylaBitti(i, j);
                    frmOyunOnun foyunonun = new frmOyunOnun(Convert.ToInt32(lblSure.Text));
                    foyunonun.ShowDialog();
                    OyunKur();
                }
                else if (indeks[i, j] == -1)
                {
                    butonlar[i, j].ImageKey = "bos.PNG"; //2
                    butonlar[i, j].ImageList = ımageList1;
                    butonlar[i, j].Text = sayilar[i, j].ToString();
                    indeks[i, j] = 4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sayilariRenklendir();
            oyunBittiKontrol();
        }

        private void bombalarıIndexle() // Bomba indekslerini random ata
        {
            int x, y;
            for (int i = 0; i < toplamBombaSayisi; )
            {
                x = rnd.Next(1, 9);
                y = rnd.Next(1, 9);
                if (indeks[x, y] == 1)
                {
                    indeks[x, y] = 0;
                    i++;
                    yerlestirilenBombaSayisi++;
                }
            }
        }

        private void sayilariYerlestir() //Bomba etrafındaki sayılar
        {

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (indeks[x, y] == 0) //Bu indexe sahip buton bombaysa
                    {
                        // y + 1 , y - 1
                        if (y != 8)
                        {
                            if (indeks[x, y + 1] != 0)
                            {
                                sayilar[x, y + 1]++;
                                indeks[x, y + 1] = -1;
                            }
                        }
                        if (y != 0)
                        {
                            if (indeks[x, y - 1] != 0)
                            {
                                sayilar[x, y - 1]++;
                                indeks[x, y - 1] = -1;
                            }
                        }
                        // x + 1, x - 1
                        if (x != 8)
                        {
                            if (indeks[x + 1, y] != 0)
                            {
                                sayilar[x + 1, y]++;
                                indeks[x + 1, y] = -1;
                            }
                        }
                        if (x != 0)
                        {
                            if (indeks[x - 1, y] != 0)
                            {
                                sayilar[x - 1, y]++;
                                indeks[x - 1, y] = -1;
                            }
                        }
                        //y + 1 ---- x+1,x-1
                        if (x < 8 && y < 8)
                        {
                            if (indeks[x + 1, y + 1] != 0)
                            {
                                sayilar[x + 1, y + 1]++;
                                indeks[x + 1, y + 1] = -1;
                            }
                        }
                        if (x > 0 && y < 8)
                        {
                            if (indeks[x - 1, y + 1] != 0)
                            {
                                sayilar[x - 1, y + 1]++;
                                indeks[x - 1, y + 1] = -1;
                            }
                        }

                        // y - 1 ---- x+1,x-1
                        if (x < 8 && y > 0)
                        {
                            if (indeks[x + 1, y - 1] != 0)
                            {
                                sayilar[x + 1, y - 1]++;
                                indeks[x + 1, y - 1] = -1;
                            }
                        }
                        if (x > 0 && y > 0)
                        {
                            if (indeks[x - 1, y - 1] != 0)
                            {
                                sayilar[x - 1, y - 1]++;
                                indeks[x - 1, y - 1] = -1;
                            }
                        }
                    }
                }
            }

        }

        private void bosAc(int i, int j)
        {
            //Recursive bir yapı ile eğer basılan buton boş ise sayı görene kadar butonları her yönde aç
            if (indeks[i, j] == 1) //Buton boşsa
            {
                butonlar[i, j].ImageKey = "bos.PNG"; //Index=2
                butonlar[i, j].ImageList = ımageList1;
                indeks[i, j] = 4; //Rastgele bir rakam attık

                //jjjjjjj
                if (j < 8)
                {
                    bosAc(i, j + 1);
                }
                if (j > 0)
                {
                    bosAc(i, j - 1);
                }
                //iiiiiiii
                if (i < 8)
                {
                    bosAc(i + 1, j);
                }
                if (i > 0)
                {
                    bosAc(i - 1, j);
                }
                //iiiiiii----jjjjjjjjjj
                if (i > 0 && j < 8)
                {
                    bosAc(i - 1, j + 1);
                }
                if (i < 8 && j < 8)
                {
                    bosAc(i + 1, j + 1);
                }
                if (i < 8 && j > 0)
                {
                    bosAc(i + 1, j - 1);
                }
                if (i > 0 && j > 0)
                {
                    bosAc(i - 1, j - 1);
                }

            }
            else if (indeks[i, j] == 0)
            {
                //Bomba birşey yapma
            }
            else if (indeks[i, j] == -1)
            {
                butonlar[i, j].ImageKey = "bos.PNG"; //2
                butonlar[i, j].Text = sayilar[i, j].ToString();
                indeks[i, j] = 4;
            }
        }

        private void sayilariRenklendir()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (butonlar[i, j].Text == "1")
                    {
                        butonlar[i, j].ForeColor = System.Drawing.Color.Blue;
                    }
                    if (butonlar[i, j].Text == "2")
                    {
                        butonlar[i, j].ForeColor = System.Drawing.Color.Green;
                    }
                    if (butonlar[i, j].Text == "3")
                    {
                        butonlar[i, j].ForeColor = System.Drawing.Color.Red;
                    }
                    if (butonlar[i, j].Text == "4")
                    {
                        butonlar[i, j].ForeColor = System.Drawing.Color.DarkBlue;
                    }
                    if (butonlar[i, j].Text == "6")
                    {
                        butonlar[i, j].ForeColor = System.Drawing.Color.DarkTurquoise;
                    }
                }
            }
        }

        #endregion
        //------------------MenuStrip---------------
        #region Menu

        private void yeniOyunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OyunKur();
        }

        private void seçeneklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSecenekler forms = new frmSecenekler();
            forms.ShowDialog();
        }

        private void mayınTarlasıHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHakkinda formh = new frmHakkinda();
            formh.ShowDialog();
        }

        #endregion
        // ------------------Butonlar----------------
        #region Butonlar

        private void button1_Click(object sender, EventArgs e)
        { bombaKontrol(0, 0); }

        private void button2_Click(object sender, EventArgs e)
        { bombaKontrol(0, 1); }

        private void button3_Click(object sender, EventArgs e)
        { bombaKontrol(0, 2); }

        private void button4_Click(object sender, EventArgs e)
        { bombaKontrol(0, 3); }

        private void button5_Click(object sender, EventArgs e)
        { bombaKontrol(0, 4); }

        private void button6_Click(object sender, EventArgs e)
        { bombaKontrol(0, 5); }

        private void button7_Click(object sender, EventArgs e)
        { bombaKontrol(0, 6); }

        private void button8_Click(object sender, EventArgs e)
        { bombaKontrol(0, 7); }

        private void button9_Click(object sender, EventArgs e)
        { bombaKontrol(0, 8); }

        private void button10_Click(object sender, EventArgs e)
        { bombaKontrol(1, 0); }

        private void button11_Click(object sender, EventArgs e)
        { bombaKontrol(1, 1); }

        private void button12_Click(object sender, EventArgs e)
        { bombaKontrol(1, 2); }

        private void button13_Click(object sender, EventArgs e)
        { bombaKontrol(1, 3); }

        private void button14_Click(object sender, EventArgs e)
        { bombaKontrol(1, 4); }

        private void button15_Click(object sender, EventArgs e)
        { bombaKontrol(1, 5); }

        private void button16_Click(object sender, EventArgs e)
        { bombaKontrol(1, 6); }

        private void button17_Click(object sender, EventArgs e)
        { bombaKontrol(1, 7); }

        private void button18_Click(object sender, EventArgs e)
        { bombaKontrol(1, 8); }

        private void button19_Click(object sender, EventArgs e)
        { bombaKontrol(2, 0); }

        private void button20_Click(object sender, EventArgs e)
        { bombaKontrol(2, 1); }

        private void button21_Click(object sender, EventArgs e)
        { bombaKontrol(2, 2); }

        private void button22_Click(object sender, EventArgs e)
        { bombaKontrol(2, 3); }

        private void button23_Click(object sender, EventArgs e)
        { bombaKontrol(2, 4); }

        private void button24_Click(object sender, EventArgs e)
        { bombaKontrol(2, 5); }

        private void button25_Click(object sender, EventArgs e)
        { bombaKontrol(2, 6); }

        private void button26_Click(object sender, EventArgs e)
        { bombaKontrol(2, 7); }

        private void button27_Click(object sender, EventArgs e)
        { bombaKontrol(2, 8); }

        private void button28_Click(object sender, EventArgs e)
        { bombaKontrol(3, 0); }

        private void button29_Click(object sender, EventArgs e)
        { bombaKontrol(3, 1); }

        private void button30_Click(object sender, EventArgs e)
        { bombaKontrol(3, 2); }

        private void button31_Click(object sender, EventArgs e)
        { bombaKontrol(3, 3); }

        private void button32_Click(object sender, EventArgs e)
        { bombaKontrol(3, 4); }

        private void button33_Click(object sender, EventArgs e)
        { bombaKontrol(3, 5); }

        private void button34_Click(object sender, EventArgs e)
        { bombaKontrol(3, 6); }

        private void button35_Click(object sender, EventArgs e)
        { bombaKontrol(3, 7); }

        private void button36_Click(object sender, EventArgs e)
        { bombaKontrol(3, 8); }

        private void button37_Click(object sender, EventArgs e)
        { bombaKontrol(4, 0); }

        private void button38_Click(object sender, EventArgs e)
        { bombaKontrol(4, 1); }

        private void button39_Click(object sender, EventArgs e)
        { bombaKontrol(4, 2); }

        private void button40_Click(object sender, EventArgs e)
        { bombaKontrol(4, 3); }

        private void button41_Click(object sender, EventArgs e)
        { bombaKontrol(4, 4); }

        private void button42_Click(object sender, EventArgs e)
        { bombaKontrol(4, 5); }

        private void button43_Click(object sender, EventArgs e)
        { bombaKontrol(4, 6); }

        private void button44_Click(object sender, EventArgs e)
        { bombaKontrol(4, 7); }

        private void button45_Click(object sender, EventArgs e)
        { bombaKontrol(4, 8); }

        private void button46_Click(object sender, EventArgs e)
        { bombaKontrol(5, 0); }

        private void button47_Click(object sender, EventArgs e)
        { bombaKontrol(5, 1); }

        private void button48_Click(object sender, EventArgs e)
        { bombaKontrol(5, 2); }

        private void button49_Click(object sender, EventArgs e)
        { bombaKontrol(5, 3); }

        private void button50_Click(object sender, EventArgs e)
        { bombaKontrol(5, 4); }

        private void button51_Click(object sender, EventArgs e)
        { bombaKontrol(5, 5); }

        private void button52_Click(object sender, EventArgs e)
        { bombaKontrol(5, 6); }

        private void button53_Click(object sender, EventArgs e)
        { bombaKontrol(5, 7); }

        private void button54_Click(object sender, EventArgs e)
        { bombaKontrol(5, 8); }

        private void button55_Click(object sender, EventArgs e)
        { bombaKontrol(6, 0); }

        private void button56_Click(object sender, EventArgs e)
        { bombaKontrol(6, 1); }

        private void button57_Click(object sender, EventArgs e)
        { bombaKontrol(6, 2); }

        private void button58_Click(object sender, EventArgs e)
        { bombaKontrol(6, 3); }

        private void button59_Click(object sender, EventArgs e)
        { bombaKontrol(6, 4); }

        private void button60_Click(object sender, EventArgs e)
        { bombaKontrol(6, 5); }

        private void button61_Click(object sender, EventArgs e)
        { bombaKontrol(6, 6); }

        private void button62_Click(object sender, EventArgs e)
        { bombaKontrol(6, 7); }

        private void button63_Click(object sender, EventArgs e)
        { bombaKontrol(6, 8); }

        private void button64_Click(object sender, EventArgs e)
        { bombaKontrol(7, 0); }

        private void button65_Click(object sender, EventArgs e)
        { bombaKontrol(7, 1); }

        private void button66_Click(object sender, EventArgs e)
        { bombaKontrol(7, 2); }

        private void button67_Click(object sender, EventArgs e)
        { bombaKontrol(7, 3); }

        private void button68_Click(object sender, EventArgs e)
        { bombaKontrol(7, 4); }

        private void button69_Click(object sender, EventArgs e)
        { bombaKontrol(7, 5); }

        private void button70_Click(object sender, EventArgs e)
        { bombaKontrol(7, 6); }

        private void button71_Click(object sender, EventArgs e)
        { bombaKontrol(7, 7); }

        private void button72_Click(object sender, EventArgs e)
        { bombaKontrol(7, 8); }

        private void button73_Click(object sender, EventArgs e)
        {            bombaKontrol(8, 0);        }

        private void button74_Click(object sender, EventArgs e)
        { bombaKontrol(8, 1); }

        private void button75_Click(object sender, EventArgs e)
        { bombaKontrol(8, 2); }

        private void button76_Click(object sender, EventArgs e)
        { bombaKontrol(8, 3); }

        private void button77_Click(object sender, EventArgs e)
        { bombaKontrol(8, 4); }

        private void button78_Click(object sender, EventArgs e)
        { bombaKontrol(8, 5); }

        private void button79_Click(object sender, EventArgs e)
        { bombaKontrol(8, 6); }

        private void button80_Click(object sender, EventArgs e)
        { bombaKontrol(8, 7); }

        private void button81_Click(object sender, EventArgs e)
        { bombaKontrol(8, 8); }

        #endregion

    }
}

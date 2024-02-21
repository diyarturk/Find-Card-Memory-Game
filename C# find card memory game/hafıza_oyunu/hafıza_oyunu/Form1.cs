using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hafıza_oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            zemin();
            gizle();
            listBox1.Visible = false;
            listBox2.Visible = false;
            for (int i= 0;i<20;i++)
            {
                resimler[i].Enabled = false;
            }
            
        }
        public PictureBox[] resimler;
      
        public void zemin()   // Resimler için 5*4 lük bir picturebox matrisi oluşturdum ve boyutunu ayarladım.
        {
            resimler = new PictureBox[20];
            int d = 0;
            for (int i = 0; i < 5; i++)
            {
                for(int j=0;j<4;j++)
                {
                    resimler[d] = new PictureBox();
                    resimler[d].Name = d.ToString();
                    resimler[d].Top = 12+j*110;
                    resimler[d].Left = 12+i*110;
                    resimler[d].Width = 110;
                    resimler[d].Height = 110;
                    resimler[d].SizeMode = PictureBoxSizeMode.StretchImage;
                    this.Controls.Add(resimler[d]);
                    resimler[d].Click += new EventHandler(PictureBox_Click);
                    d++;
                }
            }
        }


        public void rDoldur() //Resimler klasörümdeki resimlerin ismine 1-20 arası sayılar verdim.Bu sayılar
                              //eşliğinde resimleri random bir şekilde listeye atıp butona basıldığında karışık gelmesini sağladım.
        {
            listBox2.Items.Clear();
            for (int i = 1; i <= 20; i++)
            {
                listBox1.Items.Add(i);
            }

            Random r = new Random();

            int sayac = 0;


            while (sayac != 20)
            {
                int rsayi = r.Next(0, listBox1.Items.Count);
                listBox1.SelectedIndex = rsayi;
                resimler[sayac].Image = Image.FromFile("resimler\\" + listBox1.Items[rsayi].ToString() + ".jpg");
                

                listBox2.Items.Add(listBox1.Items[rsayi].ToString());
                listBox1.Items.RemoveAt(rsayi);
                sayac++;
            }
            sıra1.Text = "Oyun Sırası=>";
            sıra2.Text = "";

        }

        public void zemintemizle()//Resimleri sildirip tekrardan karıştırmak için.
        {
            for (int i= 0;i<20;i++)
            {
                this.Controls.Remove(resimler[i]);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)//Oyunu yeniden başlatıp görselleri karışık dağıtım zamanlayıcının tekrar başlamasını sağlamak için.
        {
            for (int i = 0; i < 20; i++)
            {
                resimler[i].Enabled = true;
            }
            bir_oyuncu.Text = "0";
            iki_oyuncu.Text = "0";
            zemintemizle();
            zemin();
            rDoldur();
            timer1.Start();
            
            
        }
        public void gizle()//Resimleri gizleyip siyah nokta resmini göstermesi için.
        {
            for (int i = 0; i < 20; i++)
            {
                resimler[i].Image = Image.FromFile("resimler\\g.jpg");
            }
            
            sıradaki();
        }

        int oyunsirasi = 1;
        public void sıradaki()//Oyun sırası yazısının oyuncu sırasına göre değişmesi.
        {
            if (oyunsirasi % 2 != 0)
            {
                sıra2.Text = "Oyun Sırası=>";
                sıra1.Text = "";
            }
            else
            {
                sıra1.Text = "Oyun Sırası=>";
                sıra2.Text = "";
            }
            kontrol = -1;
            oyunsirasi++;

            
        }
        public void kazanan()//Oyuncuların puanlarının textleri 10 dan büyükse  hangi oyuncunun kazandığını gösterir eşit ise berabere mesajı verir.
        {
            süresay = 5;
            sayac = 0;
           if(Convert.ToInt16(bir_oyuncu.Text)>=11)
            {
                MessageBox.Show("TEBRİKLER BİRİNCİ OYUNCU KAZANDI");
                zemintemizle();
            }
           else if(Convert.ToInt16(iki_oyuncu.Text) >= 11)
            {
                MessageBox.Show("TEBRİKLER İKİNCİ OYUNCU KAZANDI");
                zemintemizle();
            }
           else if(Convert.ToInt16(iki_oyuncu.Text) == 10 && Convert.ToInt16(bir_oyuncu.Text) == 10)
            {
                MessageBox.Show("TEBRİKLER YENİŞEMEDİNİZ BERABERE KALDI");
                zemintemizle();
            }
        }

        int kontrol=-1;
        
        public void tiklanan(int i) //eğer tıklanan resimler aynı ise tebrik mesajı verir ve oyuncunun hanesine 2 puan ekler.
        {
            
            timer1.Start();
            resimler[i].Image = Image.FromFile("resimler\\" + listBox2.Items[i].ToString() + ".jpg");
            if(kontrol!= -1)
            {
                timer1.Stop();
                if (Convert.ToInt16(listBox2.Items[i].ToString()) + 10 == Convert.ToInt16(listBox2.Items[kontrol].ToString()) ||
                    Convert.ToInt16(listBox2.Items[i].ToString())  ==  10+ Convert.ToInt16(listBox2.Items[kontrol].ToString()))
                {
                    timer1.Stop();
                    sayac = 0;
                    tbas = 0;
                    MessageBox.Show("TEBRİKLER");
                    this.Controls.Remove(resimler[i]);
                    this.Controls.Remove(resimler[kontrol]);
                    if (oyunsirasi % 2 != 0)
                    {
                        bir_oyuncu.Text = (Convert.ToInt16(bir_oyuncu.Text) + 2).ToString();
                    }
                    else
                    {
                        iki_oyuncu.Text = (Convert.ToInt16(iki_oyuncu.Text) + 2).ToString();
                    }
                }
                else
                {
                    timer1.Start();
                }
                if(kontrol!=i)
                {
                    kontrol = -1;
                }
                
            }
            else
            {
                kontrol = i;
            }
            kazanan();
        }
        int tbas = 0;
        private void PictureBox_Click(object sender, EventArgs e)// iki resim seçildikten sonra 3.süne basılırsa hata mesajı verir.
                                                                 // Zaten 2 kere basilmişssa tiklanan metodu ile kontrol sağlanır.
        {
            tbas++;
            if(tbas >= 3)
            {
                MessageBox.Show("AYNI ANDA SADECE İKİ SEÇİM YAPILABİLİR");
            }
            else
            {
                PictureBox aa = (sender as PictureBox);

                for (int i = 0; i < 20; i++)
                {
                    if (aa == resimler[i])
                    {
                        tiklanan(i);
                        break;
                    }
                }
            }
            
        }
        int sayac;
        int süresay = 5;
        private void timer1_Tick(object sender, EventArgs e)//Timerin 5 den geriye süreyi sayması için.
        {
            süresay--;
            süre.Text = süresay.ToString();
            
            sayac++;
            if (sayac == 5)
            {
                gizle();
                sayac = 0;
                süresay = 5;
                süre.Text = süresay.ToString();
                timer1.Stop();
                tbas = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

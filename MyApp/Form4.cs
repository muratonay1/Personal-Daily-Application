using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using Tulpep.NotificationWindow;

namespace MyApp
{
    public partial class Form4 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=MyAppData.mdb");
        public string Seçim_Text;
        private int tmr;
        public int sayac = 3;
        public int kontrol = 0;
        public int toplam_bak;
        public int kuveyt_bak;
        public int ziraat_bak;
        public int vakıf_bak;

        public Form4()
        {
            InitializeComponent();
        }
        public void Popup_Notification(string title,string uyarı)
        {
            Font header = new Font("Rajdhani", 16, FontStyle.Bold);
            Font cont = new Font("Rajdhani", 14, FontStyle.Regular);
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.info1;
            popup.TitlePadding = new Padding(50);
            popup.TitleText = title;
            popup.TitleFont = header;
            popup.Size = new Size(500, 200);
            popup.BodyColor = Color.FromArgb(44, 62, 80);
            popup.TitleColor = Color.FromArgb(46, 204, 113);
            popup.BorderColor = Color.FromArgb(243, 156, 18);
            popup.ContentFont = cont;
            popup.ContentColor = Color.White;
            popup.ContentText = uyarı;
            popup.Popup();
        }
        private void ovalShape1_Click(object sender, EventArgs e)
        {

        }

        private void ovalShape1_Click_1(object sender, EventArgs e)
        {
            Seçim_Text = "KuveytTürk Bankası Seçildi";
            label6.Text = Seçim_Text;
            ovalShape1.BorderColor = Color.FromArgb(76, 209, 55);
            ovalShape2.BorderColor = Color.FromArgb(53, 59, 72);
            ovalShape3.BorderColor = Color.FromArgb(53, 59, 72);
            kontrol = 1;
        }

        private void ovalShape2_Click(object sender, EventArgs e)
        {
            Seçim_Text = "VakıfBank Bankası Seçildi";
            label6.Text = Seçim_Text;
            ovalShape3.BorderColor = Color.FromArgb(53, 59, 72);
            ovalShape2.BorderColor = Color.FromArgb(76, 209, 55);
            ovalShape1.BorderColor = Color.FromArgb(53, 59, 72);
            kontrol = 1;
        }

        private void ovalShape3_Click(object sender, EventArgs e)
        {
            Seçim_Text = "ZiraatBank Bankası Seçildi";
            label6.Text = Seçim_Text;
            ovalShape3.BorderColor = Color.FromArgb(76, 209, 55);
            ovalShape2.BorderColor = Color.FromArgb(53, 59, 72);
            ovalShape1.BorderColor = Color.FromArgb(53, 59, 72);
            kontrol = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tmr++;
            if(tmr%5==0)
            {
                label7.Text = sayac.ToString();
                sayac--;
                if(sayac==-1)
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    sayac = 3;
                    tmr = 0;
                    label7.Text = "";
                    label6.ForeColor = Color.FromArgb(220, 221, 225);
                    panel2.BringToFront();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(kontrol==1)
            {
                button2.Enabled = false;
                if(ovalShape1.BorderColor==Color.FromArgb(76, 209, 55))
                {
                    label8.Text = "KuveytTürk";
                }
                else if(ovalShape2.BorderColor == Color.FromArgb(76, 209, 55))
                {
                    label8.Text = "VakıfBank";
                }
                else if(ovalShape3.BorderColor == Color.FromArgb(76, 209, 55))
                {
                    label8.Text = "ZiraatBank";
                }
                label6.ForeColor = Color.FromArgb(68, 189, 50);
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Unknown Selection");
            }           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedValue = "";
            panel1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(label8.Text=="KuveytTürk")
            {
                KuveytTürkInsert();
                panel1.BringToFront();
                button2.Enabled = true;
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.SelectedValue = "";

            }
            else if(label8.Text=="VakıfBank")
            {
                VakıfBankInsert();
                panel1.BringToFront();
                button2.Enabled = true;
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.SelectedValue = "";
            }
            else if(label8.Text=="ZiraatBank")
            {
                ZiraatBankInsert();
                panel1.BringToFront();
                button2.Enabled = true;
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.SelectedValue = "";
            }           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();            
            this.Close();          
        }
        public void KuveytTürkInsert()
        {
            Form2 frm2 = new Form2();
            string query;
            baglanti.Open();
            query = "insert into Kuveyt_Bankası (kullanici_id,kuveyt_aciklama,tarih,tutar,hesap_hareketi) values (@kullanici_id,@kuveyt_aciklama,@tarih,@tutar,@hesap_hareketi)";
            OleDbCommand komut = new OleDbCommand(query,baglanti);
            komut.Parameters.AddWithValue("@kullanici_id", int.Parse(Form2.user_id));

            komut.Parameters.AddWithValue("@kuveyt_aciklama",textBox1.Text);

            komut.Parameters.AddWithValue("@tarih", DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/" + "Saat: " + DateTime.Now.ToString("hh:mm:ss").ToString());

            komut.Parameters.AddWithValue("@tutar", textBox2.Text);

            komut.Parameters.AddWithValue("@hesap_hareketi", comboBox1.SelectedItem.ToString());

            komut.ExecuteNonQuery();
            baglanti.Close();

            KuveytTurkHesapHareketiDeğiştirme();
        }
        public void KuveytTurkHesapHareketiDeğiştirme()
        {
            Form2 frm2 = new Form2();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Gelir_Gider where kullanici_id=" + int.Parse(Form2.user_id);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string toplam_bakiye = ds.Tables[0].Rows[i]["toplam_bakiye"].ToString();
                string kuveyt_bakiye = ds.Tables[0].Rows[i]["kuveyt_bakiye"].ToString();
                toplam_bak = int.Parse(toplam_bakiye);
                kuveyt_bak = int.Parse(kuveyt_bakiye);
                if(comboBox1.SelectedItem.ToString()== "Hesaba Para Girişi")
                {
                    int para = int.Parse(textBox2.Text);
                    kuveyt_bak = kuveyt_bak + para;
                    toplam_bak = toplam_bak + para;
                    Popup_Notification("KuveytTürk Banka İşlemi", "'" + para + "' ₺ tutarında hesabına para aktarıldı.");
                }
                else if(comboBox1.SelectedItem.ToString()== "Hesaptan Para Çıkışı")
                {
                    int para = int.Parse(textBox2.Text);
                    kuveyt_bak = kuveyt_bak - para;
                    toplam_bak = toplam_bak - para;
                    Popup_Notification("KuveytTürk Banka İşlemi", "'" + para + "' ₺ tutarında hesabından para çıkışı gerçekleşti.");
                }               
            }
            baglanti.Close();
            KuveytTurkHesapHareketiGüncelle();
        }
        public void KuveytTurkHesapHareketiGüncelle()
        {
            Form2 frm2 = new Form2();
            string query;
            baglanti.Open();
            query = "update Gelir_Gider set toplam_bakiye='"+toplam_bak+"',kuveyt_bakiye='"+kuveyt_bak+"' where kullanici_id="+int.Parse(Form2.user_id);
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }

        public void ZiraatBankInsert()
        {
            Form2 frm2 = new Form2();
            string query;
            baglanti.Open();
            query = "insert into Ziraat_Bankası (kullanici_id,ziraat_aciklama,tarih,tutar,hesap_hareketi) values (@kullanici_id,@ziraat_aciklama,@tarih,@tutar,@hesap_hareketi)";
            OleDbCommand komut = new OleDbCommand(query, baglanti);
            komut.Parameters.AddWithValue("@kullanici_id", int.Parse(Form2.user_id));

            komut.Parameters.AddWithValue("@ziraat_aciklama", textBox1.Text);

            komut.Parameters.AddWithValue("@tarih", DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/" + "Saat: " + DateTime.Now.ToString("hh:mm:ss").ToString());

            komut.Parameters.AddWithValue("@tutar", textBox2.Text);

            komut.Parameters.AddWithValue("@hesap_hareketi", comboBox1.SelectedItem.ToString());

            komut.ExecuteNonQuery();
            baglanti.Close();

            ZiraatBankHesapHareketiDeğiştirme();
        }
        public void ZiraatBankHesapHareketiDeğiştirme()
        {
            Form2 frm2 = new Form2();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Gelir_Gider where kullanici_id=" + int.Parse(Form2.user_id);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string toplam_bakiye = ds.Tables[0].Rows[i]["toplam_bakiye"].ToString();
                string ziraat_bakiye = ds.Tables[0].Rows[i]["ziraat_bakiye"].ToString();
                toplam_bak = int.Parse(toplam_bakiye);
                ziraat_bak = int.Parse(ziraat_bakiye);
                if (comboBox1.SelectedItem.ToString() == "Hesaba Para Girişi")
                {
                    int para = int.Parse(textBox2.Text);
                    ziraat_bak = ziraat_bak + para;
                    toplam_bak = toplam_bak + para;
                    Popup_Notification("Ziraat Banka İşlemi", "'" + para + "' ₺ tutarında hesabına para aktarıldı.");
                }
                else if (comboBox1.SelectedItem.ToString() == "Hesaptan Para Çıkışı")
                {
                    int para = int.Parse(textBox2.Text);
                    ziraat_bak = ziraat_bak - para;
                    toplam_bak = toplam_bak - para;
                    Popup_Notification("Ziraat Banka İşlemi", "'" + para + "' ₺ tutarında hesabından para çıkışı gerçekleşti.");
                }
            }
            baglanti.Close();
            ZiraatBankHesapHareketiGüncelle();
        }
        public void ZiraatBankHesapHareketiGüncelle()
        {
            Form2 frm2 = new Form2();
            string query;
            baglanti.Open();
            query = "update Gelir_Gider set toplam_bakiye='" + toplam_bak + "',ziraat_bakiye='" + ziraat_bak + "' where kullanici_id=" + int.Parse(Form2.user_id);
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }

        public void VakıfBankInsert()
        {
            Form2 frm2 = new Form2();
            string query;
            baglanti.Open();
            query = "insert into Vakıf_Bankası (kullanici_id,vakıf_aciklama,tarih,tutar,hesap_hareketi) values (@kullanici_id,@vakıf_aciklama,@tarih,@tutar,@hesap_hareketi)";
            OleDbCommand komut = new OleDbCommand(query, baglanti);
            komut.Parameters.AddWithValue("@kullanici_id", int.Parse(Form2.user_id));

            komut.Parameters.AddWithValue("@vakıf_aciklama", textBox1.Text);

            komut.Parameters.AddWithValue("@tarih", DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/" + "Saat: " + DateTime.Now.ToString("hh:mm:ss").ToString());

            komut.Parameters.AddWithValue("@tutar", textBox2.Text);

            komut.Parameters.AddWithValue("@hesap_hareketi", comboBox1.SelectedItem.ToString());

            komut.ExecuteNonQuery();
            baglanti.Close();

            VakıfBankHesapHareketiDeğiştirme();
        }
        public void VakıfBankHesapHareketiDeğiştirme()
        {
            Form2 frm2 = new Form2();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Gelir_Gider where kullanici_id=" + int.Parse(Form2.user_id);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string toplam_bakiye = ds.Tables[0].Rows[i]["toplam_bakiye"].ToString();
                string vakıf_bakiye = ds.Tables[0].Rows[i]["vakıf_bakiye"].ToString();
                toplam_bak = int.Parse(toplam_bakiye);
                vakıf_bak = int.Parse(vakıf_bakiye);
                if (comboBox1.SelectedItem.ToString() == "Hesaba Para Girişi")
                {
                    int para = int.Parse(textBox2.Text);
                    vakıf_bak = vakıf_bak + para;
                    toplam_bak = toplam_bak + para;
                    Popup_Notification("VakıfBank Banka İşlemi", "'" + para + "' ₺ tutarında hesabına para aktarıldı.");
                }
                else if (comboBox1.SelectedItem.ToString() == "Hesaptan Para Çıkışı")
                {
                    int para = int.Parse(textBox2.Text);
                    vakıf_bak = vakıf_bak - para;
                    toplam_bak = toplam_bak - para;
                    Popup_Notification("VakıfBank Banka İşlemi", "'" + para + "' ₺ tutarında hesabından para çıkışı gerçekleşti.");
                }
            }
            baglanti.Close();
            VakıfBankHesapHareketiGüncelle();
        }
        public void VakıfBankHesapHareketiGüncelle()
        {
            Form2 frm2 = new Form2();
            string query;
            baglanti.Open();
            query = "update Gelir_Gider set toplam_bakiye='" + toplam_bak + "',vakıf_bakiye='" + vakıf_bak + "' where kullanici_id=" + int.Parse(Form2.user_id);
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {           
            MessageBox.Show(comboBox1.SelectedItem.ToString());
        }
    }
}

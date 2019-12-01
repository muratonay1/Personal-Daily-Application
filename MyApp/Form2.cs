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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tulpep.NotificationWindow;

namespace MyApp
{
    public partial class Form2 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=MyAppData.mdb");
       
        public int timer=0;
        private int tmr11;
        private int tmr22;
        private int tmr33;
        public int kuveyt_para;
        public int ziraat_para;
        public int vakıf_para;
        public int doğrulama;
        public static string user_id="";
        public Form2()
        {
            InitializeComponent();
        }
        //pop up bilgilendirme
        public void Popup_Notification(string title, string uyarı)
        {
            Font header = new Font("Rajdhani", 18, FontStyle.Bold);
            Font cont = new Font("Rajdhani", 16, FontStyle.Regular);
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.info1;
            popup.TitlePadding = new Padding(10);
            popup.TitleText = title;
            popup.TitleFont = header;
            popup.Size = new Size(500, 200);
            popup.BodyColor = Color.FromArgb(44, 62, 80);
            popup.Delay = 4000;
            popup.TitleColor = Color.FromArgb(46, 204, 113);
            popup.BorderColor = Color.FromArgb(243, 156, 18);
            popup.ContentFont = cont;
            popup.ContentColor = Color.FromArgb(236, 240, 241);
            popup.ContentText = uyarı;
            popup.Popup();
        }
       

        private void button3_Click(object sender, EventArgs e) //Not ekle
        {
            panel1.BringToFront();
        }
       
        
        private void button4_Click(object sender, EventArgs e) //kayıtlı notlar
        {
            textBox2.Text = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/" + "Saat: " + DateTime.Now.ToString("hh:mm:ss");
            flowLayoutPanel1.Controls.Clear();
            ArrayList  Mesaj = new ArrayList();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;           
            string query = "select * from Mesaj where kullanici_id=" + int.Parse(textBox4.Text);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);           
            DataSet ds = new DataSet();
            ds.Merge(dt);
           
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string k_i = ds.Tables[0].Rows[i]["kullanici_id"].ToString();
                string me = ds.Tables[0].Rows[i]["mesaj"].ToString();
                string ta = ds.Tables[0].Rows[i]["tarih"].ToString();
                string m_b = ds.Tables[0].Rows[i]["mesaj_baslik"].ToString();              
                ListeOluştur(me, ta, m_b);

            }          
            baglanti.Close();
            flowLayoutPanel1.BringToFront();
        }

        public void ProjeListele()
        {
            flowLayoutPanel2.Controls.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Projeler where kullanici_id=" + int.Parse(textBox4.Text);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string k_i = ds.Tables[0].Rows[i]["kullanici_id"].ToString();
                string me = ds.Tables[0].Rows[i]["proje_ismi"].ToString();
                string ta = ds.Tables[0].Rows[i]["proje_aciklamasi"].ToString();
                string m_b = ds.Tables[0].Rows[i]["tarih"].ToString();
                ListeOluşturProje(me, m_b, ta);
            }
            label12.Text = ds.Tables[0].Rows.Count.ToString();
           
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e) // hesap ayarları
        {
            panel3.BringToFront();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            user_id = textBox4.Text;
            panel1.BringToFront();          
            textBox2.Text = DateTime.Now.Day+"-"+DateTime.Now.Month+"-"+DateTime.Now.Year+"/"+"Saat: "+DateTime.Now.ToString("hh:mm:ss");
            KuveytVadeTarihiYaz();
        }
        private void ListeOluştur(string headerText,string MessageText,string DateText)     //Flatlist layout panel Tasarımı
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.BackColor = Color.Gray;
            flp.Size = new System.Drawing.Size(486, 225);
            flp.Padding = new Padding(10);
            flp.AutoScroll = true;
            flp.FlowDirection = FlowDirection.TopDown;

            Font header = new Font("Rajdhani", 16,FontStyle.Bold);
            Label lblheader = new Label();
            lblheader.Top = 15;
            lblheader.AutoSize = true;
            lblheader.AutoEllipsis = true;
            lblheader.UseCompatibleTextRendering = true;
            lblheader.Text = headerText;
            lblheader.ForeColor = Color.Black;
            lblheader.Font = header;

            Font txt = new Font("Rajdhani", 12);
            Label lbltxt = new Label();
            lbltxt.AutoSize = true;
            lbltxt.AutoEllipsis = true;
            lbltxt.UseCompatibleTextRendering = true;
            lbltxt.Top = 40;
            lbltxt.Text = MessageText;
            lbltxt.ForeColor = Color.Green;
            lbltxt.Font = header;

            Font date = new Font("Rajdhani", 12);
            Label lbldate = new Label();
            lbldate.Top = 80;
            lbldate.AutoSize = true;
            lbldate.AutoEllipsis = true;
            lbldate.UseCompatibleTextRendering = true;
            lbldate.Text = DateText;
            lbldate.ForeColor = Color.Purple;
            lbldate.Font = date;

            Font btn = new Font("Rajdhani", 12);
            Button btnDelete = new Button();
            btnDelete.Top = 130;
            btnDelete.AutoSize = true;
            btnDelete.AutoEllipsis = true;
            btnDelete.UseCompatibleTextRendering = true;
            btnDelete.Text = "Delete";
            btnDelete.ForeColor = Color.Black;
            
            flp.Controls.Add(lblheader);
            flp.Controls.Add(lbltxt);
            flp.Controls.Add(lbldate);
            flp.Controls.Add(btnDelete);
            flowLayoutPanel1.Controls.Add(flp);                       
        }
        //PROJE LISTELEME LAYOUT PANEL TASARIMI
        private void ListeOluşturProje(string proje_ismi, string tarih, string aciklama)     
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.BackColor = Color.Gray;
            flp.Size = new System.Drawing.Size(450, 182);
            flp.Padding = new Padding(10);
            flp.AutoScroll = true;
            flp.FlowDirection = FlowDirection.TopDown;

            Font header = new Font("Rajdhani", 16, FontStyle.Bold);
            Label lblheader = new Label();
            //lblheader.Top = 15;
            lblheader.AutoSize = true;
            lblheader.AutoEllipsis = true;
            lblheader.UseCompatibleTextRendering = true;
            lblheader.Text = proje_ismi;
            lblheader.ForeColor = Color.Black;
            lblheader.Font = header;

            Font txt = new Font("Rajdhani", 12);
            Label lbltxt = new Label();
            lbltxt.AutoSize = true;
            lbltxt.AutoEllipsis = true;
            lbltxt.UseCompatibleTextRendering = true;
            //lbltxt.Top = 40;
            lbltxt.Text = aciklama;
            lbltxt.ForeColor = Color.White;
            lbltxt.Font = txt;

            Font date = new Font("Rajdhani", 12);
            Label lbldate = new Label();
            //lbldate.Top = 80;
            lbldate.AutoSize = true;
            lbldate.AutoEllipsis = true;
            lbldate.UseCompatibleTextRendering = true;
            lbldate.Text = tarih;
            lbldate.ForeColor = Color.Purple;
            lbldate.Font = date;
            
            flp.Controls.Add(lblheader);
            flp.Controls.Add(lbldate);
            flp.Controls.Add(lbltxt);
                      
            flowLayoutPanel2.Controls.Add(flp);
        }

        //ZIRAAT BANKASI HESAAP HAREKETLERI LISTELEME PANEL TASARIMI
        private void ListeleZiraatHesapHareketleri(string ziraat_aciklama, string tarih, string tutar,string hesap_hareketi) 
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.BackColor = Color.FromArgb(39, 60, 117);
            flp.Size = new System.Drawing.Size(280, 130);
            flp.Padding = new Padding(10);
            flp.AutoScroll = true;
            flp.FlowDirection = FlowDirection.TopDown;

            Font header = new Font("Rajdhani", 14, FontStyle.Bold);
            Label lblheader = new Label();
            //lblheader.Top = 15;
            lblheader.AutoSize = true;
            lblheader.AutoEllipsis = true;
            lblheader.UseCompatibleTextRendering = true;
            lblheader.Text = "Ziraat Hesap Hareketi";
            lblheader.ForeColor = Color.White;
            lblheader.Font = header;

            Font txt = new Font("Rajdhani", 14);
            Label lbltxt = new Label();
            lbltxt.AutoSize = true;
            lbltxt.AutoEllipsis = true;
            lbltxt.UseCompatibleTextRendering = true;
            //lbltxt.Top = 40;
            lbltxt.Text = ziraat_aciklama;
            lbltxt.ForeColor = Color.FromArgb(46, 204, 113);
            lbltxt.Font = txt;

            Font date = new Font("Rajdhani", 10);
            Label lbldate = new Label();
            //lbldate.Top = 80;
            lbldate.AutoSize = true;
            lbldate.AutoEllipsis = true;
            lbldate.UseCompatibleTextRendering = true;
            lbldate.Text = tarih;
            lbldate.ForeColor = Color.FromArgb(251, 197, 49);
            lbldate.Font = date;

            if (hesap_hareketi == "Hesaptan Para Çıkışı")
            {
                Label lbltutar = new Label();
                Font yy = new Font("Rajdhani", 14);
                //lbldate.Top = 80;
                lbltutar.AutoSize = true;
                lbltutar.AutoEllipsis = true;
                lbltutar.UseCompatibleTextRendering = true;
                lbltutar.Text = tutar + " ₺";
                lbltutar.ForeColor = Color.FromArgb(232, 65, 24);
                lbltutar.Font = yy;

                Label lblhesap_hareketi = new Label();
                //lbldate.Top = 80;
                lblhesap_hareketi.AutoSize = true;
                lblhesap_hareketi.AutoEllipsis = true;
                lblhesap_hareketi.UseCompatibleTextRendering = true;
                lblhesap_hareketi.Text = hesap_hareketi;
                lblhesap_hareketi.ForeColor = Color.FromArgb(232, 65, 24);
                lblhesap_hareketi.Font = date;

                flp.Controls.Add(lblheader);
                flp.Controls.Add(lbldate);
                flp.Controls.Add(lbltxt);
                flp.Controls.Add(lbltutar);
                flp.Controls.Add(lblhesap_hareketi);

                flowLayoutPanel7.Controls.Add(flp);
            }
            else if (hesap_hareketi == "Hesaba Para Girişi")
            {
                Label lbltutar = new Label();
                //lbldate.Top = 80;
                Font yy = new Font("Rajdhani", 14);
                lbltutar.AutoSize = true;
                lbltutar.AutoEllipsis = true;
                lbltutar.UseCompatibleTextRendering = true;
                lbltutar.Text = tutar + " ₺";
                lbltutar.ForeColor = Color.FromArgb(251, 197, 49);
                lbltutar.Font = yy;

                Label lblhesap_hareketi = new Label();
                //lbldate.Top = 80;
                lblhesap_hareketi.AutoSize = true;
                lblhesap_hareketi.AutoEllipsis = true;
                lblhesap_hareketi.UseCompatibleTextRendering = true;
                lblhesap_hareketi.Text = hesap_hareketi;
                lblhesap_hareketi.ForeColor = Color.FromArgb(251, 197, 49);
                lblhesap_hareketi.Font = date;

                flp.Controls.Add(lblheader);
                flp.Controls.Add(lbldate);
                flp.Controls.Add(lbltxt);
                flp.Controls.Add(lbltutar);
                flp.Controls.Add(lblhesap_hareketi);

                flowLayoutPanel7.Controls.Add(flp);
            }
        }

        //kUVEYT HESAP HAREKETLERİ PANEL TASARIMI
        private void ListeleKuveytHesapHareketleri(string kuveyt_aciklama,string tarih,string tutar, string hesap_hareketi)
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.BackColor = Color.FromArgb(39, 60, 117);
            flp.Size = new System.Drawing.Size(280, 130);
            flp.Padding = new Padding(10);
            flp.AutoScroll = true;
            flp.FlowDirection = FlowDirection.TopDown;

            Font header = new Font("Rajdhani", 14, FontStyle.Bold);
            Label lblheader = new Label();
            //lblheader.Top = 15;
            lblheader.AutoSize = true;
            lblheader.AutoEllipsis = true;
            lblheader.UseCompatibleTextRendering = true;
            lblheader.Text = "Kuveyt Hesap Hareketi";
            lblheader.ForeColor = Color.White;
            lblheader.Font = header;

            Font txt = new Font("Rajdhani", 14);
            Label lbltxt = new Label();
            lbltxt.AutoSize = true;
            lbltxt.AutoEllipsis = true;
            lbltxt.UseCompatibleTextRendering = true;
            //lbltxt.Top = 40;
            lbltxt.Text = kuveyt_aciklama;
            lbltxt.ForeColor = Color.FromArgb(46, 204, 113);
            lbltxt.Font = txt;

            Font date = new Font("Rajdhani", 10);
            Label lbldate = new Label();
            //lbldate.Top = 80;
            lbldate.AutoSize = true;
            lbldate.AutoEllipsis = true;
            lbldate.UseCompatibleTextRendering = true;
            lbldate.Text = tarih;
            lbldate.ForeColor = Color.FromArgb(251, 197, 49);
            lbldate.Font = date;

            if (hesap_hareketi == "Hesaptan Para Çıkışı")
            {
                Label lbltutar = new Label();
                Font yy = new Font("Rajdhani", 14);
                //lbldate.Top = 80;
                lbltutar.AutoSize = true;
                lbltutar.AutoEllipsis = true;
                lbltutar.UseCompatibleTextRendering = true;
                lbltutar.Text = tutar+" ₺";
                lbltutar.ForeColor = Color.FromArgb(232, 65, 24);
                lbltutar.Font = yy;


                Label lblhesap_hareketi = new Label();
                //lbldate.Top = 80;
                lblhesap_hareketi.AutoSize = true;
                lblhesap_hareketi.AutoEllipsis = true;
                lblhesap_hareketi.UseCompatibleTextRendering = true;
                lblhesap_hareketi.Text = hesap_hareketi;
                lblhesap_hareketi.ForeColor = Color.FromArgb(232, 65, 24);
                lblhesap_hareketi.Font = date;

                flp.Controls.Add(lblheader);
                flp.Controls.Add(lbldate);
                flp.Controls.Add(lbltxt);
                flp.Controls.Add(lbltutar);
                flp.Controls.Add(lblhesap_hareketi);

                flowLayoutPanel6.Controls.Add(flp);
            }
            else if(hesap_hareketi== "Hesaba Para Girişi")
            {
                Label lbltutar = new Label();
                //lbldate.Top = 80;
                Font yy = new Font("Rajdhani", 14);
                lbltutar.AutoSize = true;
                lbltutar.AutoEllipsis = true;
                lbltutar.UseCompatibleTextRendering = true;
                lbltutar.Text = tutar+" ₺";
                lbltutar.ForeColor = Color.FromArgb(251, 197, 49);
                lbltutar.Font = yy;

                Label lblhesap_hareketi = new Label();
                //lbldate.Top = 80;
                lblhesap_hareketi.AutoSize = true;
                lblhesap_hareketi.AutoEllipsis = true;
                lblhesap_hareketi.UseCompatibleTextRendering = true;
                lblhesap_hareketi.Text = hesap_hareketi;
                lblhesap_hareketi.ForeColor = Color.FromArgb(251, 197, 49);
                lblhesap_hareketi.Font = date;

                flp.Controls.Add(lblheader);
                flp.Controls.Add(lbldate);
                flp.Controls.Add(lbltxt);
                flp.Controls.Add(lbltutar);
                flp.Controls.Add(lblhesap_hareketi);

                flowLayoutPanel6.Controls.Add(flp);
            }           
        }

        private void ListeleVakıftHesapHareketleri(string vakıf_aciklama, string tarih, string tutar, string hesap_hareketi)
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.BackColor = Color.FromArgb(39, 60, 117);
            flp.Size = new System.Drawing.Size(280, 130);
            flp.Padding = new Padding(10);
            flp.AutoScroll = true;
            flp.FlowDirection = FlowDirection.TopDown;

            Font header = new Font("Rajdhani", 14, FontStyle.Bold);
            Label lblheader = new Label();
            //lblheader.Top = 15;
            lblheader.AutoSize = true;
            lblheader.AutoEllipsis = true;
            lblheader.UseCompatibleTextRendering = true;
            lblheader.Text = "Vakıf Hesap Hareketi";
            lblheader.ForeColor = Color.White;
            lblheader.Font = header;

            Font txt = new Font("Rajdhani", 14);
            Label lbltxt = new Label();
            lbltxt.AutoSize = true;
            lbltxt.AutoEllipsis = true;
            lbltxt.UseCompatibleTextRendering = true;
            //lbltxt.Top = 40;
            lbltxt.Text = vakıf_aciklama;
            lbltxt.ForeColor = Color.FromArgb(46, 204, 113);
            lbltxt.Font = txt;

            Font date = new Font("Rajdhani", 10);
            Label lbldate = new Label();
            //lbldate.Top = 80;
            lbldate.AutoSize = true;
            lbldate.AutoEllipsis = true;
            lbldate.UseCompatibleTextRendering = true;
            lbldate.Text = tarih;
            lbldate.ForeColor = Color.FromArgb(251, 197, 49);
            lbldate.Font = date;

            if (hesap_hareketi == "Hesaptan Para Çıkışı")
            {
                Label lbltutar = new Label();
                Font yy = new Font("Rajdhani", 14);
                //lbldate.Top = 80;
                lbltutar.AutoSize = true;
                lbltutar.AutoEllipsis = true;
                lbltutar.UseCompatibleTextRendering = true;
                lbltutar.Text = tutar + " ₺";
                lbltutar.ForeColor = Color.FromArgb(232, 65, 24);
                lbltutar.Font = yy;

                Label lblhesap_hareketi = new Label();
                //lbldate.Top = 80;
                lblhesap_hareketi.AutoSize = true;
                lblhesap_hareketi.AutoEllipsis = true;
                lblhesap_hareketi.UseCompatibleTextRendering = true;
                lblhesap_hareketi.Text = hesap_hareketi;
                lblhesap_hareketi.ForeColor = Color.FromArgb(232, 65, 24);
                lblhesap_hareketi.Font = date;

                flp.Controls.Add(lblheader);
                flp.Controls.Add(lbldate);
                flp.Controls.Add(lbltxt);
                flp.Controls.Add(lbltutar);
                flp.Controls.Add(lblhesap_hareketi);

                flowLayoutPanel8.Controls.Add(flp);
            }
            else if (hesap_hareketi == "Hesaba Para Girişi")
            {
                Label lbltutar = new Label();
                //lbldate.Top = 80;
                Font yy = new Font("Rajdhani", 14);
                lbltutar.AutoSize = true;
                lbltutar.AutoEllipsis = true;
                lbltutar.UseCompatibleTextRendering = true;
                lbltutar.Text = tutar + " ₺";
                lbltutar.ForeColor = Color.FromArgb(251, 197, 49);
                lbltutar.Font = yy;

                Label lblhesap_hareketi = new Label();
                //lbldate.Top = 80;
                lblhesap_hareketi.AutoSize = true;
                lblhesap_hareketi.AutoEllipsis = true;
                lblhesap_hareketi.UseCompatibleTextRendering = true;
                lblhesap_hareketi.Text = hesap_hareketi;
                lblhesap_hareketi.ForeColor = Color.FromArgb(251, 197, 49);
                lblhesap_hareketi.Font = date;

                flp.Controls.Add(lblheader);
                flp.Controls.Add(lbldate);
                flp.Controls.Add(lbltxt);
                flp.Controls.Add(lbltutar);
                flp.Controls.Add(lblhesap_hareketi);

                flowLayoutPanel8.Controls.Add(flp);
            }
        }
        public void ZiraatAciklamaListeleSQL()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Ziraat_Bankası where kullanici_id=" + int.Parse(textBox4.Text);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = ds.Tables[0].Rows.Count-1; i >=0 ; i--)
            {
                string ziraat_aciklama = ds.Tables[0].Rows[i]["ziraat_aciklama"].ToString();
                string tarih = ds.Tables[0].Rows[i]["tarih"].ToString();
                string tutar = ds.Tables[0].Rows[i]["tutar"].ToString();
                string hesap_hareketi = ds.Tables[0].Rows[i]["hesap_hareketi"].ToString();

                ListeleZiraatHesapHareketleri(ziraat_aciklama, tarih,tutar,hesap_hareketi);
            }            
            baglanti.Close();
        }
        public void KuveytAciklamaListeleSQL()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Kuveyt_Bankası where kullanici_id=" + int.Parse(textBox4.Text);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = ds.Tables[0].Rows.Count-1; i >=0 ; i--)
            {
                string kuveyt_aciklama = ds.Tables[0].Rows[i]["kuveyt_aciklama"].ToString();
                string tarih = ds.Tables[0].Rows[i]["tarih"].ToString();
                string tutar = ds.Tables[0].Rows[i]["tutar"].ToString();
                string hesap_hareketi = ds.Tables[0].Rows[i]["hesap_hareketi"].ToString();

                ListeleKuveytHesapHareketleri(kuveyt_aciklama, tarih,tutar,hesap_hareketi);
            }
            baglanti.Close();
        }
        public void VakıfAciklamaListeleSQL()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Vakıf_Bankası where kullanici_id=" + int.Parse(textBox4.Text);
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = ds.Tables[0].Rows.Count-1; i >=0 ; i--)
            {
                string vakıf_aciklama = ds.Tables[0].Rows[i]["vakıf_aciklama"].ToString();
                string tarih = ds.Tables[0].Rows[i]["tarih"].ToString();
                string tutar = ds.Tables[0].Rows[i]["tutar"].ToString();
                string hesap_hareketi = ds.Tables[0].Rows[i]["hesap_hareketi"].ToString();

                ListeleVakıftHesapHareketleri(vakıf_aciklama, tarih, tutar, hesap_hareketi);                
            }
            baglanti.Close();
        }

        public void KuveytVadeTarihiYaz()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Kuveyt_Vade";
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                string kuveyt_vade = ds.Tables[0].Rows[i]["tarih"].ToString();
                textBox7.Text = kuveyt_vade;
            }
            baglanti.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string query;
            baglanti.Open();
            query = "insert into Mesaj (kullanici_id,mesaj,tarih,mesaj_baslik) values ('" + Convert.ToInt32(textBox4.Text) + "','" + richTextBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox1.Text.ToString() + "')";
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            textBox2.Text = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/" + "Saat: " + DateTime.Now.ToString("hh:mm:ss");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult ds;
            ds = MessageBox.Show("Oturumu kapatmak için onay verin", "Oturum İşlemi", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (ds == DialogResult.OK)
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Close();
            }
            else { }                     
        }
        public void HesapBilgileriListele() //Hesap Hareketleri Listele SQL FONKSİYONU
        {         
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from Gelir_Gider where kullanici_id=" + int.Parse(textBox4.Text);
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
                string ziraat_bakiye = ds.Tables[0].Rows[i]["ziraat_bakiye"].ToString();
                string vakıf_bakiye = ds.Tables[0].Rows[i]["vakıf_bakiye"].ToString();
                kuveyt_para = int.Parse(kuveyt_bakiye);
                ziraat_para = int.Parse(ziraat_bakiye);
                vakıf_para = int.Parse(vakıf_bakiye);
               
                label15.Text = kuveyt_para.ToString()+" ₺";
                label18.Text = ziraat_para.ToString()+" ₺";
                label20.Text = vakıf_para.ToString()+" ₺";
                label14.Text = (kuveyt_para + ziraat_para + vakıf_para).ToString() + " ₺";
                Popup_Notification("Hesap Ayrıntıları\n", "KuveytTürk Bakiyesi: " + kuveyt_bakiye + "\nZiraat Bakiyesi: " + ziraat_bakiye + "\nVakıf Bakiyesi: " + vakıf_bakiye + "\n\nToplam Bakiye: " + toplam_bakiye);
            }                      
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel7.Controls.Clear();
            flowLayoutPanel6.Controls.Clear();
            flowLayoutPanel8.Controls.Clear();
            ProjeListele();
            HesapBilgileriListele();
            ZiraatAciklamaListeleSQL();
            KuveytAciklamaListeleSQL();
            VakıfAciklamaListeleSQL();
            panel4.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if((textBox3.Text =="" || textBox3==null)&&(richTextBox2.Text=="" || richTextBox2.Text==null))
            {
                MessageBox.Show("Proje ismi ve açıklama boş olamaz");
            }
            else
            {
                string query;
                baglanti.Open();
                query = "insert into Projeler (kullanici_id,proje_ismi,proje_aciklamasi,tarih) values ('" + Convert.ToInt32(textBox4.Text) + "','" + textBox3.Text.ToString() + "','" + richTextBox2.Text.ToString() + "','" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/" + "Saat: " + DateTime.Now.ToString("hh:mm:ss") + "')";
                OleDbCommand com = new OleDbCommand();
                com.Connection = baglanti;
                com.CommandText = query;
                com.ExecuteNonQuery();
                baglanti.Close();
                textBox3.Clear();
                richTextBox2.Clear();
                ProjeListele();
            }                       
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }//BOŞ

        private void button8_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
        }

        private void button9_Click(object sender, EventArgs e)//kuveyt vade güncelleme (ok tuşu)
        {
            string query;
            baglanti.Open();
            query = "update Kuveyt_Vade set tarih='" + textBox7.Text + "' where Kimlik=1"; 
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }
        public void KuveytHesapHareketiSil()
        {
            string query;
            baglanti.Open();
            query = "delete from Kuveyt_Bankası where kullanici_id="+int.Parse(textBox4.Text);
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }
        public void VakıfHesapHareketiSil()
        {
            string query;
            baglanti.Open();
            query = "delete from Vakıf_Bankası where kullanici_id=" + int.Parse(textBox4.Text);
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }
        public void ZiraatHesapHareketiSil()
        {
            string query;
            baglanti.Open();
            query = "delete from Ziraat_Bankası where kullanici_id=" + int.Parse(textBox4.Text);
            OleDbCommand com = new OleDbCommand();
            com.Connection = baglanti;
            com.CommandText = query;
            com.ExecuteNonQuery();
            baglanti.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult ds;
            ds = MessageBox.Show("Hesap Hareketleri", "Hesap hareket dökümünü silmek üzeresin.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(ds==DialogResult.OK)
            {
                KuveytHesapHareketiSil();
                VakıfHesapHareketiSil();
                ZiraatHesapHareketiSil();

                flowLayoutPanel7.Controls.Clear();
                flowLayoutPanel6.Controls.Clear();
                flowLayoutPanel8.Controls.Clear();
                ProjeListele();
                HesapBilgileriListele();
                ZiraatAciklamaListeleSQL();
                KuveytAciklamaListeleSQL();
                VakıfAciklamaListeleSQL();
            }                       
        }
     
        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            if (textBox5.Text == "" || textBox5.Text.Length<=2||textBox6.Text=="" || textBox6.Text.Length<=2)
            {               
                MessageBox.Show("UYARI", "Değerlerin uzunluğu 2 den fazla ve değerler boş olmamalıdır.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button10.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                pictureBox4.Visible = true;
            }            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tmr11++;
            if(tmr11%1==0)
            {
                if(tmr11==20)
                {                   
                    pictureBox4.Visible = false;
                    timer2.Enabled = true;
                    timer2.Start();
                    timer1.Enabled = false;
                    timer1.Stop();
                    tmr11 = 0;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox5.Visible = true;
            label25.Visible = true;
            tmr22++;
            if(tmr22%1==0)
            {
                if(tmr22==20)
                {
                    pictureBox4.Visible = false;
                    
                    timer3.Enabled = true;
                    timer3.Start();
                    timer2.Enabled = false;
                    timer2.Stop();
                    tmr22 = 0;
                    
                    string query;
                    baglanti.Open();
                    query = "update DbUserlar set kullanici_adi='" + textBox5.Text + "' , sifre='" + textBox6.Text + "'where id=" + int.Parse(textBox4.Text);
                    OleDbCommand com = new OleDbCommand();
                    com.Connection = baglanti;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                    baglanti.Close();
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            tmr33++;
            if(tmr33%1==0)
            {
                if(tmr33==20)
                {
                    pictureBox5.Visible = false;
                    timer3.Enabled = false;
                    timer3.Stop();
                    tmr33 = 0;
                    button10.Enabled = true;
                    label25.Visible = false;
                }
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label27_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

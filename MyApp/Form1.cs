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
    public partial class Form1 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=MyAppData.mdb");
        public bool control = false;
        public string k_id;
        public string k_ad;
        public string k_sifre;
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
     
        public Form1()
        {
            InitializeComponent();
        }
        private void Kullanıcı_Kontrol(string ad,string sifre)
        {
            ArrayList Kullanıcı_Adları = new ArrayList();
            ArrayList Kullanıcı_Idleri = new ArrayList();
            int sayac = 0;
            int stopsayac = 0;
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select id,kullanici_adi,sifre from DbUserlar";
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string x = ds.Tables[0].Rows[i]["kullanici_adi"].ToString();
                string y = ds.Tables[0].Rows[i]["sifre"].ToString();
                string z = ds.Tables[0].Rows[i]["id"].ToString();
                Kullanıcı_Adları.Add(x+y);
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string z = ds.Tables[0].Rows[i]["id"].ToString();
                Kullanıcı_Idleri.Add(z);
            }
            foreach (string isim in Kullanıcı_Adları)
            {
                if(isim == ad+sifre)
                {
                    control = true;
                    stopsayac = sayac;
                    GirisBilgilerics nesne = new GirisBilgilerics();
                    /*k_id = 
                    k_ad = ad;
                    k_sifre = sifre;*/
                    form2.textBox4.Text= Kullanıcı_Idleri[stopsayac].ToString();
                    form2.textBox5.Text = ad.ToString();
                    form2.textBox6.Text = sifre.ToString();
                }
                else
                {
                    sayac++;
                }              
            }           
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // textBox1.Text = "murat";
            //textBox2.Text = "123";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanıcı_Kontrol(textBox1.Text,textBox2.Text);
            if(control==true)
            {
                this.Hide();
                textBox1.Clear();
                textBox2.Clear();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("hata");
            }
        }

        private void Form1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text=="murat" && textBox2.Text=="123")
            {
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hata");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            form3.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

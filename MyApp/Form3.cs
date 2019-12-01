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
namespace MyApp
{
    
    public partial class Form3 : Form
    {
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=MyAppData.mdb");
        public bool control=false;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        public void ExitApp(int deger)
        {
            if(deger==1)
            {
                this.Close();
            }
            else{}
            
        }
        //Kayıtlı kullanıcı kontrolü
        private void Kullanıcı_Kontrol(string ad)
        {
            ArrayList Kullanıcı_Adları = new ArrayList();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            string query = "select * from DbUserlar";
            komut.CommandText = query;
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Merge(dt);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Kullanıcı_Adları.Add(ds.Tables[0].Rows[i]["kullanici_adi"]);
            }
            foreach (string isim in Kullanıcı_Adları)
            {
                if (isim == ad)
                {
                    control = true;
                }
                else
                {
                    control = false;
                }
            }
            baglanti.Close();
           
        }
        //giriş penceresine giden link
        private void label1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanıcı_Kontrol(textBox1.Text);
            if (control==false)
            {
                string query;
                baglanti.Open();
                query = "insert into DbUserlar (kullanici_adi,sifre) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "')";
                OleDbCommand com = new OleDbCommand();
                com.Connection = baglanti;
                com.CommandText = query;
                com.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Successful");
            }
            if(control==true)
            {
                MessageBox.Show("UnSuccessfull");
            }
            
        }
    }
}

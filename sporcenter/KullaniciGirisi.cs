using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;



namespace sporcenter
{
    public partial class KullaniciGirisi : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
       
        public KullaniciGirisi()
        {
            InitializeComponent();
        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            

            string user = guna2TextBox1.Text;
            string password= guna2TextBox2.Text;
            con=new SqlConnection("Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog=dbspor;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from kayitbilgi where adsoyad ='" + guna2TextBox1.Text +
                "'And sifrebelirle='" + guna2TextBox2.Text + "'";
            dr = com.ExecuteReader();
            if(dr.Read())
            {
                
                kullanicianasayfa gecis= new kullanicianasayfa(user);
                gecis.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("hatalı kullanıcı adı veya şifre");
            }
            con.Close();
           
        }
       

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kaydol gecis = new Kaydol();
            gecis.Show();
            this.Hide();
        }

        private void KullaniciGirisi_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form1 gecis = new Form1();
            gecis.Show();
            this.Hide();
        }
    }
    }


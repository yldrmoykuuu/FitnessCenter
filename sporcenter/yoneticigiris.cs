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

namespace sporcenter
{
    public partial class yoneticigiris : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;

        public yoneticigiris()
        {
            InitializeComponent();
        }
        
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            string user = guna2TextBox1.Text;
            string password = guna2TextBox2.Text;
            con = new SqlConnection("Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog=dbspor;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from yonetici_bilgi where kullanici_adi ='" + guna2TextBox1.Text +
                "'And sifre='" + guna2TextBox2.Text + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                genelislem gecis = new genelislem();
                gecis.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("hatalı kullanıcı adı veya şifre");
            }
            con.Close();

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void yoneticigiris_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form1 gecis=new Form1();
            gecis.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

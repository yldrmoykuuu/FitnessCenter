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
    public partial class Kaydol : Form
    {
        public Kaydol()
        {
            InitializeComponent();
        }
        static string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
        SqlConnection connect = new SqlConnection(constring);


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Kaydol_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        { try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                string kayitbilgi = "insert into kayitbilgi (adsoyad,dogumtar,cinsiyet,telno,sifrebelirle) values (@adsoyad,@dogumtar,@cinsiyet,@telno,@sifrebelirle)";
                SqlCommand komut= new SqlCommand(kayitbilgi,connect); 
                komut.Parameters.AddWithValue("@adsoyad",guna2TextBox1.Text);
                komut.Parameters.AddWithValue("@dogumtar", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@cinsiyet", guna2ComboBox1.Text);
                komut.Parameters.AddWithValue("@telno", guna2TextBox2.Text);
                komut.Parameters.AddWithValue("@sifrebelirle", guna2TextBox3.Text);
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt Eklendi");
               



            }
            catch (Exception ex) 
            {
                MessageBox.Show("Hata meydana geldi" + ex.Message);




            }   
            KullaniciGirisi gecis=new KullaniciGirisi();
            gecis.Show();
            this.Hide();

          

           

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            KullaniciGirisi gecis = new KullaniciGirisi();
            gecis.Show();
            this.Hide();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

namespace sporcenter
{
    public partial class silinenuye : Form
    {
        public silinenuye()
        {
            InitializeComponent();
        }

        static string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
        SqlConnection connect = new SqlConnection(constring);
        private void silinenUyeler()
        {
            connect.Open();
            string query = " select * from silinenkayit";
            SqlDataAdapter sda = new SqlDataAdapter(query,connect);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            connect.Close();
        }
        private void FilterKayitbilgiHarfegöreara(string filterValue) // Küçük harfle parametre adı düzeltilmiş
        {
            // SqlBaglanti sınıfı üzerinden bağlantıyı alıyoruz.
            //   SqlConnection connection = connection.baglanti();
            string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
            SqlConnection connection = new SqlConnection(constring);

            using (SqlCommand command = new SqlCommand("FilterKayitbilgiHarfegöreara", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Stored procedure parametresini ekliyoruz.
                command.Parameters.AddWithValue("@FilterValue", filterValue); // Doğru parametre adı eşleşiyor

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                try
                {
                    adapter.Fill(dt); // Veriyi DataTable'a dolduruyoruz.
                    guna2DataGridView1.DataSource = dt; // DataGridView'e bağlıyoruz.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    connection.Close(); // Bağlantıyı kapatıyoruz.

                }


            }
        }

        

        private void silinenuye_Load(object sender, EventArgs e)
        {
            silinenUyeler();
        }

        public void listele()
        {

            string getir = "select* from silinenkayit";
            SqlCommand cmd = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            connect.Close();

        }
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            listele();
            string filterValue = guna2TextBox1.Text.Trim(); // Metin kutusundaki değeri alıyoruz.

            // Eğer metin kutusu boşsa kullanıcıyı uyarabiliriz
            if (string.IsNullOrEmpty(filterValue))
            {
                MessageBox.Show("Lütfen bir  giriniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FilterKayitbilgiHarfegöreara(filterValue); // Arama işlemini başlatıyoruz.  

            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Yanasayfa gecis= new Yanasayfa();
            gecis.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

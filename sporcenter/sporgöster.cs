using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace sporcenter
{
    public partial class sporgöster : Form
    {
        public sporgöster()
        {
            InitializeComponent();
        }
        static string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog=dbspor;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        // Veritabanından verileri al ve DataGridView'e yükle


        private void FilterTakipSporHarfegöreara(string filterValue) // Küçük harfle parametre adı düzeltilmiş
        {
            // SqlBaglanti sınıfı üzerinden bağlantıyı alıyoruz.
            //   SqlConnection connection = connection.baglanti();
            string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
            SqlConnection connection = new SqlConnection(constring);

            using (SqlCommand command = new SqlCommand("FilterTakipSporHarfegöreara", connection))
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


        private void sporgöster_Load(object sender, EventArgs e)
        {
            // yapılanspor();
          

        }
        public void LoadData(DataTable data)
        {
           guna2DataGridView1.DataSource = data;  // DataGridView'e veri aktarımı
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            genelislem gecis = new genelislem();
            gecis.Show();
            this.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
            string filterValue = guna2TextBox1.Text.Trim(); // Metin kutusundaki değeri alıyoruz.

            // Eğer metin kutusu boşsa kullanıcıyı uyarabiliriz
            if (string.IsNullOrEmpty(filterValue))
            {
                MessageBox.Show("Lütfen bir harf giriniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FilterTakipSporHarfegöreara(filterValue); // Arama işlemini başlatıyoruz.  

        }
    }
}



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sporcenter
{
    public partial class genelislem : Form
    {
        public genelislem()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog=dbspor;Integrated Security=True";

        private DataTable data

            (string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);  // Veriyi çek ve DataTable'a yükle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            return dataTable;
        }
          

            private void pictureBox1_Click(object sender, EventArgs e)
        {
            Yanasayfa gecis = new Yanasayfa();
            gecis.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pdfcevir gecis = new pdfcevir();
            gecis.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL bağlantı dizesi
                //  string connectionString = "Server=DESKTOP-KBTUU42\\SQLEXPRESS;Database=master;Integrated Security=True;";  // master veritabanını kullanıyoruz
                string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog=dbspor;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);

                // Kullanıcıya yedek dosyasını seçtirmek için OpenFileDialog kullanıyoruz
                OpenFileDialog openFileDialog = new OpenFileDialog();
                // openFileDialog.Filter = "Yedek Dosyaları (.bak)|.bak"; // sadece .bak dosyalarını gösterir.
                openFileDialog.Filter = "Yedek Dosyaları (*.bak)|*.bak|Tüm Dosyalar (*.*)|*.*";
                openFileDialog.Title = "Yedek Dosyasını Seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string backupFile = openFileDialog.FileName;  // Kullanıcının seçtiği dosya yolu

                    // Geri yükleme komutu
                    string restoreQuery = $"RESTORE DATABASE [dbspor] FROM DISK = '{backupFile}' WITH REPLACE;";

                    // SQL bağlantısı ve komut çalıştırır.
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(restoreQuery, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Başarılı mesajı
                    MessageBox.Show($"Veritabanı başarıyla geri yüklendi.\nYedek Dosyası: {backupFile}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                // Hata mesajı
                //MessageBox.Show($"Yedekten dönme sırasında bir hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            try
            {
                // Veritabanı bağlantı bilgileri
                //  string connectionString = "Server=SERVER_ADI;Database=VeritabaniAdi;User Id=KULLANICI_ADI;Password=ŞİFRE;";
                string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog=dbspor;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                string backupDirectory = @"C:\Yedekler";

                // Yedek dosyasının kaydedileceği yol
                string backupFileName = $"VeritabaniYedek_{DateTime.Now:yyyyMMddHHmmss}.bak";
                string backupFilePath = Path.Combine(backupDirectory, backupFileName);

                // Yedekleme dizini oluşturulmamışsa oluştur
                if (!Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                // SQL yedekleme komutunu oluştur
                //  string backupQuery = $"BACKUP DATABASE dbspor TO DISK = '{backupFilePath}'";
                string backupQuery = $"BACKUP DATABASE [dbspor] TO DISK = '{backupFileName}'";

                // SQL bağlantısını aç ve yedekleme komutunu çalıştır
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show($"Veritabanı başarıyla yedeklendi!\nYedek dosyası: {backupFilePath}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

            // SQL Sorgusu: Kayitbilgi ve Takipspor tablosunu birleştir
            string query = @"
                SELECT k.adsoyad, k.telno, t.sportur, t.zamanlama
                FROM kayitbilgi k
                JOIN takipspor t ON k.adsoyad = t.adsoyad
            ";

            // Veriyi DataTable'a al
            DataTable dataTable = data( query);

            // Veriyi Form2'ye gönder
            sporgöster form2 = new sporgöster();
            form2.LoadData(dataTable);  // DataTable'ı Form2'ye aktarıyoruz
            form2.Show();




        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            yoneticigiris gecis=new yoneticigiris();
            gecis.Show();
            this.Hide();
        }

        private void genelislem_Load(object sender, EventArgs e)
        {

        }
    }
    }


    



   





   
        
    


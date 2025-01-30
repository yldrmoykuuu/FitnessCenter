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
using sporcenter.dbsporDataSet1TableAdapters;
using Guna.UI2.WinForms;
using DataTable = System.Data.DataTable;
using Application = System.Windows.Forms.Application;
using Excel = Microsoft.Office.Interop.Excel;
using excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;




namespace sporcenter
{
    public partial class Yanasayfa : Form
    {
        public Yanasayfa()
        {
            InitializeComponent();
        }

        static string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
        SqlConnection connection = new SqlConnection(constring);
        // SQL sorgusu
        string query = @"
        SELECT 
            k.adsoyad, 
            k.cinsiyet, 
            k.dogumtar, 
            k.telno, 
            s.zamanlama, 
            s.sportur
        FROM 
            kayitbilgi k
        INNER JOIN 
            takipspor s ON k.adsoyad = s.adsoyad
    ";
        private void FilterKayitbilgiAdsoyad(string filterValue) // Küçük harfle parametre adı düzeltilmiş
        {
            // SqlBaglanti sınıfı üzerinden bağlantıyı alıyoruz.
            //   SqlConnection connection = connection.baglanti();
            string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
            SqlConnection connection = new SqlConnection(constring);

            using (SqlCommand command = new SqlCommand("FilterKayitbilgiAdsoyad", connection))
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
        public void kayitlarigetir()
        {
            string getir = "select*from kayitbilgi order by adsoyad";
            SqlCommand cmd = new SqlCommand(getir, connection);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            connection.Close();
        }

        public void verisil(string adsoyad)
        {
            string sil = "delete from kayitbilgi where adsoyad=@adsoyad";
            SqlCommand cmd = new SqlCommand(sil, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
            cmd.ExecuteNonQuery();
            connection.Close();

        }
        public void listele()
        {

            string getir = "select* from kayitbilgi";
            SqlCommand cmd = new SqlCommand(getir, connection);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            connection.Close();

        }


        private void Yanasayfa_Load(object sender, EventArgs e)
        {
            listele();
        }
       

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            silinenuye gecis = new silinenuye();
            gecis.Show();
            this.Hide();
        }
        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void guna2GradientButton3_Click_1(object sender, EventArgs e)
        {
            listele();
            string filterValue = guna2TextBox3.Text.Trim(); // Metin kutusundaki değeri alıyoruz.

            // Eğer metin kutusu boşsa kullanıcıyı uyarabiliriz
            if (string.IsNullOrEmpty(filterValue))
            {
                MessageBox.Show("Lütfen bir arama kriteri giriniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FilterKayitbilgiAdsoyad(filterValue); // Arama işlemini başlatıyoruz.  
            
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constring))
            {
                try
                {
                    conn.Open();

                    // Silinecek kaydın adsoyadını alıyoruz
                    string adsoyad = guna2TextBox3.Text;

                    // SilinenUyeler tablosunda kayıt olup olmadığını kontrol et
                    string kontrolQuery = "SELECT COUNT(*) FROM silinenkayit WHERE id IN (SELECT id FROM kayitbilgi WHERE adsoyad = @adsoyad)";
                    SqlCommand kontrolCommand = new SqlCommand(kontrolQuery, conn);
                    kontrolCommand.Parameters.AddWithValue("@adsoyad", adsoyad);
                    int kayitVarMi = (int)kontrolCommand.ExecuteScalar();

                    if (kayitVarMi == 0) // Eğer kayıt yoksa silme işlemi yapılabilir
                    {
                        // Silinen kaydı SilinenUyeler tablosuna ekle
                        string insertQuery = @"
                INSERT INTO silinenkayit (id, adsoyad, cinsiyet, dogumtar, silinmetarihi)
                SELECT id, adsoyad, cinsiyet, dogumtar, GETDATE()
                FROM kayitbilgi
                WHERE adsoyad = @adsoyad";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                        insertCommand.Parameters.AddWithValue("@adsoyad", adsoyad);
                        insertCommand.ExecuteNonQuery();

                        // Ardından kayitbilgi tablosundan sil
                        string deleteQuery = "DELETE FROM kayitbilgi WHERE adsoyad = @adsoyad";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                        deleteCommand.Parameters.AddWithValue("@adsoyad", adsoyad);
                        deleteCommand.ExecuteNonQuery();

                        MessageBox.Show("Kayıt başarıyla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Bu kayıt zaten SilinenUyeler tablosunda mevcut.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            /*using (SqlConnection conn = new SqlConnection(constring))
            {
                string adsoyad = guna2TextBox3.Text; // TextBox'tan alınan isim
                try
                {
                    conn.Open();
                    // Silinecek üyenin bilgilerini SilinenUyeler tablosuna ekleyelim
                    string insertQuery = @"
                    INSERT INTO silinenkayit (id, adsoyad, cinsiyet, dogumtar, silinmetarihi)
                    SELECT id, adsoyad, cinsiyet, dogumtar, GETDATE()
                    FROM kayitbilgi
                    WHERE adsoyad = @adsoyad ";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                    insertCommand.Parameters.AddWithValue("@adsoyad", adsoyad);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Silinen kaydın SilinenUyeler tablosuna eklendiğini bildir
                        MessageBox.Show("Kayıt SilinenUyeler tablosuna aktarıldı.");

                        // Ardından KayıtBilgi tablosundan silme işlemi
                        /*string deleteQuery = "DELETE FROM kayıtBilgi WHERE AdSoyad = @AdSoyad AND Cinsiyet = @Cinsiyet";

                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                        deleteCommand.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        deleteCommand.Parameters.AddWithValue("@Cinsiyet", cinsiyet);

                        deleteCommand.ExecuteNonQuery(); // Kayıt silme işlemi

                        MessageBox.Show("Kayıt KayıtBilgi tablosundan silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Silinecek kayıt bulunamadı.");
                    }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            foreach (DataGridViewRow drow in guna2DataGridView1.SelectedRows)
            {
                string adsoyad = guna2TextBox3.Text;
                verisil(adsoyad);
                kayitlarigetir();
            }*/
        }
        int i = 0;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // TextBox'lardan alınan veriler
            string adSoyad = guna2TextBox3.Text;
            string yeniTelno = guna2TextBox2.Text;
            string cinsiyet = guna2ComboBox1.Text;
            string dogumtar = maskedTextBox1.Text;
            string sifrebelirle = guna2TextBox4.Text;

            // Veritabanı bağlantı dizesi (kendi veritabanınıza göre düzenleyin)
            string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";

            // SQL sorgusu: adsoyad'a göre telno'yu güncelleme
            string query = "UPDATE kayitbilgi SET telno = @telno,cinsiyet = @cinsiyet, dogumtar = @dogumtar, sifrebelirle = @sifrebelirle  WHERE adsoyad = @adsoyad";


            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    // SQL komutunu oluşturma
                    SqlCommand cmd = new SqlCommand(query, connection);
                    // Parametreleri ekliyoruz
                    cmd.Parameters.AddWithValue("@telno", yeniTelno);
                    cmd.Parameters.AddWithValue("@adsoyad", adSoyad);
                    cmd.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                    cmd.Parameters.AddWithValue("@dogumtar", dogumtar);
                    cmd.Parameters.AddWithValue("@sifrebelirle", sifrebelirle);
                    // Bağlantıyı açıyoruz
                    connection.Open();

                    // SQL komutunu çalıştırıyoruz
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Eğer etkilenen satır sayısı 0 ise, adsoyad bulunamadı demek
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Bilgiler güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Ad soyad bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumu
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
            listele();

            
        }

        
        private void guna2DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            guna2TextBox3.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.Rows[i].Cells[4].Value.ToString();
            guna2ComboBox1.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
            maskedTextBox1.Text = guna2DataGridView1.Rows[i].Cells[2].Value.ToString();



        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    string kayit = "insert into kayitbilgi (adsoyad,telno,cinsiyet,dogumtar,sifrebelirle)values(@adsoyad,@telno,@cinsiyet,@dogumtar,@sifrebelirle)";
                    SqlCommand cmd = new SqlCommand(kayit, connection);
                    cmd.Parameters.AddWithValue("@adsoyad", guna2TextBox3.Text);
                    cmd.Parameters.AddWithValue("@telno", guna2TextBox2.Text);
                    cmd.Parameters.AddWithValue("@cinsiyet", guna2ComboBox1.Text);
                    cmd.Parameters.AddWithValue("@dogumtar", maskedTextBox1.Text);
                    cmd.Parameters.AddWithValue("@sifrebelirle", guna2TextBox4.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("kayıt başarılı");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BİR HATA VAR" + ex.Message);
            }
            listele();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            listele();
        }





        private void guna2GradientButton6_Click_1(object sender, EventArgs e)
        {
            // Excel dosyasını seçmek için OpenFileDialog kullanımı
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Excel Dosyasını Seçin"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string excelFilePath = openFileDialog.FileName;

                try
                {
                    // Excel'den veriyi oku ve DataGridView'e yükle
                    DataTable dataTable = ReadExcelFile(excelFilePath);
                    guna2DataGridView1.DataSource = dataTable;

                    // Veriyi SQL veritabanına aktar
                    InsertDataIntoSQL(dataTable);

                    MessageBox.Show("Veriler başarıyla aktarıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
            private DataTable ReadExcelFile(string filePath)
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);
                Excel.Worksheet worksheet = workbook.Sheets[1]; // İlk sayfa
                Excel.Range range = worksheet.UsedRange;

                DataTable dataTable = new DataTable();

                // Excel başlıklarını DataTable'a ekle
                for (int col = 1; col <= range.Columns.Count; col++)
                {
                    dataTable.Columns.Add(range.Cells[1, col].Value2.ToString());
                }

                // Excel verilerini DataTable'a ekle
                for (int row = 2; row <= range.Rows.Count; row++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int col = 1; col <= range.Columns.Count; col++)
                    {
                        dataRow[col - 1] = range.Cells[row, col].Value2;
                    }
                    dataTable.Rows.Add(dataRow);
                }

                // Kaynakları serbest bırak
                workbook.Close(false);
                excelApp.Quit();

                return dataTable;
            }
        private void InsertDataIntoSQL(DataTable dataTable)
        {
            string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataRow row in dataTable.Rows)
                {
                    string query = "INSERT INTO kayitbilgi ( adsoyad, dogumtar, cinsiyet,telno,sifrebelirle) VALUES ( @adsoyad, @dogumtar, @cinsiyet,@telno,@sifrebelirle)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                       
                        cmd.Parameters.AddWithValue("@adsoyad", row["adsoyad"]);
                        cmd.Parameters.AddWithValue("@dogumtar", row["dogumtar"]);
                        cmd.Parameters.AddWithValue("@cinsiyet", row["cinsiyet"]);
                        cmd.Parameters.AddWithValue("@telno", row["telno"]);
                        cmd.Parameters.AddWithValue("@sifrebelirle", row["sifrebelirle"]);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
            if (openFileDialog .ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog .FileName;
                DataTable datatable=new DataTable();    
                using (var workbook=new ClosedXML.Excel.XLWorkbook(filepath))
                {
                    var worksheet=workbook.Worksheet(1);
                    bool firstrow = true;
                    foreach (var row in worksheet.RowsUsed())
                    {
                        if (firstrow)
                        {
                            foreach(var cell in row.Cells())
                            {
                                datatable.Columns.Add(cell.Value.ToString());
                            }
                            firstrow =false;
                        }
                        else
                        {
                            datatable.Rows.Add(row.Cells().Select(c => c.Value.ToString()).ToArray());
                        }
                    }
                }
                guna2DataGridView1.DataSource = datatable;
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        foreach (DataRow row in datatable.Rows)
                        {
                            cmd.CommandText = "INSERT INTO kayitbilgi (adsoyad,dogumtar,cinsiyet,telno,sifrebelirle) " +
                                "values (@adsoyad,@dogumtar,@cinsiyet,@telno,@sifrebelirle) ";
                            cmd.Parameters.AddWithValue("@adsoyad", row["adsoyad"]);
                            cmd.Parameters.AddWithValue("@dogumtar", row["dogumtar"]);
                            cmd.Parameters.AddWithValue("@cinsiyet", row["cinsiyet"]);
                            cmd.Parameters.AddWithValue("@telno", row["telno"]);
                            cmd.Parameters.AddWithValue("@sifrebelirle", row["sifrebelirle"]);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();  
                        }
                    }
                }
                MessageBox.Show("Başarılı","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {

            try
            {
                Excel.Application app = new Excel.Application();
                app.Visible = true;
                Excel.Workbook kitap = app.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet sayfa = (Excel.Worksheet)kitap.Sheets[1];
                for (int i = 0; i < guna2DataGridView1.Columns.Count; i++)
                {
                    Excel.Range alan = (Excel.Range)sayfa.Cells[1, i + 1];
                    alan.Value = guna2DataGridView1.Columns[i].HeaderText;


                }
                for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < guna2DataGridView1.Columns.Count; j++)
                    {
                        Excel.Range alan = (Excel.Range)sayfa.Cells[i + 2, j + 1];
                        alan.Value = guna2DataGridView1[j, i].Value?.ToString();
                    }
                }
                MessageBox.Show("Aktarıldı", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("veriler aktarılmadı" + ex.Message, "hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            genelislem gecis=new genelislem();
            gecis.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan telefon numarası
            string phoneNumber = guna2TextBox2.Text;

            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";

            // SQL bağlantısı
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Stored Procedure çağırma
                    using (SqlCommand command = new SqlCommand("SearchByPhone", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parametre ekleme
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        // Sonuçları bir DataTable içine çekme
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable resultsTable = new DataTable();
                        adapter.Fill(resultsTable);

                        // Sonuçları bir DataGridView'de gösterme
                        guna2DataGridView1.DataSource = resultsTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan cinsiyet bilgisi
            string gender = guna2ComboBox1.SelectedItem?.ToString(); // Cinsiyet ComboBox'tan alınır

            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Lütfen bir cinsiyet seçin!");
                return;
            }

            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";

            // SQL bağlantısı
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Stored Procedure çağırma
                    using (SqlCommand command = new SqlCommand("SearchByGender", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parametre ekleme
                        command.Parameters.AddWithValue("@Gender", gender);

                        // Sonuçları bir DataTable içine çekme
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable resultsTable = new DataTable();
                        adapter.Fill(resultsTable);

                        // Sonuçları bir DataGridView'de gösterme
                        guna2DataGridView1.DataSource = resultsTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }
        }
    }
    }



   
    
    

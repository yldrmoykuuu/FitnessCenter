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

using System.Security.Cryptography;
using System.Linq.Expressions;
using Guna.UI2.WinForms;

namespace sporcenter
{
    public partial class kullanicianasayfa : Form
    {
        //SqlConnection connection =new SqlConnection("Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True");

        public kullanicianasayfa(string user)
        {

            InitializeComponent();
            guna2TextBox3.Text = user;

        }
        static string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
        SqlConnection connection = new SqlConnection(constring);


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


       
        private void kullanicianasayfa_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        private void LoadData()
        {
            try
            {
                connection.Open();
                string query = @"select k.id,k.adsoyad,k.telno,k.cinsiyet,k.dogumtar,t.sportur,t.zamanlama from kayitbilgi k left join takipspor t on k.id=t.id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("hata" + ex.Message);
            }
            finally { connection.Close(); }
            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    string query = "SELECT id, adsoyad, cinsiyet, telno, dogumtar, silinmetarihi FROM silinenkayit";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // DataGridView'e verileri yükle
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

            
            DialogResult dialogResult = MessageBox.Show(
                 "Hesabınızı silmek istediğinize emin misiniz? Bu işlem geri alınamaz.",
                 "Hesap Silme Onayı",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
             );


            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(constring))
                    {
                        connect.Open();
                        string query = "DELETE FROM kayitbilgi WHERE adsoyad = @adsoyad";


                        using (SqlCommand komut = new SqlCommand(query, connect))
                        {

                            komut.Parameters.AddWithValue("@adsoyad", guna2TextBox3.Text);


                            int sonuc = komut.ExecuteNonQuery();

                            if (sonuc > 0)
                            {
                                MessageBox.Show("Hesabınız başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                Form1 fr = new Form1();
                                fr.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Hesap silme başarısız oldu. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }


    

    private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            
            string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
            string sportur=guna2ComboBox1.SelectedItem.ToString();
            string telno=guna2TextBox2.Text.ToString(); 
            DateTime zamanlama = guna2DateTimePicker1.Value;
            string adsoyad=guna2TextBox3.Text;
            using (SqlConnection connection=new SqlConnection(constring))
            {
                try
                {
                    string query = "insert into takipspor(adsoyad,telno,sportur,zamanlama)values(@adsoyad,@telno,@sportur,@zamanlama)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
                    cmd.Parameters.AddWithValue("@telno", telno);
                    cmd.Parameters.AddWithValue("@sportur", sportur);
                    cmd.Parameters.AddWithValue("@zamanlama", zamanlama);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("kaydedildi");
                    guna2TextBox3.Clear();
                    guna2ComboBox1.SelectedIndex = 0;
                    guna2DateTimePicker1.Value = DateTime.Now;
             
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("hata" + ex.Message);
                }
            }
            
            
        }
            private void guna2TextBox3_TextChanged(object sender, EventArgs e)
            {
                string adsoyad = guna2TextBox3.Text;
                string query = "select id,telno from kayitbilgi where adsoyad=@adsoyad";
                using (SqlConnection connection = new SqlConnection(constring))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@adsoyad", adsoyad);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            guna2TextBox1.Text = reader["id"].ToString();
                            guna2TextBox2.Text = reader["telno"].ToString();

                        }
                        else
                        {
                            
                           
                           

                        }
                        reader.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("hata meydana geldi" + ex.Message);
                    }
                   
                    

                }

            }
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // TextBox'lardan alınan veriler
            string adSoyad = guna2TextBox3.Text;
            string yeniTelno = guna2TextBox2.Text;

            // Veritabanı bağlantı dizesi (kendi veritabanınıza göre düzenleyin)
            string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";

            // SQL sorgusu: adsoyad'a göre telno'yu güncelleme
            string query = "UPDATE kayitbilgi SET telno = @telno WHERE adsoyad = @adsoyad";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    // SQL komutunu oluşturma
                    SqlCommand cmd = new SqlCommand(query, connection);
                    // Parametreleri ekliyoruz
                    cmd.Parameters.AddWithValue("@telno", yeniTelno);
                    cmd.Parameters.AddWithValue("@adsoyad", adSoyad);

                    // Bağlantıyı açıyoruz
                    connection.Open();

                    // SQL komutunu çalıştırıyoruz
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Eğer etkilenen satır sayısı 0 ise, adsoyad bulunamadı demek
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Telefon numarası başarıyla güncellendi.");
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
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            KullaniciGirisi gecis=new KullaniciGirisi();
            gecis.Show();
            this.Hide();

        }
    }
    }
    




   
    

  
      






    


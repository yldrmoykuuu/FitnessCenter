using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Guna.UI2.WinForms;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace sporcenter
{
    public partial class pdfcevir : Form
    {
        public pdfcevir()
        {
            InitializeComponent();
        }
        static string constring = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";
        SqlConnection connection = new SqlConnection(constring);

        private void LoadData()
        {
            // SQL Server bağlantı dizesi
            string connectionString = "Data Source=LAPTOP-E07ISON9\\SQLEXPRESS;Initial Catalog = dbspor; Integrated Security = True";

            // SQL sorgusu
            string query = "SELECT * FROM kayitbilgi";

            // SqlConnection nesnesini oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL sorgusunu çalıştırmak için SqlDataAdapter kullan
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                // DataTable oluştur
                DataTable dataTable = new DataTable();

                try
                {
                    // Veriyi DataTable'a doldur
                    connection.Open();
                    adapter.Fill(dataTable);

                    // DataGridView'e DataTable'ı ata
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    // Hata mesajı göster
                    MessageBox.Show($"Hata: {ex.Message}", "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close(); // Bağlantıyı kapat
                }
            }
        }
        private void pdfcevir_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "PDF Dosyasını Kaydet",
                FileName = "UyelerRaporu.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    ExportDataGridViewToPdf(dataGridView1, filePath);
                    MessageBox.Show("PDF başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ExportDataGridViewToPdf(DataGridView dataGridView, string filePath)
        {
            
            // DataGridView sütun sayısını kontrol et
            if (dataGridView.ColumnCount == 0 || dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("DataGridView'de veri bulunmuyor. Lütfen tabloyu doldurduğunuzdan emin olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Fonksiyonu sonlandır
            }

            // PDF belgesi oluştur
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Başlık ekle
            document.Add(new Paragraph("KAYITLI ÜYELER")
            {
                Alignment = Element.ALIGN_CENTER,
                Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18f)
            });

            document.Add(new Chunk("\n")); // Boşluk için

            // Tablo oluştur ve sütun başlıklarını ekle
            PdfPTable table = new PdfPTable(dataGridView.ColumnCount)
            {
                WidthPercentage = 100
            };

            // Sütun başlıklarını ekle
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText))
                {
                    BackgroundColor = new BaseColor(240, 240, 240),
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                table.AddCell(headerCell);
            }

            // Satır verilerini ekle
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow) // Yeni satır eklenmemişse
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellText = cell.Value?.ToString() ?? string.Empty;
                        PdfPCell dataCell = new PdfPCell(new Phrase(cellText))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(dataCell);
                    }
                }
            }

            // Tabloyu PDF'e ekle
            document.Add(table);

            // PDF dosyasını kapat
            document.Close();
        
    }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            genelislem gecis=new genelislem();
            gecis.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}




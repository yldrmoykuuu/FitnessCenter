🏋️‍♂️ Spor Center Veri Tabanı Projesi 🏋️‍♀️


📋 Proje Hakkında
Bu proje, bir spor salonuna gelen kişilerin geliş-gidiş takvim bilgileri, yaptıkları spor aktiviteleri ve kullanıcı detaylarını tutmak için tasarlanmış kapsamlı bir veri tabanı yönetim sistemidir.
Amaç: Spor salonu üyelerinin kayıtlarının ve aktivitelerinin güvenli ve düzenli takibi.

⚙️ Proje Özellikleri
✅ Üye bilgileri, spor aktiviteleri ve giriş-çıkış takibi

📅 Spor salonu kullanımı takvim görünümünde

🔐 Yönetici ve kullanıcı olmak üzere iki farklı yetki seviyesi

🗑️ Silinen üyeler arşivlenir ve gerektiğinde geri alınabilir

🔄 Veri tabanı trigger ve stored procedure kullanımı

🔍 Gelişmiş arama ve filtreleme fonksiyonları

📤 Veri import/export ve yedekleme işlemleri

📄 Üyeleri PDF formatında görüntüleme
🗄️ Veri Tabanı Tasarımı
📌 Tablolar
kayitbilgi — Üye bilgileri

takipspor — Spor aktiviteleri

yonetici_bilgi — Yönetici hesapları

silinenkayit — Silinen üyelerin kayıtları

🔗 İlişkiler
takipspor tablosu, kayitbilgi ile uye_id üzerinden ilişkilidir.

Silinen üyeler silinenkayit tablosunda arşivlenir.

🛠️ Teknik Özellikler
⚡ Triggerlar
🗑️ Üye silindiğinde otomatik arşivleme

✏️ Üye bilgileri güncellendiğinde tetikleme

➕ Yeni üye eklenince bilgilendirme

🔍 Stored Procedures
Ad, telefon, cinsiyet gibi kriterlerle arama

Silinen kayıtlar ve spor aktiviteleri için arama

🔑 Kullanıcı Girişi & Yetkilendirme
👨‍💼 Yönetici: admin / 123

👤 Kullanıcı: Üyelere özel giriş

Yönetici paneli ile üyeler üzerinde tam kontrol sağlanır.

🖥️ Uygulama Sayfaları
🔐 Login Sayfası
👤 Kullanıcı Giriş Sayfası
📝 Kayıt Ol Sayfası
🏠 Kullanıcı Anasayfası
🔐 Yönetici Giriş Sayfası🛠️ Yönetici Anasayfası
👥 Yönetici Üye İşlemleri Sayfası
📄 Üyeleri PDF’ye Aktarma
🗑️ Silinen Üyelerin Listesi
📅 Spor Takvimi Görüntüleme
💾 Import / Export İşlemleri
📤 Veriler dışa aktarılabilir
📥 Import edilen veriler veri tabanına kaydedilir
💽 Yedekleme ve Geri Yükleme
🛡️ Veri tabanı yedeklenir
↩️ Yedekten geri dönüş mümkündür

🧰 Kullanılan Teknolojiler
Microsoft SQL Server

C# / ASP.NET

PDF oluşturma kütüphaneleri

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
<img width="945" height="572" alt="image" src="https://github.com/user-attachments/assets/ab6c834c-a2d4-4bb4-a22e-5b7193388175" />

👤 Kullanıcı Giriş Sayfası
<img width="945" height="539" alt="image" src="https://github.com/user-attachments/assets/79bb6cd5-fdaf-48ce-815b-2df3daf213de" />

📝 Kayıt Ol Sayfası

🏠 Kullanıcı Anasayfası
<img width="945" height="486" alt="image" src="https://github.com/user-attachments/assets/6dbacaa8-f470-4af1-af85-a1334d8e970b" />



🔐 Yönetici Giriş Sayfası

🛠️ Yönetici Anasayfası
<img width="940" height="570" alt="image" src="https://github.com/user-attachments/assets/3c30f77a-a95a-42e6-b9b7-070509e34989" />

👥 Yönetici Üye İşlemleri Sayfası
<img width="585" height="442" alt="image" src="https://github.com/user-attachments/assets/3cd4e6ec-0935-4f26-8a8a-c0460e8bb66e" />

📄 Üyeleri PDF’ye Aktarma

🗑️ Silinen Üyelerin Listesi

📅 Spor Takvimi Görüntüleme

💾 Import / Export İşlemleri
📤 Veriler dışa aktarılabilir
![Uploading image.png…]()

📥 Import edilen veriler veri tabanına kaydedilir

💽 Yedekleme ve Geri Yükleme
🛡️ Veri tabanı yedeklenir

↩️ Yedekten geri dönüş mümkündür

🧰 Kullanılan Teknolojiler
Microsoft SQL Server

C# / ASP.NET

PDF oluşturma kütüphaneleri

# RiseAssessment
## Proje Tanımı
1. Background Service mimarili Telefon Rehberi uygulamasıdır.
2. Background Service'ler arası iletişim MassTransit/RabbitMQ üzerinden gerçekleşir. 

## Projeyi Çalıştırmak için
1. Proje klasörü altında src klasörü oluşturulmalıdır. 
2. GitHub dizininde yer alan docker-compose.yml dosyası bu dizine taşınmalı ve docker-compose up ile ayaklandırılmalıdır. 
3. RabbitMQ, PosgreSQL ve pgAdmin container'lerinin ayaklandığından emin olunmalıdır.  
4. Proje .sln dosyası Visual Studio 2022 ile çalıştırılmalıdır. 
5. Proje açıldığında Solution Explorer'da Solution üzerinde sağ tuş ile Properties ekranı açılır. 
6. Açılan ekranda Common Properties altında StartUp Project seçilir. 
7. Ekranın sağ bölümünde Multiple startup projects işaretlenir ve listeden Contact.API ve Reporting.API projeleri Start olarak işaretlenir ve OK ile çıkılır. 
8. Proje Start edilir. 
9. Tanımlı tarayıcıda iki ayrı ugulama açılacaktır. 
10. Birinci uygulama Telefon Rehberine kişi ve iletişim bilgileri eklemek üzere kullanacağımız Contact.API servis metotlarını sağlamaktadır. 
11. Uygulama içerisine ön tanımlı olarak bazı kayıtlar oluşturulmuştur. 
12. Yeni kayıtlar oluşturmak, mevcut kayıtları düzenlemek veya silmek için bu servis metotlarını kullanabilirsiniz. 
13. İkinci uygulama ise Telefon Rehberi ile ilgili rapor oluşturmak için kullanılacaktır. 
14. Rapor oluşturmak için önce RequestNewReport metodu ile yeni rapor oluşturulmalı ve dönüş yapısındaki ReportId verisi saklanmalıdır. 
15. Rapor verilerini almak için ise GetReportResult metoduna oluşturulan rapora ait ReportId verisi yollanmalıdır. 

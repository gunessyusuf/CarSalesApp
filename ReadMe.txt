﻿Mvc (ASP.NET Core Web App Model View Controller), Business (Class Library), DataAccess (Class Library) ve AppCore (Class Library) projeleri oluşturulduktan sonra solution build edilir ve DataAccess projesine AppCore, Business projesine AppCore ve DataAccess, Mvc projesine ise Business, DataAccess ve AppCore projeleri referans olarak eklenir:

1) Solution altında AppCore adında yeni bir proje oluşturulur

2) DataAccess katmanında AppCore katmanındaki RecordBase'dan miras alan Entity'ler oluşturulur.

3) AppCore katmanına Microsoft.EntityFrameworkCore ile DataAccess katmanına Microsoft.EntityFrameworkCore.SqlServer ve Microsoft.EntityFrameworkCore.Tools paketleri NuGet'ten indirilir. .NET versiyonu hangisi ise NuGet'ten o versiyon numarası ile başlayan paketlerin son versiyonları indirilmelidir.

4) DataAccess katmanında DbContext'ten türeyen Db ve içerisindeki DbSet'ler ile connection string bilgisini de içeren DbContextOptions tipindeki objenin Db'ye constructor üzerinden enjekte edilmesini sağlayacak parametreli constructor oluşturulur, daha sonra Mvc katmanında Program.cs'teki IoC Container'da DbContext için bağımlılık yönetimi gerçekleştirilir.

5) Mvc katmanındaki appsettings.json ile eğer istenirse appsettings.Development.json içerisine connection string 
 yazılır. appsettings.json dosyası Properties klasöründeki launchSettings.json dosyasında konfigüre edilen production (canlı) profilinde,  appsettings.Development.json dosyası ise development (geliştirme) profilinde proje çalıştırıldığında kullanılacaktır.
Ayrıca launchSettings.json dosyasına view'larda değişiklik yapıldığında projenin tekrar başlatılma gerekliliğini ortadan kaldırmak için
"ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation" satırının eklenmesi
ve NuGet üzerinden Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation kütüphanesinin MvcWebUI projesine indirilmesi faydalı olacaktır.


6) Mvc katmanına Microsoft.EntityFrameworkCore.Design paketi NuGet'ten indirilerek Mvc projesi Startup Project yapılır. 
Tools -> NuGet Package Manager -> Package Manager Console açılır, Default project DataAccess seçilir ve önce örneğin add-migration v1 daha sonra update-database komutları çalıştırılır. Entity'ler veya DbSet'ler üzerinde yapılan her değişiklik için örneğin add-migration v2 daha sonra da update-database çalıştırılmalıdır.


7) DataAccess katmanında entity'ler üzerinden AppCore'daki RepoBase'den miras alan concrete (somut) repository (Repo) oluşturulur ve Mvc katmanında Program.cs'teki IoC Container'da bağımlılığı yönetilir.

8) Business katmanında entity'ler üzerinden model class'ları AppCore katmanındaki RecordBase'dan miras alacak şekilde oluşturulur, eğer istenirse Mvc katmanında view'larda kullanılmak üzere formatlama, ilişkili referans özellikleri kullanma, vb. için yeni özellikler eklenebilir.

9) Business katmanında model'ler üzerinden entity <-> model dönüşümlerini gerçekleştirip DataAccess katmanındaki Repo üzerinden veritabanı işlemleri gerçekleştirmek için AppCore'daki IService'i implemente eden interface'ler ile bu interface'leri implemente eden concrete (somut) service'ler oluşturulur ve Mvc katmanında Program.cs'teki IoC Container'da bağımlılıkları yönetilir.


10) Mvc katmanında yönetilecek model için controller ile ilgili action ve view'ları oluşturularak ilgili service constructor üzerinden enjekte edilir ve controller action'larında methodları kullanılarak model objeleri üzerinden işlemler (örneğin CRUD) gerçekleştirilir.

View <-> Controller (Action) <-> Service (Model) <-> Repository (Entity) <-> DbContext (Entity) <-> Database

11) Mvc katmanındaki Views -> Shared klasörü altındaki projede tüm oluşturulan view'ların bir şablon içerisinde gösterilmesini
sağlayan _Layout1.cshtml view'ı içerisinde controller ve action'lar üzerinden, örneğin menüye link'ler eklenir.

12) Eğer istenirse veritabanındaki tüm verilerin sıfırdan oluşturulmasını sağlayan, örneğin Mvc katmanında Areas klasöründeki Database area'sı içerisinde, bir controller ve aksiyonu yazılabilir.


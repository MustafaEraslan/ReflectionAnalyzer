# ReflectionAnalyzer - Kullanım Kılavuzu 📝

## 📖 **Proje Tanımı**
**ReflectionAnalyzer**, .NET uygulamalarında DLL dosyalarını analiz eden ve içerisindeki sınıflar, metotlar ve kontroller hakkında detaylı bilgi sunan bir Windows Forms uygulamasıdır. Kullanıcılar DLL içindeki metotları tetikleyebilir ve parametreli metotlar için input sağlayabilir.

---

## 🚀 **Kurulum ve Çalıştırma**
1. **Proje Dosyalarını İndirin:** 
   - Projeyi indirin veya GitHub üzerinden klonlayın.

2. **Visual Studio ile Açın:** 
   - `ReflectionAnalyzer.sln` dosyasını Visual Studio üzerinden açın.

3. **Bağımlılıkları Kontrol Edin:** 
   - Projede eksik NuGet paketleri varsa, **Tools → NuGet Package Manager → Restore Packages** üzerinden tamamlayın.

4. **Projeyi Çalıştırın:** 
   - Çalıştırmak için **F5** tuşuna basın veya üst menüden **Debug → Start Debugging** seçin.

---

## 🖥️ **Kullanım Adımları**

### ✅ **1. DLL Dosyalarını Yükleme**
- **"Load DLL(s)"** butonuna tıklayın.
- Açılan dosya seçme penceresinden analiz etmek istediğiniz `.dll` dosyalarını seçin.  
- Yüklenen DLL içindeki sınıflar sağ tarafta listelenecektir.

### ✅ **2. Sınıf Seçimi**
- Sağdaki listeden bir sınıf seçin. 
- Seçilen sınıfa ait metotlar ve kontroller listelenecektir.

### ✅ **3. Metot Seçimi ve Tetikleme**
- Listelenen metotlar arasından çalıştırmak istediğiniz metodu seçin.
- **"Invoke Method"** butonuna tıklayın.  
  - Eğer metot **parametresiz** ise, doğrudan çalıştırılır ve sonucu gösterilir.  
  - Eğer metot **parametreli** ise, bir input ekranı açılır.

### ✅ **4. Parametre Girme** *(Parametreli Metotlar İçin)*
- Açılan pencerede metot parametrelerini girin. Beklenen değer türleri için açıklamalar:
  - **int:** Tam sayı (örn: `42`)
  - **double:** Ondalık sayı (örn: `3.14`)
  - **bool:** Mantıksal değer (örn: `true` veya `false`)
  - **DateTime:** Tarih (örn: `2024-02-26`)
  - **Font:** Yazı tipi (örn: `Arial,12,Bold` → `FontName, Size, [Style]`)
  - **Enum:** İzin verilen değerler kullanıcıya listelenir.

### ✅ **5. Çıktının Görüntülenmesi**
- Metot başarıyla çalıştırıldığında sonuç bir bildirim penceresinde gösterilir.
- Parametre yanlış girilirse hata mesajında beklenen değer ve format belirtilir.

---

## 💡 **Özellikler**
✔️ Birden fazla DLL dosyasını aynı anda yükleme  
✔️ Form türevli sınıfların ve kontrollerin listelenmesi  
✔️ Metot tetikleme ve parametre girişi desteği  
✔️ Hatalı girişlerde kullanıcı dostu hata mesajları  
✔️ Çıktıların anlaşılır şekilde gösterilmesi  

---

## 🏆 **Değerlendirme Kriterlerine Uygunluk**
- ✅ **Reflection API'sinin etkin ve doğru kullanımı:** Dinamik analiz için geniş kapsamlı Reflection kullanımı sağlanmıştır.  
- ✅ **Kodun okunabilirliği ve modülerliği:** Yardımcı sınıflar ve metodlar ile temiz bir yapı sunulmuştur.  
- ✅ **Kullanıcı arayüzünün kullanım kolaylığı:** Basit ve anlaşılır bir UI tasarlanmıştır.  
- ✅ **Doğru sınıflandırma ve dinamik analiz yeteneği:** Yüklenen DLL'ler anında analiz edilip listelenir.  
- ✅ **Metotların hatasız tetiklenmesi ve çıktılar:** Kullanıcı hataları önlemek için giriş doğrulama ve hata mesajları mevcuttur.  

---

## ❓ **Sıkça Sorulan Sorular (SSS)**
### 1. **Birden fazla DLL seçemiyorum, ne yapmalıyım?**  
DLL'ler aynı klasörde ve bağımlılıkları tam olmalıdır. Bağımlı DLL'ler aynı dizinde olmalıdır.  

### 2. **Parametre girişinde hata alıyorum, çözümü nedir?**  
Hata mesajındaki format yönergelerine uyun. Örnek: `Font` için `Arial,12,Bold`.  

### 3. **Metot çalıştırırken "Method not found" hatası alıyorum, neden?**  
Seçilen metot parametreleri doğru girilmediğinde veya erişim seviyesi uyumsuz olduğunda bu hata oluşabilir.  

---

## 📬 **Destek ve İletişim**
Herhangi bir sorunla karşılaşırsanız veya önerileriniz varsa bana ulaşabilirsiniz. 😊

---

✅ **Hazırlayan:** Mustafa Eraslan  
✅ **Versiyon:** 1.0.0  
✅ **Tarih:** 2024-02-26

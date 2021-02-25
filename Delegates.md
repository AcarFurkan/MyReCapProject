# Delegates
- Delegelerin kullanım amaçları, metot adresi saklamaktır. Bazen metotlarımızı, ihtiyacımız olduğu anda çalıştırmak isteyebiliriz. 
Olay(event) tabanlı programlama ve asenkron programlama yaparken, 
anonim metot yazarken delegelerden faydalanırız. Bir diğer kullanım amacı da, bir metoda parametre olarak başka bir metot verebilmektir. 

 - Delegeler referans türlü bir tiptir. Dolayısı ile nesneleri heap’de durur. Girişte bahsettiğimiz gibi, görevleri metot adresi tutmaktır. 
 Burada dikkat edilmesi gereken nokta; **delegenin imzası, tuttuğu metodun imzası ile aynı olmalıdır.** İmzadan kastımız, geriye dönüş tipi ve aldığı parametrelerdir.
 Bir delege, birden fazla metot adresi tutabilir. Bu durumda FIFO (ilk giren ilk çıkar) prensibi geçerlidir. 
 Yani metotlar, delegeye bağlanma sırasına göre çalışırlar. Sonuç almak istediğimiz zaman, en son eklenen metodun yaptığı işi görürüz.
 
 ----- sagladigi avantajlar delegatlere bir den fazla method atanarak kullanilabilir.
 - 
 - public delegate void MyDelegate(); --> Tanımladığımız bu delege, geriye dönüş tipi void olan ve parametre almayan metotların adreslerini saklayabilir.
 - MyDelegate myDelegate = customerManager.SendMessage; --> customer manager da ki sendmessage methodunu  myDelegate e vermis olduk.
 - myDelegate += customerManager.ShowAlert; --> bu serkilde delegalere bir den fazla method atanabiliyor.
 - myDelegate -= customerManager.showAlert; --> bu sekilde delegatelerden bu method aliniyor.
 - myDelegate(); yaparsak method calisir.

 -  public delegate void MyDelegate2(string text); // parametre alan void donduren bir delegate de yaratabiliriz.

 -MyDelegate2 myDelegate2 = customerManager.SendMessage;
 myDelegate2 += customerManager.ShowAlert2;
 myDelegate2("hello"); // bu sekilde delegate atanan methodlarin hepsine ayni parametre gonderilir.
 
  - public delegate int MyDelegate3(int number1, int number2);  --> 2 tane parametre isteyen ve int donduren operasyonlara hizmet ediyor diyebiliriz.

- Gunumuzde programlamada action ve func gibi delege tutuculari siklikla kullaniyoruz.
# Action
-   0,1 veya daha fazla parametresi olan bir deger dondurmeyen methodu capsuller. --> dokumantasyon tanimi.
 
- find sadece denemek icin olusturuldu. gerekli aciklamalar kodun ustunde yapilmistir.
![delegeaction2PNG](https://user-images.githubusercontent.com/65075121/108988990-29a10800-76a6-11eb-9312-156277c59f34.PNG)
- dokumantasyon da yazana gore action parametre alir sikinti yok ama deger dondurmez ama deger dondurmemiz gerekiyorsa func kullanmamiz gerektigi soyleniyor.

-- bir tane de dokumantasyonda ornek verelim
- `    public static void Main(){
   
      Action<string> messageTarget; --> burda parametri string olan bir action olusturuluyor.

      messageTarget = ShowWindowsMessage; --> assagida string parametremesi olan method action a veriliyor.
      messageTarget("Hello, World!"); } --> ama parametre action cagrilirken ataniyor.
     
      private static void ShowWindowsMessage(string message){
   
      MessageBox.Show(message);}
      `
 - aslinda baska bir yazida deginecegim anonim(anonymous) method ve classlara simdi kullanacagim ama listede var onlari da detayli inceleyecegim.


 -`   public static void Main()
   {
      Action<string> messageTarget; --> burda parametri string olan bir action olusturuluyor.

      
         messageTarget = **delegate(string s) { ShowWindowsMessage(s); };** yildizlar arasidnaki kisim anonim method oluyor  yani bu sekildede atama yapilabiliyor ama daha cok asagida yazili olan lamda expresionla olusturolan kullaniliyor.
         // messageTarget = s => ShowWindowsMessage(s); bu bir ust satirin lamda ile yazilmis hali

      messageTarget("Hello, World!");
   } 

   private static void ShowWindowsMessage(string message)
   {
      MessageBox.Show(message);
   }
 `
 # Func
 -  0,1 veya daha fazla parametresi olan bir deger donduren methodu capsuller. --> dokumantasyon tanimi.

- ` Func<int, int , int> add = Topla;` --> iki int deger alip geriye int olarak bunlarin toplamini donen bir methodumuz oldugunu var sayalim
- `Console.WriteLine(Topla(2,3)); `--> parametreleri burada gonderiyoruz consolewriteline methoduyla return ile bize donen degeri yaziyoruz.

-` Func<int> getRandomNumber = delegegate(){  --> aynen bu sekilde parametresiz int deger donduren bir func tanimlamis oluyoruz.
     Random random = new Random();
     return random.Next(1,100)};
 `
 `Console.WriteLine(getRandomNumber));`
 
 -` Func<int> getRandomNumber =()=>new Random().Next(1,100) `
 -` Console.WriteLine(getRandomNumber)); ` bunun la hemen usteki ayni seyi ifade ediyor sadece lamda ile yazilmis
- Buda func icin bir ornek sadece return degerleri var.

- ![funkkk](https://user-images.githubusercontent.com/65075121/108998822-82769d80-76b2-11eb-8c79-3ec38f2ed3ea.PNG)

# Ozet
- func ve action method tutucudurlar birden fazla method alabilirler aralarindaki fark func return deger donduren methodlari alirken. action deger void donduren methodlari alir.
 # Kaynakca 
 - Btk akademi engin demirognun kursunun 27. bolumu
 - https://docs.microsoft.com/en-us/dotnet/api/system.func-2?view=net-5.0 --> func dokumani
 - https://docs.microsoft.com/en-us/dotnet/api/system.action-1?view=net-5.0 --> action dokumani
 - http://onursalkaya.blogspot.com/2011/03/c-delegate-nedir.html

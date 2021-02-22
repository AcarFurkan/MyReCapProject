# Aspect Oriented Programming


*Oncelikle yazdiklarim asagida verilen kaynakca kisminda olan linklerden alinmis kopya ve ozetlerdir. Kendim icin sonradan hatirlamasi kolay olsun diye parcalardan aldigim onemli bilgileri paylasiyorum. Eger zamaniniz varsa kaynaklari incelemenizi tavsiye ederim.*

## AOP

 - Uygulamalarımızı geliştirirken kodun; okunabilirliği, bakımı (maintability), modülaritesi, tekrar kullanılabilirliği (reusability) gibi pek çok kavram üzerinde çalışmaktayız. Her ne kadar bunlara dikkat etmeye çalışsak da proje büyüdükçe, kod blokları uzadıkça da kodun okunması,  bakımı vs. zorlaşabiliyor. Bu tarz durumlarda hızlı, temiz ve tekrar kullanılabilir çözümler üretmenin yolu Aspect Oriented Programming’den geçmektedir.

## Aspect Oriented Programming (AOP) Nedir?
>*AOP, yazılımın karmaşıklığını azaltmaya, modülariteyi artırmaya yarayan bir yaklaşım biçimidir. Buradaki modülariteden kasıt uygulama süresince sistemin birçok bölümünde kullanılan, fonksiyonel olmayan kodun yani kesişen ilgilerin ufak ufak parçalara ayrılmasıdır (Seperation of Cross Cutting Concerns). Bu sayede uygulama genelinde kullanılacak olan yapıları, sistemden soyutlamış olup enkapsüle de ederek birçok yerde kullanılmasını sağlar. Genel olarak AOP bir sorunu çözmektense var olan sistemin daha güzel bir hale getirilmesini de sağlamaya yardımcı olur denilebilir.*

- Yani AOP bir programlama dili veya OOP nin yerine gececek bir yaklasim degildir. 
- Sadece Loglama ya da hata yakalama operasyonlari icin degildir.


## Cross Cutting Concerns Nedir?
Cross Cutting Concerns’ün Türkçe anlamına bakacak olursak “Kesişen İlgiler” ya da “Çapraz Kesen İlgiler” diye geçmektedir. Yandaki fotoğrafa bakacak olursak da konunun anlaşılması ve devamlılığı için daha sağlıklı olacaktır. Yanda klasik bir monolitik yapı görünmektedir ve sağ kısımda Cross Cutting Concerns çatısı altında; security, caching, exception handling, logging gibi işlemler yer almaktadır. Bunların hepsi birer Concern yani ilgidir. Cross Cutting Concerns denmesinin sebebi projeyi dikine kesmesinden kaynaklı, dikine kesmesinin altında yatan anlam ise katman bağımsız, projenin istenilen yerinde kullanılması, kullanılan yerden soyutlanmış olmasından kaynaklıdır. Özetle yazılımın birden fazla yerinde kullanılan, fonksiyonel olmayan ihtiyaçlar denilebilir.
- [![Image]()](https://devnot.com/wp-content/uploads/2020/02/l4n_1.png)
> ## Birbiriyle kesisen ilgiler : 
- Loglama(logging)
- On Bellege alma(caching)
- Hata ayiklama (Exception Handling)
- Dogrulama (Validation)
- Yetkilendirme (Authorization)
- Kimlik Dogrulama (Authentication)
- Is kurallari (Businnes Rules) 

> Buraya kadar olan bilgiler ile AOP’un çıkış noktasının Cross Cutting Concerns’ler olduğunu söylemek yeridir. Cross Cutting Concerns’ler olmasa idi AOP’a ihtiyaç yoktur denilebilir.



- Birbirleri ile kesisen ilgileri ayirmak ve basitlestirmek icin kullanilabilecek bir yaklasimdir. 
- Yazilimdaki, tekrar kullanilabilirlik ve birimsellik kalite ozelliklerine katki saglar
- Yazilimdaki, ilgilerin 'concern' netlesmesini saglar
- Degisikliklere kolay adepte edilir.
- Yazilimin yasam suresinin uzamasini saglar.
- Genisletebilirlige on ayak olur.
- Daha duzenli ve kontrol edilebilir kod ortami saglar. (Kodlar sadece bizimle Allah arasinda kalmaz baskalarida anlayabilir. 
Yazdigimiz kod 1 Hafta once sadece Allah benim aramdaydi suan sadece allah biliyor muhabbetlerinden bizi kurtarir. )


## Aspect
 - Aspect dedigimiz olay bir biri ile kesisen ilgilerin ayrildigi parcalar demek. 
 - Yani Yukarida birbiri ile kesisen ilgiler basligi altinda siraladigim ilgiler her biri bir aspecti temsil ediyor.
 - Ayrica yazilim yasam suresi boyunca kazandigi ya da sahip oldugu ozellikler, sekiller... Gorunumu...
- Aspectler ilgilerin gizlendigi birer kutu gibidir. Kendi ozellikleri, sahip oldugu metodlar icelerinde saklidir.
 
 - Calisma zamaninda gerceklesebilir (Run-Time)
    - Performans acisindan yavas olur.
    - daha esnek olur genisletibilir.
 
 - Derleme zamaninda gerceklesebilir (Compile-Time)
    - Performans acisindan hizlidir.
    - Type-safe olur.
  
  
.Net AOP Spring.Net,PostSharp,Aspect#,Unity
.NetFramework icersinde AOP yaklasimini uygulayabilecek siniflar, methodlar, ozellikler... vs. mevcut.. 
--ContextBoundObeject
Attribute. springe

> ## Peki bu AOP yi, Aspect filan nasil kullanicaz
    - Yazilim gelistirme kapminda gelenlerler bilir bu aspectler attribute lar ile method veya classlarin uzerine asagida gibi yazilir.
>> [ValidationAspect(typeof(CarValidator))]  
        public IResult Add(Car car){}
- Bu modüllerin çalışması bir Interceptor sayesinde gerçekleşmektedir. 
- Interceptor klasorunde detayli bilgiyi bulabilirsiniz.



# Kaynaklar 
https://devnot.com/2020/aspect-oriented-programming/
https://www.gokhan-gokalp.com/aspect-oriented-programming-aop-giris-ve-ornek-bir-proje/

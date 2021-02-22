# Interceptors

## Interceptor Nedir, Nasıl Çalışır?
- İş ihtiyaçlarına göre; metoda girmeden önce, işlem bittikten sonra, hata durumunda gibi durumlarda araya girerek istediğimiz müdahaleyi yapabilmemizi sağlar. Bu araya girme işlemleri Compile-Time (Derleme Zamanı) veya Run-Time (Çalışma Zamanı) olmak üzere iki farklı yönden gerçekleştirilebilir.

- **Compile-Time (Derleme Zamanı):** Yazdığımız modüllere ait kodlar, uygulama derlendiği zaman çalışılması istenilen kod bloğu içerisine yerleştirilir.
- **Run-Time (Çalışma Zamanı):** Burada ise işlemler çalışma zamanında gerçekleşmektedir.

Bu interceptor altyapısını sağlamak için yazılmış birçok kaynak kod da mevcuttur. Aşağıda bazı diller için yazılmış olan interceptor kütüphanelerini bulabilirsiniz:

- .Net:
   - Postsharp, işlemleri compile-time olarak gerçekleştirmektedir, 4.2.17 versiyonu ücretsizdir güncel versiyonlar ise ücretli olarak sunulmaktadır.
   - Autofac, işlemleri run-time olarak gerçekleştirmektedir, açık kaynaktır ve kaynak kodlarına GitHub üzeriden erişilebilmektedir.
   
- Java:
   - Spring, Java için AOP implementasyonunda kolaylık sağlamaktadır. Kaynak kodlarına GitHub üzeriden erişilebilmektedir.
   
- PHP:
   - Go AOP, PHP dili için yazılmıştır. Kaynak kodlarına GitHub üzeriden erişilebilmektedir.




### Ozetle 
- Interceptorlar bir metodun tetiklenmesi ile aynı anda başka metodu otomatik olarak çağırmak ve çalıştırmak istediğimizde bu işi yerine getiren mekanizmadır.
Şöyle düşünelim ki birkaç  metot tetiklendiğinde aynı işi yapan bazı blokları olacak. Biz aynı kısımları bir metoda toplayıp Interceptor ile metotlar 
tetiklendiğinde gerekli işlemleri yine yaptırabiliriz, böylece kod kalabalığından kurtuluruz ve bakımı da kolaylaştırılmış olur. 

- Uygulmanin bazi kisimlartinda, araya girerek aspectlerimizi calistiririz(interception)


# Kaynaklar 
- https://devnot.com/2020/aspect-oriented-programming/ - ilk olarak bunu okumanizi oneririm.
- https://www.gokhan-gokalp.com/aspect-oriented-programming-aop-giris-ve-ornek-bir-proje/ --> aop,interceptor ve reflection yapilari kullanilmis guzel bir makale ve ornek iceriyor.

# IoC - Inversion of Control

*Oncelikle yazdiklarim asagida verilen kaynakca kisminda olan linklerden alinmis kopya ve ozetlerdir. Kendim icin sonradan hatirlamasi kolay olsun diye parcalardan aldigim onemli bilgileri paylasiyorum. Eger zamaniniz varsa direk kaynaklari incelemenizi tavsiye ederim.*

- **IoC(Inversion Of Control),** uygulamanın yaşam döngüsü boyunca birbirine bağımlılığı az (loose coupling) olan nesneler oluşturmayı amaçlayan bir yazılım geliştirme prensibidir. Nesnelerin yaşam döngüsünden sorumludur, yönetimini sağlar. 

- Ust sevili siniflar alt sevili siniflari kullanirken onlari interfaceleri uzeriden kullanilirlar yani assagidaki yapidan bahsediyorum.
> 
` class CustomerManager{
private ICustomer _customer;
public CustomerManager(ICustomer  customer){
    customer = customer;
}} `

 - biz bunlari kullanirken basit bir sekilde assagidaki gibi  new leyerek kullandik
 
 ` CustomerManager customerManager = new CustomerManager(new EfCustomerDal()); `
  
 ***IoC Container***  bizim ihtiyac duydugumuz nesnenin ornegini(instance) uretiyor. Yukaridaki ornege gore yani bizim icin efCustomerDal() in bir ornegini uretip tutuyor.
 Ayrica burda olusturulan instancenin yasam dongusunu tutuyor. ornek vermek gerekirse bir kere olusturulup gerimi verilecek yoksa her defasinda yeniden mi olusturulacak saatlikmi olusturacak gibi.

- Inversion of Control araclarina bazi ornekler:
     - Autofac,ninject,Castle Windsor,Structure Map,Light Inject, Dry Inject, Posts
Biz kursta Autofac aracini kullaniyoruz. Ama diger araclari da biraz inceledim mantigini anladiktan sonra sadece kelimeler degisiyor.

- Autofac ornegi
    - ` builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance(); `
    - Bu kod bize eger bir yerde ICustomerDal varsa ona EfCustomerDal ornegini veriyor.
    ` CustomerManager customerManager = new CustomerManager(ICustomerDal); ` -->  ICustomerDal  yerine EfCustomerDal ornegini (instance) veriyor.
    
    - #### Faydalari 
    
         - SingleInstance kismida bir tane ornek olusturmamazi sagliyor ICustomerDal isteyen herkese ayni refersansi veriyor. Bu sayede cok guzel bir bellek tasarrufu saglaniyor.
         - Projenin bagimliligini merkezi bir noktadan yonetebiliyorsun. Nasil yani ? sen veya musteri artik veritabanda degisiklik yapmak istiyorsun. Hadi farkli  bir ORM kullanalim diyorsunuz.
         - ORM -Entity Framework ORM(Object Relational Mapping) araçlarından biridir. ORM nedir dersek: İlişkisel veritabanı ile nesneye yönelik programlama(OOP) arasında bir köprü görevi gören araçtır. 
          ORM le alakali daha detayli bir arastirma yapip onu da seriye ekleyecegim.
         EfCustomerDal(entity framework) yerine XCustomerDal(x=hergangibir ORM olsun.)
         - ` CustomerManager customerManager = new CustomerManager(EfCustomerDal); ` --> eger bu sekilde olusturursak ve hadi X ORM ine gecelim dersek... "new CustomerManager(EfCustomerDal);" yazdigimiz her yeri XCustomerDal diye duzeltmemiz gerekir.
         - ` CustomerManager customerManager = new CustomerManager(XCustomerDal); ` --> her yeri bu sekilde yapmamiz gerekir
         - ama ` CustomerManager customerManager = new CustomerManager(ICustomerDal); ` bu sekilde olusturursak sadece IoC Container yapisinda duzelmemiz gerekir.
         - ` builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance(); ` --> bunun yerine ==> ` builder.RegisterType<XCustomerDal>().As<ICustomerDal>().SingleInstance(); ` yazarsak sadece bir yerde yaptigimiz degisiklik sayesinde IoC Container ICustomerDal isteyen herkese artik XCustomerDal ornegi (instance) i verir.
        
      - ## Ozetle
            - IoC sayisinde olusturulan nesnein yasam dongusunu kontrol edebiliyoruz.
            - Projenin bagimliligini merkezi bir noktadan yonetebiliyorsun.
            - Bağımlılıklar en aza indiği için test etmeyi/yazmayı kolaylaştırır.
    


Bu arada Visual Studio'nun kendi IoC'i alt yapisi bulunuyor ama Autofac bize bize Interceptors yani AOP alt yapisi da sagliyor bu yuzden Autofac kullaniyoruz. 
 
# Kaynaklar 
- https://www.youtube.com/watch?v=o2cGqDVNzWg
- https://devnot.com/2020/ioc-prensibi-nedir-ornek-proje-ile-kullanimi-ve-avantajlari/
- https://gokhana.medium.com/inversion-of-control-ioc-nedir-ve-avantajlar%C4%B1-nelerdir-cf05e42c16e4


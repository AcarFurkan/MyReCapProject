# Reflection Ve Assembly

*Oncelikle yazdiklarim asagida verilen kaynakca kisminda olan linklerden alinmis kopya ve ozetlerdir. Kendim icin sonradan hatirlamasi kolay olsun diye parcalardan aldigim onemli bilgileri paylasiyorum. Eger zamaniniz varsa direk kaynaklari incelemenizi tavsiye ederim.*
- Reflection ile run time(calisma ani) da calistiginiz her hangi bir nesnenin hakkinda bilgi toplayabilir ve bu topladiginiz  bilgiye gore calisma aninda istedigimiz zaman bir methodunu calistirabilirsiniz. 
Peki bu neden lazim ? 
- Mesala uygulama calisma aninda hagni nesne ile calisacagimizi bilmemizdir. kullanicin girdigi degerlere ne yapmamiz gerektigine karar verebiliyoruz. yani hangi nesnenin ve hangi methodun calisacagina uygulamanin gidisatina gore karar veriyoruz.
- bilgi almak derken mesala o methodun ozellikleri(property) nelerdir varsa attributelari nelerdir. paramertleri nelerdir
- veya calisma aninda kullanicinin ekrana girdigi degerlere gore bir sorgu olusturmaniz gerekebilir. veri tabanina bir insert bir select olusturmaniz gerekebilir.Yani dinamik olarak nesnenin ozelliklerine veri tabanina bir sorgu yazabilirsin.
> Ozetle calisma aninda nesnelerden bilgi almak ve yine calisma aninda nesneleri calistirabilmek.
- Peki her seyi reflection ile niye kullanmiyoruz. Cunku reflection pahali bir nesnedir. cok az da olsa performans kaybi yasatir.
[
![Ekran Alıntısı](https://user-images.githubusercontent.com/65075121/108801344-8d440c00-75a6-11eb-9e98-5e5476847445.PNG)
](url)

- bu rasi biraz karisik gelmis olabilir ama kesinlikle degil. 14. ile 24. satir arasinda dependency injection kullanilarak attribute tan validoturun type aliniyor yani biz incelemeye 26. satirdan baslayacagiz ve bilmemiz gereken tek sey _validatorType valitorun type ni tasiyor.
projeden ornek vermek gerekirse yani carValidator mi colorvalidator mi onun bilgisi _validatortype tutuyor.
- 26. satirda reflection da activator denilen bir sinif var. ` Activator.CreateInstance(nesne) ` dedigi yerde bize neyin instance ni olusturmak istiyorsak onu veriyor. yani nesne yazan yere biz neyin type ni verirsek bize onun bir ornegini(instanceni)olusturup veriyor. 
(IValidator yazmamizin sebebi) ` Activator.CreateInstance(nesne) ` biz bir obje donduruyor. biz buna gelen typenin IValidator dan implementte edilen bir nesne oldugunu bildimiz icin ona cast islemi uyguluyoruz ve validator'a atiyoruz.  
- Onemli olan nokta ***reflection*** sayesinde calisma aninda bir instance olusturup bunu 31. satirdaki ` ValidationTool.Validate(validator, entity); ` a verdik.

> Yine kurstan bir ornek daha verelim. 
![reflectionexample2](https://user-images.githubusercontent.com/65075121/108803623-30e3eb00-75ac-11eb-999f-d77b2c66d13c.PNG)

- Burada reflection dan bahsettigimiz icin ilgilenmemiz gereken nokta 14. satir ile 18. satir arasi. Burada ` type.GetCustomAttributes ` diyerek class a ait attribute larinin bilgisini , 
` type.GetMethod(method.Name)  ` methodun attribute larinin bilgisini aldik. 

## Ozetle 
- Reflection nun amaci calisma aninda nesnelerden bilgi almak ve yine calisma aninda nesnelerden instance(ornek) uretmek bu urettigimiz nesnelerini de calistirmak diyebiliriz.

# Assembly 
- Derlenmiş exe ve dll dosyalarına assembly denir. Çalışan programımız içinde bir assembly olabileceği gibi birden fazla assembly de olabilir.
Programımızda birden fazla assembly olması demek programımızın ilişkide olduğu bir ya da daha fazla dll dosyası mevcut demektir. 
System isim alanındaki AppDomain sınıfı çoklu assembly işlemleriyle ilgilenirken, System.Reflection isim alanındaki Assembly sınıfı ise tek bir assembly ile ilgili 
işlemler yapılmasını sağlar. 

- Kodumuzdan ornek vermek gerekirse `  var assembly = System.Reflection.Assembly.GetExecutingAssembly(); ` --> bu arkadas bizim icin calisan derlenen uygulamadiki 
yani .dll ve .exe dosyalarinda ki assembly yi almazi sagliyor.

- `  var assembly = System.Reflection.Assembly.LoadFrom(.exe dosyasinin yolunu buraya giriyoruz.); ` --> buda bize baska bir assembly alamamizi sagliyor.
- bunlari istersek using kismina System.Reflection i ekleyip  `  var assembly = Assembly.GetExecutingAssembly(); ` seklinde kullanabilir.


# Kaynakca 

- BTK akademi de engin hocanin c# kursunda 26.bolum 
- https://tr.wikibooks.org/wiki/C_Sharp_Programlama_Dili/Assembly_kavram%C4%B1 --> assembly icin guzel bir makale -- ilk basta okurken kafaniz biraz karisabilir. sonra engin hocanin  videolarini tekrar izleyebilirsin bide alta verdigim assembly icin benim cok begendim bir video serisi var onlarida izlerseniz oturacagini dusunuyorum. 

## Assembly ve Reflection icin cok guzel detayli bir anlatim iceriyor
- https://www.youtube.com/watch?v=ZWMWSmMQvXY
- https://www.youtube.com/watch?v=zBX0vZAAWIY
- https://www.youtube.com/watch?v=AfLq3evXwuo  --> burada sadece kendisi icinde calisan degil baske assemblyleride nasil alabilir ve kullanabilir onu anatmis 
- https://www.youtube.com/watch?v=0-nw1Jc_3M4 

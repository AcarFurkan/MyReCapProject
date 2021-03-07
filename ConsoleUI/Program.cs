using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

using Autofac.Extras.DynamicProxy;

using Castle.DynamicProxy;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceStack.Text;
using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities.Concrete;

namespace ConsoleUI
{

    class Program 
    {

         static void Main(string[] args)
        {
        //Program program = new Program(IUserService);
        // AutofacBusinessModule container = new AutofacBusinessModule();


        // var builder = new ContainerBuilder();

        //   builder.Register(c => new TaskController(c.Resolve<AutofacBusinessModule>()));

        var host = Host.CreateDefaultBuilder(args)
               .UseServiceProviderFactory(new AutofacServiceProviderFactory())
               .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());     
                }).Build();


            var svc = ActivatorUtilities.GetServiceOrCreateInstance<IUserService>(host.Services);
            /*var user1 = svc.Add(new User()
            {
                FirstName = "furkan",
                LastName = "acar",
                Email = "fafafgma@kk.asd",
                Password = "asdffh4jkghjk"
            });*/
          //  Console.WriteLine(user1.Message);

           

            foreach (var item in svc.GetAll().Data)
            {
                Console.WriteLine(item.FirstName);
            }


            // container.Load();
            // var builder = new ContainerBuilder();
            // container.RegisterType<Customer>();


            //CarManager carManager = new CarManager(new EfCarDal());



            //UserManager userManager = new UserManager(new EfUserDal());


            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            //Console.WriteLine(carManager.GetById(1).Data.Description);

            //Rental rental = new Rental {CarId=2,CustomerId=2,RentDate=DateTime.Now };
            //Console.WriteLine(rentalManager.GetRentalDetails().Data);
            /*
            foreach (var item in rentalManager.GetRentalDetails().Data)
            {
                Console.WriteLine(item.BrandName);
            }
            foreach (var item in carManager.GetAll().Data)
            {
                Console.WriteLine(item.Description);
            }
            
            */
            //var result = rentalManager.Add(rental);
             //Console.WriteLine(result.Message);

             //var result2 = rentalManager.Update(rental);
             //Console.WriteLine(result2.Message);
            //var result3 = rentalManager.Delete(rental);
            //Console.WriteLine(result3.Message);



           
            /*
            foreach (var item in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(item.BrandName + " " + item.ColorName + " " + item.UnitPrice);
            }

            Console.WriteLine("************************************");*/
            //var customer1 = new Customer() {UserId=1,CompanyName= "furkan" };// user id nasil eklerim bul
            //var customer2 = new Customer() { UserId = 1, CompanyName = "yusuf" };
            //customerManager.Add(customer2);
            /*
            var user1 = userManager.Add(new User()
            {
                FirstName = "furkan",
                LastName = "acar",
                Email = "fafaf@gma.asd",
                Password = "asdf"
            });
            var x = new User()
            {
                FirstName = "furkan",
                LastName = "acar",
                Email = "fafaf@gma.asd",
                Password = "asdf"
            };
            var y = new User()
            {
                FirstName = "furkan",
                LastName = "acar",
                Email = "fafaf@gma.asd",
                Password = "asdf"
            };


            var user2 = userManager.Add(new User()
            {
                FirstName = "yusuf",
                LastName = "acar",
                Email = "fafaf@gma.asd",
                Password = "asdf"
            });
            User[] users = {x,y };
            */
            //Console.WriteLine(user2.Message);
            // var result=  rentalManager.Rent(1, 1);
            //var result2 = rentalManager.Rent(2, 2);
            // Rental rental1 = new Rental(){CarId= 2,CustomerId=2};
            //var result3= rentalManager.Rent(3,2);
            //var result1= rentalManager.Add(rental1);
            // var result2=rentalManager.Deliver(3);
            // Console.WriteLine(result1.Message);
            //  Console.WriteLine(result2.Message);
            // Console.WriteLine(result3);

            //ReturnRental(rentalManager);






            //carManager.Add(new Car {ModelYear=2000,DailyPrice=150,BrandId=3,ColorId=3,Description="furkans car"});


            /*
             foreach (var item in carManager.GetAll())
             {
                 Console.WriteLine(item.DailyPrice);
             }

             BrandManager brandManager = new BrandManager(new EfBrandDal());

             Console.WriteLine(brandManager.GetById(carManager.GetById(1).BrandId).BrandName);
             brandManager.Add(new Brand { BrandName = "DFGHS" });
             */
        }
        /*
        private static void ReturnRental(RentalManager rentalManager)
        {
            Console.WriteLine("Kiraladığınız araba hangi Car Id'ye sahip?");
            int carId = Convert.ToInt32(Console.ReadLine());
            var returnedRental = rentalManager.GetRentalDetails(r => r.CarId == carId);//bunun icin rentalla sildigin rental dto method icini tekrar yazman gerek onu kontrol et
            foreach (var rental in returnedRental.Data)
            {
                rental.ReturnDate = DateTime.Now;
                Console.WriteLine(returnedRental.Message);
               
            }
            rentalManager.Update(carId);
        }*/
    }
}

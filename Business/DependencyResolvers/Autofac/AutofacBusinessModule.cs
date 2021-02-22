using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();

            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();

            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();

            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();







            


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//calisan uygulama icinde

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()// implemente edilmis interfacleri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() 
                {
                    Selector = new AspectInterceptorSelector()//bu bizim registerteyple yazdigimiz butun siniflar icin git bak diyo bu sinifin aspect interceptori varmi diyo --     onlar icin aspectinterceptorselctori cagir diyor//boyle bir sinif ekledik ona git bak 
                }).SingleInstance();// bu bizim intercepterimizden bir instance uretiyo yani validatoraspecti gibi aspectleri calistiriyor.

        }
    }
}

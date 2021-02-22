using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//
                .ConfigureContainer<ContainerBuilder>(builder =>//
                {
                    builder.RegisterModule(new AutofacBusinessModule());//.net core yerine ben baska bir IOC container kullanmak istersem burdaki // islemleri yapiyoruz./// aslinda gene kendi ioc alt yapisini kullaniyor .net but fabrika olarak bizim yazdimiz autofacbusinnesmodule u kullan diyoruz
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

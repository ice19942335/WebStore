using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Data;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Entities;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var log4NetConfigXml = new XmlDocument();

            var configFileName = "log4net.config";

            log4NetConfigXml.Load(configFileName);

            var repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repository, log4NetConfigXml["log4net"]);

            ILog log = LogManager.GetLogger(typeof(Program));


            log.Info("Application started");

            var host = CreateWebHostBuilder(args).Build();


            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

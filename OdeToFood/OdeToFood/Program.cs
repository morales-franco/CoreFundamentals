using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /*
         * Default Web Server: Kestrel.
         * We'll use Kestre Web Server --> Cross platform web servers
         * Configure IIS integration : ej si nuestra app usa Windows Auth le pasa las credenciales a Kestrel
         * Confiura Logging para ver mensajes en el output de VS
         * Genera un IConfiguration service para que podamos utilizarlo desde la aplicación, leer archivos, configuraciones externas, Json Files, Enviroments variables, command lines arguments, etc 
         */
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

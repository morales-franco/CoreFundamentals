using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //Register dependency injection
        public void ConfigureServices(IServiceCollection services)
        {
            //Authentication Services
            services.AddAuthentication(options =>
            {
                //Friendly name de como autenticamos a los usuarios. Tokens, cookies, etc
                //En nuestro caso una vez que el provider acepte las credenciales nos devuelve un token que nosotros
                //almacenamos en una cookie y es alli donde estaran las credenciales del usuario --> DefaultScheme: Cookies
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //Obligamos al usuario a autenticarse con nuestro provider OpenIdConnect
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect( options => {

                //Bind: Automaticamente hace un binding de las properties del bloque de AzureAd con las properties de
                //options. En este caso setea las siguientes properties:
                //options.Authority
                //options.ClientId
                _configuration.Bind("AzureAd", options);
            })
            .AddCookie();



            /*
             * Singleton: sola una instancia en toda la aplicación
             * Transient: Siempre que alguien necesite una instancia de un servicio, por ejemplo de IGreeter, genera una nueva
             * Scoped: Se creara una unica instancia por cada request Http. Si se la llama de varios lados se reutiliza la instancia
             */
            //Registro service
            services.AddSingleton<IGreeter, Greeter>();
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configuración http processing pipeline
        //Analisis de cada HTTP request that arrive
        //Middleware controla como responde nuestra app a c/http request
        //Middleware: Esta conformado por un conjunto de piezas, encargado de recibir requests y enviar el response.
        //Cada pieza analiza el request y la va pasando a la siguiente si corresponde

        //Comportamiento de Pieces: 
        //Logger --> Authorizer --> Router -->
        //Logger<-- Authorizer<-- Router<--

        //Pieces que conforman el Request:
        //1) Logger: Puede ver los request, header, cookies, etc
        //2) Authorizer: Analiza JWT, una cookie especifica, etc.
        //3) Router: Redirecciona al controller, retorna la pagina correspondiete, xml o json

        //Cualquier pieza del Middleware puede reject un request
        //Con IApplicationBuilder nosotros configuramos el Middleware
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              IGreeter greeter)
        {

            if (env.IsDevelopment())
            {
                //env.EnvironmentName -->  Analiza el valor de variable de entorno del servidor ASPNETCORE_ENVIROMENT
                //Esta variable se puede configurar en el launchSettings.json
                //Se pueden configurar distintos perfiles por ejemplo si se levanta en un IISExpress setear el valor de la variable en Development or production

                //show useful information for developer --> Muestra Información detallada de la excepción en el browser
                app.UseDeveloperExceptionPage();
            }

            /*
             * Examina URL entrantes y podemos configurarles reglas como conditionals o regular expressions que si matchean o  no con el incoming request 
             * podemos por ejemplo redirigirlos a cierto lado
             * Me aseguro que todas los request entrantes sean HTTPS
             * Cada vez que un cliente se quiera conectar a nuestra app si se conecta con una url HTTP se lo redigira a la misma pagina de la app
             * pero con HTTPS
             */
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            //Muestra welcome page
            //app.UseWelcomePage();

            //app.UseDefaultFiles();
            //Permite acceder a archivos statics que estan en www.root si la coincidencia de nombre es exacta
            app.UseStaticFiles();


            //Por default los archivos staticos vienen de la carpeta www.root entonces tengo que configurar el middleware
            //para que permita al servidor servir contenido estatico desde la carpeta node_modules

            app.UseNodeModules(env.ContentRootPath);

            //despues de staticFiles pero antes de useMvc
            //Una vez que especificamos que va ausar Authentication tenemos que setearles los servicios en ConfigureServices
            app.UseAuthentication();


            app.UseMvc(ConfigureRoutes);

            //app.Run(async (context) =>
            //{
            //    //Code for every request recibido
            //    var greeting = greeter.GetMessageOfTheDay();
            //    await context.Response.WriteAsync($"{ greeting } : { env.EnvironmentName }" );
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // Home/Index
            //? optional
            //{action=Index} Default value

            routeBuilder.MapRoute("Default", 
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}

using System;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using ReactiveUI;
using Splat;

namespace ClassLibrary1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IPublicClientApplication app)
        {
            //Configuration = configuration;


            //var n = new DataHandler();
            //n.getJSONasObject();
            ConfigureServices(app);

        }

        public void ConfigureServices(IPublicClientApplication app)
        {

            /* Locator.CurrentMutable.RegisterConstant(app);
             Locator.CurrentMutable.RegisterConstant(new RoutingState(), typeof(RoutingState));*/
            //Locator.CurrentMutable.RegisterLazySingleton(() => new DiceRollerService(), typeof(DiceRollerService));


        }
    }
}
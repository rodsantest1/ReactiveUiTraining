using ClassLibrary1;
using Microsoft.Identity.Client;
using ReactiveUI;
using Splat;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }

        public App()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            _clientApp = PublicClientApplicationBuilder.Create(new Guid().ToString())
                .WithAuthority(string.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}/v2.0", new Guid().ToString()))
                .WithDefaultRedirectUri()
                .Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new Startup(_clientApp);
        }
    }
}


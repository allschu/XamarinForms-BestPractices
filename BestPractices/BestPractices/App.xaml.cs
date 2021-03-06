using BestPractices.IoC;
using BestPractices.Services.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BestPractices
{
    public partial class App : Application
    {
        private Task InitNavigation()
        {
            var navigationService = Bootstrap.GetLocater().GetInstance<INavigationService>();
            return navigationService.InitializeAsync();
        }

        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            await InitNavigation();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

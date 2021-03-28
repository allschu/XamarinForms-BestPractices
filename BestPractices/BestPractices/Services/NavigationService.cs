using BestPractices.Logging;
using BestPractices.Services.Interfaces;
using BestPractices.ViewModels;
using BestPractices.Views;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BestPractices.Services
{
   public class NavigationService : INavigationService
    {
        private readonly ILoggerAgent _loggerAgent;

        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        public NavigationService(ILoggerAgent loggerAgent)
        {
            _loggerAgent = loggerAgent ?? throw new ArgumentNullException(nameof(loggerAgent));
            _loggerAgent.Information($"{nameof(NavigationService)}: initizaling");
        }

        public Task InitializeAsync()
        {
            _loggerAgent.Information($"{nameof(NavigationService)}: InitializeAsync");
            return NavigateToAsync<SearchViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            _loggerAgent.Information($"{nameof(NavigationService)}: InitializeAsync");
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);
            
            if (page is MainPage)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            Page page = Activator.CreateInstance(pageType) as Page;

            if (parameter != null)
            {
                var contentPage = (ContentPage)page;
                contentPage.BindingContext = parameter;
                return contentPage;
            }

            return page;
        }

        /// <summary>
        /// Retrieves the correct Views page from the Views
        /// Watch out! this requires a specific naming convention between your views and viewmodels
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns>Specific view</returns>
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            viewName = viewName.Insert(viewName.Length, "Page");

            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);

            if (viewType == null)
                throw new ArgumentOutOfRangeException(nameof(viewModelType));

            return viewType;
        }

    }
}

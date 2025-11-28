using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagerDesktopUI.Core;
using OrderManagerDesktopUI.ViewModels;
using OrderManagerDesktopUI.Views;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services;
using System.Data;
using System.Windows;

namespace OrderManagerDesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<NewOrderViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Delivery>,  DeliveryRepository>();
            services.AddScoped<IRepository<MobilePayment>, MobilePaymentRepository>();
            services.AddScoped<IRepository<Note>, NoteRepository>();
            services.AddScoped<IRepository<OrderLine>, OrderLineRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Payment>, PaymentRepository>();
            services.AddScoped<IRepository<PickUp>, PickUpRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<Func<Type, ViewModel>>(_serviceProvider =>
                viewModelType => (ViewModel)_serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagerDesktopUI.Core;
using OrderManagerDesktopUI.ViewModels;
using OrderManagerDesktopUI.Views;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services;
using System.Data;
using System.IO;
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
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddScoped<NewOrderViewModel>();
            services.AddScoped<NewOrderProductsViewModel>();
            services.AddScoped<NewOrderDetailsViewModel>();
            services.AddSingleton<OrdersViewModel>();
            services.AddSingleton<ProductsViewModel>();
            services.AddSingleton<CustomersViewModel>();
            services.AddSingleton<SalesDataViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddScoped<IDataAccess, DataAccess>();
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

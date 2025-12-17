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
using OrderManagerLibrary.Services.Interfaces;
using System.Data;
using System.IO;
using System.Windows;
using Size = OrderManagerLibrary.Model.Classes.Size;

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

            services.AddSingleton<Func<Type, ViewModel>>(_serviceProvider =>
                             viewModelType => (ViewModel)_serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<NewOrderViewModel>();
            services.AddTransient<NoteViewModel>();
            services.AddTransient<NewOrderDetailsViewModel>();
            services.AddTransient<OrdersViewModel>();
            services.AddTransient<ProductsViewModel>();
            services.AddTransient<AddProductViewModel>();
            services.AddTransient<EditProductViewModel>();
            services.AddTransient<CustomersViewModel>();
            services.AddTransient<SalesDataViewModel>();
            services.AddScoped<IDataAccess, DataAccess>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<PaymentMethod>, PaymentMethodRepository>();
            services.AddScoped<IRepository<Note>, NoteRepository>();
            services.AddScoped<IRepository<OrderLine>, OrderLineRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Payment>, PaymentRepository>();
            services.AddScoped<IRepository<PickUp>, PickUpRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Size>, SizeRepository>();
            services.AddScoped<IRepository<Taste>, TasteRepository>();
            services.AddScoped<IRepository<ProductSize>, ProductSizeRepository>();
            services.AddScoped<IRepository<ProductTaste>, ProductTasteRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderLineService, OrderLineService> ();
            services.AddScoped<IPaymentService, PaymentService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        private void Trigger_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }

}

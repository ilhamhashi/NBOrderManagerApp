using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace OrderManagerDesktopUI.ViewModels
{
    public class ProductsViewModel : ViewModel
    {
        private readonly IOrderService _orderservice;
        public ObservableCollection<Product>? Products { get; set; }
        public ICollectionView? ProductsCollectionView { get; set; }

        private string noteText = "notepr";

        public string NoteText
        {
            get { return noteText; }
            set { noteText = value; OnPropertyChanged(); }
        }

        public ProductsViewModel(IOrderService orderService)
        {
            _orderservice = orderService;

            Products = new(_orderservice.ViewProductCatalogue());
            ProductsCollectionView = CollectionViewSource.GetDefaultView(Products);
        }
    }
}
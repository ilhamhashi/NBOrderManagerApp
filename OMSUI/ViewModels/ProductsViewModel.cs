using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderManagerDesktopUI.ViewModels;

public class ProductsViewModel : ViewModel
{
    private readonly IProductService _productService;

    private ObservableCollection<Product> Products { get; }
    public ICollectionView ProductsCollectionView { get; }

    private Product? _selectedProduct;
    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set { _selectedProduct = value; OnPropertyChanged(); }
    }

    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(); }
    }

    private string _description;

    public string Description
    {
        get { return _description; }
        set { _description = value; OnPropertyChanged(); }
    }

    private decimal _price;

    public decimal Price
    {
        get { return _price; }
        set { _price = value; OnPropertyChanged(); }
    }

    private string searchTerm;
    public string SearchTerm
    {
        get => searchTerm;
        set
        {
            searchTerm = value;
            OnPropertyChanged();
            ProductsCollectionView.Refresh();
        }
    }

    public ICommand CreateProductCommand { get; }
    public ICommand UpdateProductCommand { get; }
    public ICommand DeleteProductCommand { get; }

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        Products = new ObservableCollection<Product>(_productService.GetAllProducts());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(Products);
        ProductsCollectionView.Filter = FilterProducts;

        // Commands
        CreateProductCommand = new RelayCommand(execute => CreateProduct(), canExecute => true);
        UpdateProductCommand = new RelayCommand(execute => UpdateProduct(), canExecute => SelectedProduct != null);
        DeleteProductCommand = new RelayCommand((param) => DeleteProduct(param), canExecute => true);
    }


    private bool FilterProducts(object obj)
    {
        if (obj is not Product p) return false;

        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;

        return p.Name.ToLower().Contains(SearchTerm.ToLower());
    }

    private void CreateProduct()
    {
        var product = new Product(Name, Description, Price);
        _productService.CreateProduct(product);
        Products.Add(product);
        MessageBox.Show($"You have succesfully created {product.Name}.");
        ResetFields();
    }

    private void ResetFields()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
    }

    private void UpdateProduct()
    {
        _productService.UpdateProduct(SelectedProduct);
        ProductsCollectionView.Refresh();
    }

    private void DeleteProduct(object rowData)
    {
        _productService.RemoveProduct((rowData as Product).Id);
        Products.Remove(rowData as Product);
    }
}
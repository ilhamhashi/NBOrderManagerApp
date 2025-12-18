using OrderManagerDesktopUI.Core;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Size = OrderManagerLibrary.Model.Classes.Size;

namespace OrderManagerDesktopUI.ViewModels;

public class ProductsViewModel : ViewModelBase
{
    private readonly IProductService _productService;
    private ObservableCollection<Product> _products { get; }
    public ICollectionView ProductsCollectionView { get; }
    public ObservableCollection<Size> SizeOptions { get; } = [];
    public ObservableCollection<Taste> TasteOptions { get; } = [];
    private ObservableCollection<Size> _availableSizes;
    public ObservableCollection<Size> AvailableSizes
    {
        get { return _availableSizes; }
        set { _availableSizes = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Taste> _availableTastes;
    public ObservableCollection<Taste> AvailableTastes
    {
        get { return _availableTastes; }
        set { _availableTastes = value; OnPropertyChanged(); }
    }

    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get { return _selectedProduct; }
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

    private Size _selectedSize;
    public Size SelectedSize
    {
        get { return _selectedSize; }
        set { _selectedSize = value; OnPropertyChanged(); }
    }

    private Taste _selectedTaste;
    public Taste SelectedTaste
    { 
        get { return _selectedTaste; }
        set { _selectedTaste = value; OnPropertyChanged(); }
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
    public ICommand AddSizeToProductCommand { get; }
    public ICommand AddTasteToProductCommand { get; }
    public ICommand RemoveSizeFromProductCommand { get; }
    public ICommand RemoveTasteFromProductCommand { get; }

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        _products = new ObservableCollection<Product>(_productService.GetAllProducts());
        ProductsCollectionView = CollectionViewSource.GetDefaultView(_products);
        ProductsCollectionView.Filter = FilterProducts;
        AvailableSizes = new ObservableCollection<Size>(_productService.GetAllSizes());
        AvailableTastes = new ObservableCollection<Taste>(_productService.GetAllTastes());
        CreateProductCommand = new RelayCommand(execute => CreateProduct(), canExecute => true);
        UpdateProductCommand = new RelayCommand(execute => UpdateProduct(), canExecute => SelectedProduct != null);
        DeleteProductCommand = new RelayCommand((param) => DeleteProduct(param), canExecute => true);
        AddSizeToProductCommand = new RelayCommand(execute => AddSizeToProduct(), canExecute => SelectedSize != null);
        AddTasteToProductCommand = new RelayCommand(execute => AddTasteToProduct(), canExecute => SelectedTaste != null);
        RemoveSizeFromProductCommand = new RelayCommand(execute => RemoveSizeFromProduct(), canExecute => SelectedSize != null);
        RemoveTasteFromProductCommand = new RelayCommand(execute => RemoveTasteFromProduct(), canExecute => SelectedTaste != null);
    }

    private void AddSizeToProduct()
    {        
        SizeOptions.Add(SelectedSize);
        AvailableSizes.Remove(SelectedSize);
        SelectedSize = null;
    }

    private void AddTasteToProduct()
    {
        TasteOptions.Add(SelectedTaste);
        AvailableTastes.Remove(SelectedTaste);
        SelectedTaste = null;
    }

    private void RemoveSizeFromProduct()
    {        
        AvailableSizes.Add(SelectedSize);
        SizeOptions.Remove(SelectedSize);
        SelectedSize = null;
    }

    private void RemoveTasteFromProduct()
    {
        AvailableTastes.Add(SelectedTaste);
        TasteOptions.Remove(SelectedTaste);
        SelectedTaste = null;
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
        var product = new Product(Name, Description, Price, [.. SizeOptions], [.. TasteOptions]);
        _productService.CreateProduct(product);
        _products.Add(product);
        MessageBox.Show($"You have succesfully created {product.Name}.");
        ResetFields();
    }

    private void ResetFields()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
        SelectedSize = null;
        SelectedTaste = null;
        SelectedProduct = null;
        SizeOptions.Clear();
        TasteOptions.Clear();
        AvailableSizes = new ObservableCollection<Size>(_productService.GetAllSizes());
        AvailableTastes = new ObservableCollection<Taste>(_productService.GetAllTastes());
    }

    private void UpdateProduct()
    {
        if (SelectedProduct != null)
        {
            SelectedProduct.SizeOptions = [.. SizeOptions];
            SelectedProduct.TasteOptions = [.. TasteOptions];
            _productService.UpdateProduct(SelectedProduct);
            ProductsCollectionView.Refresh();
            ResetFields();
        }
    }

    private void DeleteProduct(object rowData)
    {
        if (rowData != null)
        {
            var product = rowData as Product;
            MessageBoxResult result = MessageBox.Show($"Do you want to remove {product.Name}?",
                                      "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _productService.RemoveProduct(product.Id);
                _products.Remove(product);

                MessageBox.Show($"{product.Name} is removed.", "OrderManager",
                           MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
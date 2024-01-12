using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1;
using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Views;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    public class ProductoViewModel : ObservableObject
    {
        // Services
        private readonly IProductRepository _productRepository;
        
        // Collections
        private List<Producto> _listaProductos;
        public List<Producto> ListaProductos 
        {
            get => _listaProductos;
            set => SetProperty(ref _listaProductos, value);
        }

        private Producto _selectedProduct;
        public Producto SelectedProduct
        {
            get => _selectedProduct;
            set {
                SetProperty(ref _selectedProduct, value);
                ShowProductDetail.Execute(_selectedProduct);
            }
        }

        public ProductoViewModel(IProductRepository productRepository) {

            // Init services
            _productRepository = productRepository;
            // Init collection
            UpdateData();
        }
        public ICommand CreateProduct =>
            new Command(async () =>
            {
               await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage(){
                     BindingContext = new NuevoProductoViewModel(AddProduct)
               });
            });

        public ICommand ShowProductDetail =>
            new Command(async () =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new DetalleProductoPage(){
                     BindingContext = new DetalleProductoViewModel(SelectedProduct, UpdateProduct, DeleteProduct)
                });
            });

        private void AddProduct(Producto newProduct)
        {
            _productRepository.Add(newProduct);
            UpdateData();

        }

        private void UpdateProduct(Producto product)
        {
            _productRepository.Update(product);
            UpdateData();
        }

        private void DeleteProduct(Producto product)
        {
            _productRepository.Delete(product);
            UpdateData();
        }

        private void UpdateData()
        {
            ListaProductos = _productRepository.GetAll();
        }
    }
}

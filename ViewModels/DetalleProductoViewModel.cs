using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.ViewModels;
using ProductoMVVMSQLite.Views;

namespace ProductoMVVMSQLite;

public class DetalleProductoViewModel : ObservableObject
{
    // Observable properties
    private string _nombre;
    private string _cantidad;
    private string _descripcion;
    public string Nombre { get => _nombre; set => SetProperty(ref _nombre, value); }
    public string Cantidad { get => _cantidad; set => SetProperty(ref _cantidad, value); }
    public string Descripcion { get => _descripcion; set => SetProperty(ref _descripcion, value); }

    // Utils
    private readonly Producto _existingProduct = null;

    // Update product delegate
    private delegate void UpdateProductDelegate(Producto product);
    private readonly UpdateProductDelegate _updateProduct;

    // Delete product delegate
    private delegate void DeleteProductDelegate(Producto product);
    private readonly DeleteProductDelegate _deleteProduct;


    public DetalleProductoViewModel(Producto selectedProduct,
         Action<Producto> updateProductAction, Action<Producto> deleteProductAction)
    {
        // Init properties
        Nombre = selectedProduct.Nombre;
        Cantidad = selectedProduct.Cantidad.ToString();
        Descripcion = selectedProduct.Descripcion;
        // Init utils
        _existingProduct = selectedProduct;
        // Init actions
        _updateProduct = new(updateProductAction);
        _deleteProduct = new(deleteProductAction);
    }

    public ICommand UpdateExistingProduct =>
        new Command(async () =>
        {
            await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage(){
                     BindingContext = new NuevoProductoViewModel(_existingProduct, UpdateProduct)
                });
        });

    public ICommand DeleteExistingProduct =>
        new Command(async () =>
        {
            _deleteProduct(_existingProduct);
            await App.Current.MainPage.Navigation.PopAsync();
        });

    private void UpdateProduct(Producto existingProduct)
    {
        _updateProduct(existingProduct);
        UpdateData();
    }

    private void UpdateData()
    {
        // Update observable properties
        Nombre = _existingProduct.Nombre;
        Cantidad = _existingProduct.Cantidad.ToString();
        Descripcion = _existingProduct.Descripcion;
    }
}

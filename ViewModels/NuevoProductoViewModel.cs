using ProductoMVVMSQLite.Models;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    public class NuevoProductoViewModel
    {
        // Observable properties
        public string Nombre { get; set; }
        public string Cantidad { get; set; }
        public string Descripcion { get; set; }

        // Utils
        private readonly Producto _existingProduct = null;

        // Add product delegate
        private delegate void AddProductDelegate(Producto newProduct);
        private readonly AddProductDelegate _addProduct;

        // Update product delegate
        private delegate void UpdateProductDelegate(Producto product);
        private readonly UpdateProductDelegate _updateProduct;

        // Constructors
        public NuevoProductoViewModel(Producto existingProduct, Action<Producto> updateProductAction)
        {
            // Init properties
            Nombre = existingProduct.Nombre;
            Cantidad = existingProduct.Cantidad.ToString();
            Descripcion = existingProduct.Descripcion;
            // Init utils
            _existingProduct = existingProduct;
            // Init delegates
            _updateProduct = new(updateProductAction);
        }
        public NuevoProductoViewModel(Action<Producto> addProductAction)
        {
            _addProduct = new(addProductAction);
        }

        public ICommand GuardarProducto =>
            new Command(async () =>
            {
                if(_existingProduct != null)
                    UpdateProduct(_existingProduct);
                else
                    AddProduct();
                await App.Current.MainPage.Navigation.PopAsync();
            });

        private void AddProduct()
        {
            Producto newProduct = new()
                {
                    Nombre = Nombre,
                    Descripcion = Descripcion,
                    Cantidad = Int32.Parse(Cantidad)
                };
            _addProduct(newProduct);
        }
        private void UpdateProduct(Producto product)
        {
            product.Nombre = Nombre;
            product.Descripcion = Descripcion;
            product.Cantidad = Int32.Parse(Cantidad);
            // Update exising product
            _updateProduct(product);
        }
    }
}

using ProductoMVVMSQLite.Services;
using ProductoMVVMSQLite.ViewModels;
using ProductoMVVMSQLite.Views;

namespace ProductoMVVMSQLite
{
    public partial class App : Application
    {
        public App(IProductRepository productRepository)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ProductoPage(){
                BindingContext = new ProductoViewModel(productRepository)
            });
        }
    }
}
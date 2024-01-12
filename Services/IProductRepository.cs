using ProductoMVVMSQLite.Models;

namespace ProductoMVVMSQLite;

public interface IProductRepository
{
    public List<Producto> GetAll();
    public Producto Get(int IdProducto);
    public void Add(Producto producto);
    public void Update(Producto producto);
    public void Delete(Producto producto);
}

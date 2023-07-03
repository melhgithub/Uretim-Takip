using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService
    {
        void ProductAdd(Product product);
        void ProductDelete(Product product);
        void ProductDeleteAdmin(Product product);
        void ProductUpdate(Product product);
        List<Product> GetList();
        Product GetByID(int ID);
        
    }
}

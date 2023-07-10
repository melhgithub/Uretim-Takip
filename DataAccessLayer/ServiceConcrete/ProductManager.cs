using DataAccessLayer.Abstract;
using DataAccessLayer.ServiceAbstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceConcrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Product GetByID(int ID)
        {
            return _productDal.GetByID(ID);
        }

        public List<Product> GetList()
        {
            return _productDal.GetListAll();
        }

        public List<Product> GetListWithIncludes()
        {
            return _productDal.GetListWithIncludes();
        }

        public async Task<List<Product>> GetListWithIncludesAsync()
        {
            return await _productDal.GetListWithIncludesAsync();
        }

        public void ProductAdd(Product product)
        {
            _productDal.Insert(product);
        }

        public void ProductDelete(Product product)
        {
            product.Status = (ProductStatuses)2;
            _productDal.Update(product);
        }

        public void ProductDeleteAdmin(Product product)
        {
            product.Status = (ProductStatuses)2;
            _productDal.Update(product);
        }

        public void ProductUpdate(Product product)
        {
            _productDal.Update(product);
        }
    }
}

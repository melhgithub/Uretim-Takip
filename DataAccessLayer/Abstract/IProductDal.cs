﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        List<Product> GetListWithIncludes();
        Task<List<Product>> GetListWithIncludesAsync();
        Task<List<Product>> GetListByCompanyID(int companyID);
    }
}

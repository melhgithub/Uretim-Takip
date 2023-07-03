﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductionService : IProductionService
    {
        IProductionDal _productionDal;

        public ProductionService(IProductionDal productionDal)
        {
            _productionDal = productionDal;
        }

        public Production GetByID(int ID)
        {
            return _productionDal.GetByID(ID);
        }

        public List<Production> GetList()
        {
            return _productionDal.GetListAll();
        }

        public void ProductionAdd(Production production)
        {
            _productionDal.Insert(production);
        }

        public void ProductionDelete(Production production)
        {
            production.Status = (ProductionStatuses)3;
            _productionDal.Update(production);
        }

        public void ProductionDeleteAdmin(Production production)
        {
            production.Status = (ProductionStatuses)2;
            _productionDal.Update(production);
        }

        public void ProductionUpdate(Production production)
        {
            _productionDal.Update(production);
        }
    }
}

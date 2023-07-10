using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.ServiceAbstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceConcrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }

        public Category GetByID(int ID)
        {
            return _categoryDal.GetByID(ID);
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetListAll();
        }
        public List<string> GetCategoryNames()
        {
            return _categoryDal.GetListAll().Select(c => c.Name).ToList();
        }

        public int GetCategoryIdByName(string categoryName)
        {
            var category = _categoryDal.GetByName(categoryName);
            return category != null ? category.ID : 0;
        }

        public async Task<List<Category>> GetListAsync()
        {
            return await _categoryDal.GetListAsync();
        }
    }
}

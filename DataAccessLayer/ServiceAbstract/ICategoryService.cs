using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ServiceAbstract
{
    public interface ICategoryService
    {
        void CategoryAdd(Category category);
        void CategoryDelete(Category category);
        void CategoryUpdate(Category category);
        List<Category> GetList();
        List<string> GetCategoryNames();
        Category GetByID(int ID);
        int GetCategoryIdByName(string categoryName);
        Task<List<Category>> GetListAsync();
    }
}

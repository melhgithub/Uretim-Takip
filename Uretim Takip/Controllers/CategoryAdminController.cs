using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Controllers
{
    public class CategoryAdminController : Controller
    {
        Context context = new Context();
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());

        private readonly Context _context;

        public CategoryAdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(CategoryFilterDto filter)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name)) categories = categories.Where(p => p.Name.Contains(filter.Name));
            if (filter.Status > 0) categories = categories.Where(p => p.Status.Equals(filter.Status));

            return View((categories.ToList(), filter));
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryAddDto category)
        {
            try
            {
                var categoryToAdd = new Category
                {
                    Name = category.Name,
                    Status = category.Status,
                };
                categoryManager.CategoryAdd(categoryToAdd);
            }
            catch (Exception)
            {
                TempData["Message"] = "Kategori eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Kategori başarıyla kaydedildi!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryEditDto category)
        {
            try
            {
                var categoryToUpdate = _context.Categories.FirstOrDefault(p => p.ID == category.ID);

                if (categoryToUpdate != null)
                {
                    categoryToUpdate.ID = category.ID;
                    categoryToUpdate.Name = category.Name;
                    categoryToUpdate.Status = category.Status;
                    categoryManager.CategoryUpdate(categoryToUpdate);

                    TempData["Message"] = "Kategori başarıyla güncellendi!";
                }
                else
                {
                    TempData["Message"] = "Güncellenmek istenen kategori bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Kategori güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCategory(CategoryDeleteDto category)
        {
            try
            {
                var categoryToDelete = _context.Categories.FirstOrDefault(p => p.ID == category.ID);

                if (categoryToDelete != null)
                {
                    categoryToDelete.Status = (CategoryStatuses)2;
                    categoryManager.CategoryUpdate(categoryToDelete);

                    TempData["Message"] = "Kategori başarıyla kaldırıldı!";
                }
                else
                {
                    TempData["Message"] = "Kaldırılmak istenen kategori bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Kategori kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ApproveCategory(CategoryApproveDto category)
        {
            try
            {
                var categoryToApprove = _context.Categories.FirstOrDefault(p => p.ID == category.ID);

                if (categoryToApprove != null)
                {
                    categoryToApprove.Status = (CategoryStatuses)1;
                    categoryManager.CategoryUpdate(categoryToApprove);

                    TempData["Message"] = "Kategori başarıyla onaylandı!";
                }
                else
                {
                    TempData["Message"] = "Onaylanmak istenen kategori bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Kategori onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

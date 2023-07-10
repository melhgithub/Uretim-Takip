using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.ServiceConcrete;
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
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());

        public IActionResult Index(CategoryFilterDto filter)
        {
            var categories = categoryManager.GetList();

            if (!string.IsNullOrEmpty(filter.Name)) { categories = categories.Where(p => p.Name.Contains(filter.Name)).ToList(); }

            if (filter.Status > 0) { categories = categories.Where(p => p.Status.Equals(filter.Status)).ToList(); }

            return View((categories.ToList(), filter));
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryAddDto category)
        {
            if(category != null)
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
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryEditDto category)
        {

            if (category != null)
            {
                try
                {
                    var categoryToUpdate = categoryManager.GetByID(category.ID);

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
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCategory(CategoryDeleteDto category)
        {
            if (category != null)
            {
                try
                {
                    var categoryToDelete = categoryManager.GetByID(category.ID);

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
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ApproveCategory(CategoryApproveDto category)
        {
            if (category != null)
            {
                try
                {
                    var categoryToApprove = categoryManager.GetByID(category.ID);

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
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}



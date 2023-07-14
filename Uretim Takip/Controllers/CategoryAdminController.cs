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
using Uretim_Takip.Models;

namespace Uretim_Takip.Controllers
{
    public class CategoryAdminController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());



        public IActionResult Index()
        {

            var categories = categoryManager.GetList();

            var filter = new CategoryFilterDto();

            var model = new CategoriesViewModel
            {
                Categories = categories,
                FilterDto = filter
            };


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryData()
        {

            var categories = await this.categoryManager.GetListAsync();

            var categoryData = categories.Select(p => new
            {
                ID = p.ID,
                Name = p.Name,
                Status = p.Status,
            });

            return Json(categoryData);
        }


        [HttpPost]
        public IActionResult AddCategory(CategoryAddDto category)
        {
            string message = "";

            if (category != null)
            {
                try
                {
                    var categoryToAdd = new Category
                    {
                        Name = category.Name,
                        Status = category.Status,
                    };
                    categoryManager.CategoryAdd(categoryToAdd);
                    message = "Kategori başarıyla kaydedildi!";
                }
                catch (Exception)
                {
                    message = "Kategori eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                    return RedirectToAction(nameof(Index));
                }
                
            }
            else
            {
                message = "Hata oluştu!";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryEditDto category)
        {
            string message = "";

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

                        message = "Kategori başarıyla güncellendi!";
                    }
                    else
                    {
                        message = "Güncellenmek istenen kategori bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message = "Kategori güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }


            return Json(message);
        }

        [HttpPost]
        public IActionResult DeleteCategory(CategoryDeleteDto category)
        {
            string message = "";

            if (category != null)
            {
                try
                {
                    var categoryToDelete = categoryManager.GetByID(category.ID);

                    if (categoryToDelete != null)
                    {
                        categoryToDelete.Status = (CategoryStatuses)2;
                        categoryManager.CategoryUpdate(categoryToDelete);

                        message =  "Kategori başarıyla kaldırıldı!";
                    }
                    else
                    {
                        message =  "Kaldırılmak istenen kategori bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Kategori kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }


            return Json(message);
        }

        [HttpPost]
        public IActionResult ApproveCategory(CategoryApproveDto category)
        {
            string message = "";

            if (category != null)
            {
                try
                {
                    var categoryToApprove = categoryManager.GetByID(category.ID);

                    if (categoryToApprove != null)
                    {
                        categoryToApprove.Status = (CategoryStatuses)1;
                        categoryManager.CategoryUpdate(categoryToApprove);

                        message =  "Kategori başarıyla onaylandı!";
                    }
                    else
                    {
                        message =  "Onaylanmak istenen kategori bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Kategori onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }

            return Json(message);
        }

    }
}



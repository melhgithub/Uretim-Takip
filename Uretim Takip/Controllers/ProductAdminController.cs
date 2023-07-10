using CoreLayer.Extensions;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.ServiceConcrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;
using Uretim_Takip.Models;

namespace Uretim_Takip.Controllers
{
    public class ProductAdminController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());

        public IActionResult Index()
        {
            var products = productManager.GetListWithIncludes();

            var filter = new ProductFilterDto
            {
                Categories = categoryManager.GetCategoryNames(),
                Companies = companyManager.GetCompanyNames()
            };

            var model = new ProductsViewModel
            {
                Products = products,
                FilterDto = filter
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductData()
        {

            var products = await this.productManager.GetListWithIncludesAsync();

            var productData = products.Select(p => new
            {
                ID = p.ID,
                Category = new { p.Category.ID, Name = p.Category.Name },
                Company = new { p.Company.ID, Name = p.Company.Name },
                Name = p.Name,
                Material = p.Material,
                Price = p.Price,
                Piece = p.Piece,
                Status = p.Status,
                Description = p.Description
            });

            return Json(productData);
        }



        [HttpPost]
        public IActionResult AddProduct(ProductAddDto product)
        {
            string message = "";

            if (product != null)
            {
                try
                {
                    int categoryID = categoryManager.GetCategoryIdByName(product.CategoryName);
                    int companyID = companyManager.GetCompanyIdByName(product.CompanyName);
                    var productToAdd = new Product
                    {
                        CategoryID = categoryID,
                        CompanyID = companyID,
                        Material = product.Material,
                        Name = product.Name,
                        Piece = product.Piece.ToDecimal(),
                        Price = product.Price.ToDecimal(),
                        Status = product.Status,
                        Description = product.Description
                    };
                    productManager.ProductAdd(productToAdd);

                    message = "Ürün başarıyla kaydedildi!";
                }
                catch (Exception)
                {
                    message = "Ürün eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

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
        public IActionResult EditProduct(ProductEditDto product)
        {
            string message = "";

            if (product != null)
            {
                try
                {

                    int categoryID = categoryManager.GetCategoryIdByName(product.CategoryName);
                    int companyID = companyManager.GetCompanyIdByName(product.CompanyName);

                    var productToUpdate = productManager.GetByID(product.ID);

                    if (productToUpdate != null)
                    {
                        productToUpdate.ID = product.ID;
                        productToUpdate.CategoryID = categoryID;
                        productToUpdate.CompanyID = companyID;
                        productToUpdate.Material = product.Material;
                        productToUpdate.Name = product.Name;
                        productToUpdate.Piece = product.Piece.ToDecimal();
                        productToUpdate.Price = product.Price.ToDecimal();
                        productToUpdate.Status = product.Status;
                        productToUpdate.Description = product.Description;

                        productManager.ProductUpdate(productToUpdate);

                        message = "Ürün başarıyla güncellendi!";
                    }
                    else
                    {
                        message = "Güncellenmek istenen ürün bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message = "Ürün güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message = "Hata oluştu!";
            }
            return Json(message);
        }

        [HttpPost]
        public IActionResult ApproveProduct(ProductApproveDto product)
        {
            string message = "";

            if (product != null)
            {
                try
                {
                    var productToApprove = productManager.GetByID(product.ID);

                    if (productToApprove != null)
                    {
                        productToApprove.Status = (ProductStatuses)1;
                        productManager.ProductUpdate(productToApprove);

                        message = "Ürün başarıyla onaylandı!";
                    }
                    else
                    {
                        message = "Onaylanmak istenen ürün bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message = "Ürün onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message = "Hata oluştu!";
            }

            return Json(message);
        }


        [HttpPost]
        public IActionResult DeleteProduct(ProductDeleteDto product)
        {
            string message = "";

            if (product != null)
            {
                try
                {
                    var productToDelete = productManager.GetByID(product.ID);

                    if (productToDelete != null)
                    {
                        productToDelete.Status = (ProductStatuses)2;
                        productManager.ProductUpdate(productToDelete);

                        message = "Ürün başarıyla kaldırıldı!";
                    }
                    else
                    {
                        message = "Kaldırılmak istenen ürün bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message = "Ürün kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message = "Hata oluştu!";
            }


            return Json(message);
        }

    }
}

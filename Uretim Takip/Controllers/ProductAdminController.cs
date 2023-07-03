using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreLayer.Extensions;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        Context context = new Context();
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());

        private readonly Context _context;

        public ProductAdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(ProductFilterDto filter)
        {
            return View(filter);
        }

        public IActionResult GetProducts(ProductFilterDto filter)
        {
            var products = _context.Products
               .Include(p => p.Category)
               .Include(p => p.Company)
               .AsQueryable();

            filter.Categories = _context.Categories.Select(c => c.Name).ToList();
            filter.Companies = _context.Companies.Select(c => c.Name).ToList();

            return View((products.ToList(), filter));
        }

        public IActionResult Filter(ProductFilterDto filter)
        {
            var products = _context.Products
               .Include(p => p.Category)
               .Include(p => p.Company)
               .AsQueryable();

            filter.Categories = _context.Categories.Select(c => c.Name).ToList();
            filter.Companies = _context.Companies.Select(c => c.Name).ToList();

            if (!string.IsNullOrEmpty(filter.Name)) products = products.Where(p => p.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.Material)) products = products.Where(p => p.Material.Contains(filter.Material));
            if (filter.Price > 0) products = products.Where(p => p.Price == filter.Price);
            if (filter.Piece > 0) products = products.Where(p => p.Piece == filter.Piece);
            if (filter.Status > 0) products = products.Where(p => p.Status.Equals(filter.Status));
            if (!string.IsNullOrEmpty(filter.Description)) products = products.Where(p => p.Description.Contains(filter.Description));
            if (!string.IsNullOrEmpty(filter.CategoryName)) products = products.Where(p => p.Category.Name.Contains(filter.CategoryName));
            if (!string.IsNullOrEmpty(filter.CompanyName)) products = products.Where(p => p.Company.Equals(_context.Companies.FirstOrDefault(x => x.Name == filter.CompanyName)));
        
            return Json((products.ToList(),filter));
        }

        [HttpPost]
        public IActionResult AddProduct(ProductAddDto product)
        {
            try
            {
                var productToAdd = new Product
                {
                    CategoryID = _context.Categories.FirstOrDefault(c => c.Name == product.CategoryName).ID,
                    CompanyID = _context.Companies.FirstOrDefault(c => c.Name == product.CompanyName).ID,
                    Material = product.Material,
                    Name = product.Name,
                    Piece = product.Piece.ToDecimal(),
                    Price = product.Price.ToDecimal(),
                    Status = product.Status,
                    Description = product.Description
                };
                productManager.ProductAdd(productToAdd);
            }
            catch (Exception)
            {
                TempData["Message"] = "Ürün eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Ürün başarıyla kaydedildi!";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult EditProduct(ProductEditDto product)
        {
            try
            {
                var productToUpdate = _context.Products.FirstOrDefault(p => p.ID == product.ID);

                if (productToUpdate != null)
                {
                    productToUpdate.ID = product.ID;
                    productToUpdate.CategoryID = _context.Categories.FirstOrDefault(c => c.Name == product.CategoryName).ID;
                    productToUpdate.CompanyID = _context.Companies.FirstOrDefault(c => c.Name == product.CompanyName).ID;
                    productToUpdate.Material = product.Material;
                    productToUpdate.Name = product.Name;
                    productToUpdate.Piece = product.Piece.ToDecimal();
                    productToUpdate.Price = product.Price.ToDecimal();
                    productToUpdate.Status = product.Status;
                    productToUpdate.Description = product.Description;

                    productManager.ProductUpdate(productToUpdate);

                    TempData["Message"] = "Ürün başarıyla güncellendi!";
                }
                else
                {
                    TempData["Message"] = "Güncellenmek istenen ürün bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Ürün güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ApproveProduct(ProductApproveDto product)
        {
            try
            {
                var productToApprove = _context.Products.FirstOrDefault(p => p.ID == product.ID);

                if (productToApprove != null)
                {
                    productToApprove.Status = (ProductStatuses)1;
                    productManager.ProductUpdate(productToApprove);

                    TempData["Message"] = "Ürün başarıyla onaylandı!";
                }
                else
                {
                    TempData["Message"] = "Onaylanmak istenen ürün bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Ürün onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteProduct(ProductDeleteDto product)
        {
            try
            {
                var productToDelete = _context.Products.FirstOrDefault(p => p.ID == product.ID);

                if (productToDelete != null)
                {
                    productToDelete.Status = (ProductStatuses)2;
                    productManager.ProductUpdate(productToDelete);

                    TempData["Message"] = "Ürün başarıyla kaldırıldı!";
                }
                else
                {
                    TempData["Message"] = "Kaldırılmak istenen ürün bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Ürün kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

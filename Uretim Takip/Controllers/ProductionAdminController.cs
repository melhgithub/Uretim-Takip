using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.ServiceConcrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Controllers
{
    public class ProductionAdminController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());
        OfferManager offerManager = new OfferManager(new EFOfferRepository());
        OrderManager orderManager = new OrderManager(new EFOrderRepository());
        ProductionManager productionManager = new ProductionManager(new EFProductionRepository());

        Context context = new Context();
        private Context _context;

        public ProductionAdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(ProductionFilterDto filter)
        {
            var productions = _context.Productions
               .Include(p => p.Customer)
               .Include(p => p.Offer)
               .Include(p => p.Company)
               .AsQueryable();

            filter.Customers = _context.Customers.Select(c => c.Name).ToList();
            filter.Companies = _context.Companies.Select(c => c.Name).ToList();

            if (filter.Price > 0) productions = productions.Where(p => p.Price == filter.Price);
            if (filter.ProductPiece > 0) productions = productions.Where(p => p.ProductPiece == filter.ProductPiece);
            if (filter.Status > 0) productions = productions.Where(p => p.Status.Equals(filter.Status));
            if (!string.IsNullOrEmpty(filter.CustomerName)) productions = productions.Where(p => p.Customer.Equals(_context.Customers.FirstOrDefault(x => x.Name == filter.CustomerName)));
            if (!string.IsNullOrEmpty(filter.CompanyName)) productions = productions.Where(p => p.Company.Equals(_context.Companies.FirstOrDefault(x => x.Name == filter.CompanyName)));

            return View((productions.ToList(), filter));
        }


        [HttpPost]
        public IActionResult ApproveProduction(ProductionApproveDto production)
        {
            try
            {
                var productionToApprove = _context.Productions.FirstOrDefault(p => p.ID == production.ID);

                if (productionToApprove != null)
                {
                    productionToApprove.Status = (ProductionStatuses)1;
                    productionManager.ProductionUpdate(productionToApprove);

                    TempData["Message"] = "Üretim başarıyla onaylandı!";
                }
                else
                {
                    TempData["Message"] = "Onaylanmak istenen üretim bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Üretim onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteProduction(ProductionDeleteDto production)
        {
            try
            {
                var productionToDelete = _context.Productions.FirstOrDefault(p => p.ID == production.ID);

                if (productionToDelete != null)
                {
                    productionToDelete.Status = (ProductionStatuses)3;
                    productionManager.ProductionUpdate(productionToDelete);

                    TempData["Message"] = "Üretim başarıyla reddedildi!";
                }
                else
                {
                    TempData["Message"] = "Reddedilmek istenen üretim bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Üretim reddedilirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult FinishProduction(ProductionFinishDto production)
        {
            try
            {
                var productionToFinish = _context.Productions.FirstOrDefault(p => p.ID == production.ID);

                if (productionToFinish != null)
                {
                    productionToFinish.Status = (ProductionStatuses)4;
                    productionManager.ProductionUpdate(productionToFinish);

                    TempData["Message"] = "Üretim başarıyla sonlandı!";
                }
                else
                {
                    TempData["Message"] = "Sonlanacak üretim bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Üretim sonlandırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

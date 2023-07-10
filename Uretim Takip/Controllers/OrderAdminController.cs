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
    public class OrderAdminController : Controller
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

        public OrderAdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(OrderFilterDto filter)
        {
            var orders = _context.Orders
               .Include(p => p.Customer)
               .Include(p => p.Offer)
               .Include(p => p.Company)
               .AsQueryable();

            filter.Customers = _context.Customers.Select(c => c.Name).ToList();
            filter.Companies = _context.Companies.Select(c => c.Name).ToList();

            if (filter.Price > 0) orders = orders.Where(p => p.Price == filter.Price);
            if (filter.ProductPiece > 0) orders = orders.Where(p => p.ProductPiece == filter.ProductPiece);
            if (filter.Status > 0) orders = orders.Where(p => p.Status.Equals(filter.Status));
            if (!string.IsNullOrEmpty(filter.CustomerName)) orders = orders.Where(p => p.Customer.Equals(_context.Customers.FirstOrDefault(x => x.Name == filter.CustomerName)));
            if (!string.IsNullOrEmpty(filter.CompanyName)) orders = orders.Where(p => p.Company.Equals(_context.Companies.FirstOrDefault(x => x.Name == filter.CompanyName)));

            return View((orders.ToList(), filter));
        }


        [HttpPost]
        public IActionResult ApproveOrder(OrderApproveDto order)
        {
            try
            {
                var orderToApprove = _context.Orders.FirstOrDefault(p => p.ID == order.ID);

                if (orderToApprove != null)
                {
                    orderToApprove.Status = (OrderStatuses)1;
                    orderManager.OrderUpdate(orderToApprove);

                    TempData["Message"] = "Sipariş başarıyla onaylandı!";
                }
                else
                {
                    TempData["Message"] = "Onaylanmak istenen sipariş bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Sipariş onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteOrder(OrderDeleteDto order)
        {
            try
            {
                var orderToDelete = _context.Orders.FirstOrDefault(p => p.ID == order.ID);

                if (orderToDelete != null)
                {
                    orderToDelete.Status = (OrderStatuses)3;
                    orderManager.OrderUpdate(orderToDelete);

                    TempData["Message"] = "Sipariş başarıyla reddedildi!";
                }
                else
                {
                    TempData["Message"] = "Reddedilmek istenen sipariş bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Sipariş reddedilirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult GoToProduction(OrderToProductionDto order)
        {
            try
            {
                var orderToProduction = _context.Orders.FirstOrDefault(p => p.ID == order.ID);

                if (orderToProduction != null)
                {
                    orderToProduction.Status = (OrderStatuses)4;
                    orderManager.OrderUpdate(orderToProduction);

                    var productionToAdd = new Production
                    {
                        CompanyID = orderToProduction.CompanyID,
                        CustomerID = orderToProduction.CustomerID,
                        Status = (ProductionStatuses)1,
                        Price = orderToProduction.Price,
                        ProductPiece = orderToProduction.ProductPiece,
                        OfferID =orderToProduction.OfferID
                    };
                    productionManager.ProductionAdd(productionToAdd);

                    TempData["Message"] = "Sipariş başarıyla üretime geçti!";
                }
                else
                {
                    TempData["Message"] = "Üretime geçecek sipariş bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Sipariş üretime geçerken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

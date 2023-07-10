using CoreLayer.Extensions;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.ServiceConcrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Controllers
{
    public class OfferAdminController : Controller
    {
        
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());
        OfferManager offerManager = new OfferManager(new EFOfferRepository());
        OfferDetailManager offerDetailManager = new OfferDetailManager(new EFOfferDetailRepository());
        OrderManager orderManager = new OrderManager(new EFOrderRepository());
        ProductionManager productionManager = new ProductionManager(new EFProductionRepository());

        Context context = new Context();
        private Context _context;

        public OfferAdminController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCompany(string companyName)
        {
            var products = await _context.Products
                .Where(p => p.Company.Name == companyName)
                .Select(p => new { ID = p.ID, Name = p.Name })
                .ToListAsync();

            return Json(products);
        }

        [HttpGet]
        public IActionResult GetOfferDetails(int offerId)
        {
            var offerDetails = _context.OfferDetails
                 .Where(od => od.OfferID == offerId)
                 .ToArray();

            var json = JsonConvert.SerializeObject(offerDetails);

            return Json(json);
        }

        public IActionResult Index(OfferFilterDto filter)
        {
            var offers = _context.Offers
               .Include(p => p.Customer)
               .Include(p => p.Company)
               .AsQueryable();

            filter.Customers = _context.Customers.Select(c => c.Name).ToList();
            filter.Companies = _context.Companies.Select(c => c.Name).ToList();
            filter.Products = _context.Products.Select(c => c.Name).ToList();

            if (filter.Price > 0) offers = offers.Where(p => p.Price == filter.Price);
            if (filter.ProductPiece > 0) offers = offers.Where(p => p.ProductPiece == filter.ProductPiece);
            if (filter.Status > 0) offers = offers.Where(p => p.Status.Equals(filter.Status));
            if (!string.IsNullOrEmpty(filter.CustomerName)) offers = offers.Where(p => p.Customer.Equals(_context.Customers.FirstOrDefault(x => x.Name == filter.CustomerName)));
            if (!string.IsNullOrEmpty(filter.CompanyName)) offers = offers.Where(p => p.Company.Equals(_context.Companies.FirstOrDefault(x => x.Name == filter.CompanyName)));

            return View((offers.ToList(), filter));
        }

        [HttpPost]
        public IActionResult AddOffer(Dictionary<string, string> form, OfferAddDto offer, int Adet)
        {
            try
            {
                var offerToAdd = new Offer
                {
                    CompanyID = _context.Companies.FirstOrDefault(c => c.Name == offer.CompanyName).ID,
                    CustomerID = _context.Customers.FirstOrDefault(c => c.Name == offer.CustomerName).ID,
                    Status = (OfferStatuses)5,
                    Price = offer.Price.ToDecimal(),
                    ProductPiece = Adet
                };
                offerManager.OfferAdd(offerToAdd);

                for (int i = 1; i <= Adet; i++)
                {
                    var productIDString = form[$"Product{i}"];
                    var productPriceString = form[$"ProductPrice{i}"];
                    var productPieceString = form[$"ProductPiece{i}"];

                    var productID = int.Parse(productIDString);
                    var productPrice = decimal.Parse(productPriceString);
                    var productPiece = int.Parse(productPieceString);

                    var offerDetailToAdd = new OfferDetail
                    {
                        OfferID = offerToAdd.ID,
                        ProductID = productID,
                        Price = productPrice,
                        Piece = productPiece
                    };
                    offerDetailManager.OfferDetailAdd(offerDetailToAdd);
                }

                TempData["Message"] = "Teklif başarıyla kaydedildi!";

            }
            catch (Exception)
            {
                TempData["Message"] = "Teklif eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditOffer(OfferEditDto offer)
        {
            try
            {
                var offerToUpdate = _context.Offers.FirstOrDefault(p => p.ID == offer.ID);

                if (offerToUpdate != null)
                {
                    offerToUpdate.ID = offer.ID;
                    offerToUpdate.CustomerID = _context.Customers.FirstOrDefault(c => c.Name == offer.CustomerName).ID;
                    offerToUpdate.CompanyID = _context.Companies.FirstOrDefault(c => c.Name == offer.CompanyName).ID;
                    offerToUpdate.Price = offer.Price.ToDecimal();
                    offerToUpdate.ProductPiece = offer.ProductPiece;
                    offerToUpdate.Status = offer.Status;

                    offerManager.OfferUpdate(offerToUpdate);

                    TempData["Message"] = "Teklif başarıyla güncellendi!";
                }
                else
                {
                    TempData["Message"] = "Güncellenmek istenen teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Teklif güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult ApproveOffer(OfferApproveDto offer)
        {
            try
            {
                var offerToApprove = _context.Offers.FirstOrDefault(p => p.ID == offer.ID);

                if (offerToApprove != null)
                {
                    offerToApprove.Status = (OfferStatuses)1;
                    offerManager.OfferUpdate(offerToApprove);

                    TempData["Message"] = "Teklif başarıyla onaylandı!";
                }
                else
                {
                    TempData["Message"] = "Onaylanmak istenen teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Teklif onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteOffer(OfferDeleteDto offer)
        {
            try
            {
                var offerToDelete = _context.Offers.FirstOrDefault(p => p.ID == offer.ID);

                if (offerToDelete != null)
                {
                    offerToDelete.Status = (OfferStatuses)3;
                    offerManager.OfferUpdate(offerToDelete);

                    TempData["Message"] = "Teklif başarıyla reddedildi!";
                }
                else
                {
                    TempData["Message"] = "Reddedilmek istenen teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Teklif reddedilirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult GoToOrder(OfferToOrderDto offer)
        {
            try
            {
                var offerToOrder = _context.Offers.FirstOrDefault(p => p.ID == offer.ID);

                if (offerToOrder != null)
                {
                    offerToOrder.Status = (OfferStatuses)4;
                    offerManager.OfferUpdate(offerToOrder);


                    var orderToAdd = new Order
                    {
                        CompanyID = offerToOrder.CompanyID,
                        CustomerID = offerToOrder.CustomerID,
                        Status = (OrderStatuses)1,
                        Price = offerToOrder.Price,
                        ProductPiece = offerToOrder.ProductPiece,
                        OfferID = offerToOrder.ID
                    };
                    orderManager.OrderAdd(orderToAdd);

                    TempData["Message"] = "Teklif başarıyla siparişe geçti!";
                }
                else
                {
                    TempData["Message"] = "Siparişe geçecek teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Teklif siparişe geçerken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

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
using Uretim_Takip.Models;

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


        public IActionResult Index()
        {
            var offers = offerManager.GetListWithIncludes();

            var filter = new OfferFilterDto
            {
                Customers = customerManager.GetCustomerNames(),
                Companies = companyManager.GetCompanyNames()
            };

            var model = new OffersViewModel
            {
                Offers = offers,
                FilterDto = filter
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetOfferData()
        {

            var offers = await this.offerManager.GetListWithIncludesAsync();

            var offerData = offers.Select(p => new
            {
                ID = p.ID,
                Customer = new { p.Customer.ID, Name = p.Customer.Name },
                Company = new { p.Company.ID, Name = p.Company.Name },
                Price = p.Price,
                productpiece = p.ProductPiece,
                Status = p.Status
            });

            return Json(offerData);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCompany(string companyName)
        {
            var companyID = companyManager.GetCompanyIdByName(companyName);
            var products = await productManager.GetListByCompanyID(companyID);

            var productData = products.Select(p => new
            {
                ID = p.ID,
                Name = p.Name
            });

            return Json(productData);
        }

        [HttpPost]
        public IActionResult AddOffer(Dictionary<string, string> form, OfferAddDto offer)
        {
            if (int.TryParse(form["adet"], out int adet) && adet != 0)
            {
                try
                {
                    var offerToAdd = new Offer
                    {
                        CompanyID = companyManager.GetCompanyIdByName(offer.CompanyName),
                        CustomerID = customerManager.GetCustomerIdByName(offer.CustomerName),
                        Status = (OfferStatuses)5,
                        Price = offer.Price.ToDecimal(),
                        ProductPiece = adet
                    };

                    for (int i = 1; i <= adet; i++)
                    {
                        if (form[$"Product{i}"] != null && int.TryParse(form[$"Product{i}"], out int productID) &&
                            decimal.TryParse(form[$"ProductPrice{i}"], out decimal productPrice) &&
                            int.TryParse(form[$"ProductPiece{i}"], out int productPiece))
                        {
                            var offerDetailToAdd = new OfferDetail
                            {
                                OfferID = offerToAdd.ID,
                                ProductID = productID,
                                Price = productPrice,
                                Piece = productPiece
                            };
                            offerDetailManager.OfferDetailAdd(offerDetailToAdd);
                        }
                        else
                        {
                            return Json("Teklif eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.");
                        }
                    }

                    offerManager.OfferAdd(offerToAdd);
                    return Json("Teklif başarıyla kaydedildi!");
                }
                catch (Exception)
                {
                    return Json("Teklif eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.");
                }
            }
            else
            {
                return Json("Hiç ürün eklemediniz!");
            }
        }

        [HttpPost]
        public IActionResult EditOffer(OfferEditDto offer)
        {
            string message = "";
            try
            {
                var offerToEdit = offerManager.GetByID(offer.ID);

                if (offerToEdit != null)
                {
                    offerToEdit.CustomerID = customerManager.GetCustomerIdByName(offer.CustomerName);
                    offerToEdit.CompanyID = companyManager.GetCompanyIdByName(offer.CompanyName);
                    offerToEdit.Price = offer.Price.ToDecimal();
                    offerToEdit.ProductPiece = offer.ProductPiece;
                    offerToEdit.Status = (OfferStatuses)offer.Status;

                    offerManager.OfferUpdate(offerToEdit);

                    message = "Teklif başarıyla güncellendi!";
                }
                else
                {
                    message = "Güncellenmek istenen teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                message = "Teklif güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return Json(message);
        }


        [HttpPost]
        public IActionResult ApproveOffer(OfferApproveDto offer)
        {
            string message = "";
            try
            {
                var offerToApprove = offerManager.GetByID(offer.ID);

                if (offerToApprove != null)
                {
                    offerToApprove.Status = (OfferStatuses)1;
                    offerManager.OfferUpdate(offerToApprove);

                    message ="Teklif başarıyla onaylandı!";
                }
                else
                {
                    message ="Onaylanmak istenen teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                message ="Teklif onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult DeleteOffer(OfferDeleteDto offer)
        {
            string message = "";
            try
            {
                var offerToDelete = offerManager.GetByID(offer.ID);

                if (offerToDelete != null)
                {
                    offerToDelete.Status = (OfferStatuses)3;
                    offerManager.OfferUpdate(offerToDelete);

                    message ="Teklif başarıyla reddedildi!";
                }
                else
                {
                    message ="Reddedilmek istenen teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                message ="Teklif reddedilirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult GoToOrder(OfferToOrderDto offer)
        {
            string message = "";
            try
            {
                var offerToOrder = offerManager.GetByID(offer.ID);

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

                    message ="Teklif başarıyla siparişe geçti!";
                }
                else
                {
                    message ="Siparişe geçecek teklif bulunamadı!";
                }
            }
            catch (Exception)
            {
                message ="Teklif siparişe geçerken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return Json(message);
        }
    }
}

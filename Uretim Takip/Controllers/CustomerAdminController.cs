using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.ServiceConcrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uretim_Takip.Dtos;

namespace Uretim_Takip.Controllers
{
    public class CustomerAdminController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());


        public IActionResult Index(CustomerFilterDto filter)
        {
            var customers = customerManager.GetList().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                customers = customers.Where(p => p.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                customers = customers.Where(p => p.LastName.Contains(filter.LastName));
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                customers = customers.Where(p => p.Description.Contains(filter.Description));
            }

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                customers = customers.Where(p => p.PhoneNumber.Contains(filter.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(filter.Mail))
            {
                customers = customers.Where(p => p.Mail.Contains(filter.Mail));
            }

            if (filter.Status > 0)
            {
                customers = customers.Where(p => p.Status.Equals(filter.Status));
            }


            return View((customers.ToList(), filter));
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerAddDto customer)
        {
            if (customer != null)
            {
                try
                {
                    var customerToAdd = new Customer
                    {
                        Name = customer.Name,
                        LastName = customer.LastName,
                        Description = customer.Description,
                        PhoneNumber = customer.PhoneNumber,
                        Password = customer.Password,
                        Mail = customer.Mail,
                        Status = customer.Status,
                    };
                    customerManager.CustomerAdd(customerToAdd);
                    TempData["Message"] = "Müşteri başarıyla kaydedildi!";
                }
                catch (Exception)
                {
                    TempData["Message"] = "Müşteri eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }

            

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditCustomer(CustomerEditDto customer)
        {
            if (customer != null)
            {
                try
                {
                    var customerToUpdate = customerManager.GetByID(customer.ID);

                    if (customerToUpdate != null)
                    {
                        customerToUpdate.ID = customer.ID;
                        customerToUpdate.Name = customer.Name;
                        customerToUpdate.LastName = customer.LastName;
                        customerToUpdate.Mail = customer.Mail;
                        customerToUpdate.Description = customer.Description;
                        customerToUpdate.PhoneNumber = customer.PhoneNumber;
                        customerToUpdate.Password = customer.Password;
                        customerToUpdate.Status = customer.Status;

                        customerManager.CustomerUpdate(customerToUpdate);

                        TempData["Message"] = "Müşteri başarıyla güncellendi!";
                    }
                    else
                    {
                        TempData["Message"] = "Güncellenmek istenen müşteri bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Müşteri güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            if (customer != null)
            {
                try
                {
                    var customerToDelete = customerManager.GetByID(customer.ID);

                    if (customerToDelete != null)
                    {
                        customerToDelete.Status = (CustomerStatuses)2;
                        customerManager.CustomerUpdate(customerToDelete);

                        TempData["Message"] = "Müşteri başarıyla kaldırıldı!";
                    }
                    else
                    {
                        TempData["Message"] = "Kaldırılmak istenen müşteri bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Müşteri kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                TempData["Message"] = "Hata oluştu!";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ApproveCustomer(CustomerApproveDto customer)
        {
            if (customer != null)
            {
                try
                {
                    var customerToApprove = customerManager.GetByID(customer.ID);

                    if (customerToApprove != null)
                    {
                        customerToApprove.Status = (CustomerStatuses)1;
                        customerManager.CustomerUpdate(customerToApprove);

                        TempData["Message"] = "Müşteri başarıyla onaylandı!";
                    }
                    else
                    {
                        TempData["Message"] = "Onaylanmak istenen müşteri bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "Müşteri onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
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

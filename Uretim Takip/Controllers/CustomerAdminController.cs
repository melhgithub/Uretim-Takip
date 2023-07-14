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
using Uretim_Takip.Models;

namespace Uretim_Takip.Controllers
{
    public class CustomerAdminController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());


        public IActionResult Index()
        {
            var customers = customerManager.GetList();

            var filter = new CustomerFilterDto();

            var model = new CustomersViewModel
            {
                Customers = customers,
                FilterDto = filter
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerData()
        {

            var customers = await this.customerManager.GetListAsync();

            var customerData = customers.Select(p => new
            {
                ID = p.ID,
                Name = p.Name,
                lastname = p.LastName,
                phonenumber = p.PhoneNumber,
                Mail = p.Mail,
                Status = p.Status,
                Description = p.Description,
                Password = p.Password
            });

            return Json(customerData);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerAddDto customer)
        {
            string message = "";
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
                    message =  "Müşteri başarıyla kaydedildi!";
                }
                catch (Exception)
                {
                    message =  "Müşteri eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                    return Json(message);
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }

            

            return Json(message);
        }

        [HttpPost]
        public IActionResult EditCustomer(CustomerEditDto customer)
        {
            string message = "";
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

                        message =  "Müşteri başarıyla güncellendi!";
                    }
                    else
                    {
                        message =  "Güncellenmek istenen müşteri bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Müşteri güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            string message = "";
            if (customer != null)
            {
                try
                {
                    var customerToDelete = customerManager.GetByID(customer.ID);

                    if (customerToDelete != null)
                    {
                        customerToDelete.Status = (CustomerStatuses)2;
                        customerManager.CustomerUpdate(customerToDelete);

                        message =  "Müşteri başarıyla kaldırıldı!";
                    }
                    else
                    {
                        message =  "Kaldırılmak istenen müşteri bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Müşteri kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult ApproveCustomer(CustomerApproveDto customer)
        {
            string message = "";
            if (customer != null)
            {
                try
                {
                    var customerToApprove = customerManager.GetByID(customer.ID);

                    if (customerToApprove != null)
                    {
                        customerToApprove.Status = (CustomerStatuses)1;
                        customerManager.CustomerUpdate(customerToApprove);

                        message =  "Müşteri başarıyla onaylandı!";
                    }
                    else
                    {
                        message =  "Onaylanmak istenen müşteri bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Müşteri onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
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

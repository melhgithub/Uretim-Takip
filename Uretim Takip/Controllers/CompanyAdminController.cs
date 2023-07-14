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
using Uretim_Takip.Models;

namespace Uretim_Takip.Controllers
{
    public class CompanyAdminController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());

        public IActionResult Index()
        {
            var companies = companyManager.GetList();

            var filter = new CompanyFilterDto();

            var model = new CompaniesViewModel
            {
                Companies = companies,
                FilterDto = filter
            };


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyData()
        {

            var companies = await this.companyManager.GetListAsync();

            var companyData = companies.Select(p => new
            {
                ID = p.ID,
                Name = p.Name,
                phonenumber = p.PhoneNumber,
                Mail = p.Mail,
                Status = p.Status,
                Description = p.Description,
                Password = p.Password
            });

            return Json(companyData);
        }

        [HttpPost]
        public IActionResult AddCompany(CompanyAddDto company)
        {
            string message = "";
            try
            {
                var companyToAdd = new Company
                {
                    Name = company.Name,
                    Description = company.Description,
                    PhoneNumber = company.PhoneNumber,
                    Password = company.Password,
                    Mail = company.Mail,
                    Status = company.Status,
                };
                companyManager.CompanyAdd(companyToAdd);
            }
            catch (Exception)
            {
                message =  "Firma eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                return Json(message);
            }

            message =  "Firma başarıyla kaydedildi!";

            return Json(message);
        }

        [HttpPost]
        public IActionResult EditCompany(CompanyEditDto company)
        {
            string message = "";
            if (company != null)
            {
                try
                {
                    var companyToUpdate = companyManager.GetByID(company.ID);

                    if (companyToUpdate != null)
                    {
                        companyToUpdate.ID = company.ID;
                        companyToUpdate.Name = company.Name;
                        companyToUpdate.Mail = company.Mail;
                        companyToUpdate.Description = company.Description;
                        companyToUpdate.PhoneNumber = company.PhoneNumber;
                        companyToUpdate.Password = company.Password;
                        companyToUpdate.Status = company.Status;

                        companyManager.CompanyUpdate(companyToUpdate);

                        message =  "Firma başarıyla güncellendi!";
                    }
                    else
                    {
                        message =  "Güncellenmek istenen firma bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Firma güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult DeleteCompany(CompanyDeleteDto company)
        {
            string message = "";
            if (company != null)
            {
                try
                {
                    var companyToDelete = companyManager.GetByID(company.ID);

                    if (companyToDelete != null)
                    {
                        companyToDelete.Status = (CompanyStatuses)2;
                        companyManager.CompanyUpdate(companyToDelete);

                        message =  "Firma başarıyla kaldırıldı!";
                    }
                    else
                    {
                        message =  "Kaldırılmak istenen firma bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Firma kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
                }
            }
            else
            {
                message =  "Hata oluştu!";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult ApproveCompany(CompanyApproveDto company)
        {
            string message = "";
            if (company != null)
            {
                try
                {
                    var companyToApprove = companyManager.GetByID(company.ID);

                    if (companyToApprove != null)
                    {
                        companyToApprove.Status = (CompanyStatuses)1;
                        companyManager.CompanyUpdate(companyToApprove);

                        message =  "Firma başarıyla onaylandı!";
                    }
                    else
                    {
                        message =  "Onaylanmak istenen firma bulunamadı!";
                    }
                }
                catch (Exception)
                {
                    message =  "Firma onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
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

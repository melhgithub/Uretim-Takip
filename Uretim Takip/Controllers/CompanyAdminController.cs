using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
    public class CompanyAdminController : Controller
    {

        Context context = new Context();
        ProductManager productManager = new ProductManager(new EFProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CompanyManager companyManager = new CompanyManager(new EFCompanyRepository());
        CustomerManager customerManager = new CustomerManager(new EFCustomerRepository());

        private readonly Context _context;

        public CompanyAdminController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(CompanyFilterDto filter)
        {
            var companies = _context.Companies.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name)) companies = companies.Where(p => p.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.Description)) companies = companies.Where(p => p.Description.Contains(filter.Description));
            if (!string.IsNullOrEmpty(filter.PhoneNumber)) companies = companies.Where(p => p.PhoneNumber.Contains(filter.PhoneNumber));
            if (!string.IsNullOrEmpty(filter.Mail)) companies = companies.Where(p => p.Mail.Contains(filter.Mail));
            if (filter.Status > 0) companies = companies.Where(p => p.Status.Equals(filter.Status));


            return View((companies.ToList(), filter));
        }

        [HttpPost]
        public IActionResult AddCompany(CompanyAddDto company)
        {
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
                TempData["Message"] = "Firma eklenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";

                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "Firma başarıyla kaydedildi!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditCompany(CompanyEditDto company)
        {
            try
            {
                var companyToUpdate = _context.Companies.FirstOrDefault(p => p.ID == company.ID);

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

                    TempData["Message"] = "Firma başarıyla güncellendi!";
                }
                else
                {
                    TempData["Message"] = "Güncellenmek istenen firma bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Firma güncellenirken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCompany(CompanyDeleteDto company)
        {
            try
            {
                var companyToDelete = _context.Companies.FirstOrDefault(p => p.ID == company.ID);

                if (companyToDelete != null)
                {
                    companyToDelete.Status = (CompanyStatuses)2;
                    companyManager.CompanyUpdate(companyToDelete);

                    TempData["Message"] = "Firma başarıyla kaldırıldı!";
                }
                else
                {
                    TempData["Message"] = "Kaldırılmak istenen firma bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Firma kaldırılırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ApproveCompany(CompanyApproveDto company)
        {
            try
            {
                var companyToApprove = _context.Companies.FirstOrDefault(p => p.ID == company.ID);

                if (companyToApprove != null)
                {
                    companyToApprove.Status = (CompanyStatuses)1;
                    companyManager.CompanyUpdate(companyToApprove);

                    TempData["Message"] = "Firma başarıyla onaylandı!";
                }
                else
                {
                    TempData["Message"] = "Onaylanmak istenen firma bulunamadı!";
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Firma onaylanırken bir sorun oluştu! Lütfen bilgileri kontrol ediniz.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

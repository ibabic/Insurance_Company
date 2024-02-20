using Partners.DataAccess.Data;
using Partners.DataAccess.Models;
using Partners.DataAccess.Models.DTOs;
using Partners.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Partners.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerRepository _patnerData;
        private readonly IPolicyRepository _policyData;

        public PartnerController(IPartnerRepository patnerData, IPolicyRepository policyData)
        {
            _patnerData = patnerData;
            _policyData = policyData;
        }
        public ActionResult Index()
        {
            
            var b = _patnerData.GetAll();
            var c = b.GetAwaiter().GetResult().ToList().OrderByDescending(m => m.CreatedAtUtc);

            return View(c);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(PartnerSaveDto partner)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Failed to create partner. Please correct the errors and try again.";
                return View("Create");
            }
            var insert = _patnerData.Insert(partner);
            if (insert.IsCompleted)
            {
                TempData["success"] = "Partner created successfully";
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(PartnerSaveDto partner)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Failed to update partner. Please correct the errors and try again.";
                return RedirectToAction("Index");
            }
            var insert = _patnerData.Insert(partner);
            if (insert.IsCompleted)
            {
                TempData["success"] = "Partner updated successfully";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            var partner = _patnerData.Get(id).Result;
            if (partner == null)
            {
                return HttpNotFound();
            }
            _patnerData.SoftDelete(id);
            TempData["success"] = "Partner deleted successfully";
            return RedirectToAction("Index");
        }

        public ActionResult GetPartnerDetails(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            var partner = _patnerData.Get(id).Result;
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View("_PartnerDetails", partner);
        }

        public ActionResult UpdatePartner(PartnerUpdateDto partner)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("aaa");
            }
            _patnerData.Update(partner);
            //toaster
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            Console.WriteLine("Get one");

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
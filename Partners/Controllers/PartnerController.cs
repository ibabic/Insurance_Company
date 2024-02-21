using Partners.DataAccess.Data;
using Partners.DataAccess.Models.DTOs;
using Partners.ViewModel;
using Serilog;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Partners.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerRepository _patnerData;

        public PartnerController(IPartnerRepository patnerData)
        {
            _patnerData = patnerData;
        }
        public ActionResult Index(bool postCreated = false)
        {
            try
            {
                var partnersWithPolicies = _patnerData.GetPartnersWithPolicies()?.OrderByDescending(m => m.CreatedAtUtc);
                if (partnersWithPolicies == null || !partnersWithPolicies.Any())
                {
                    TempData["error"] = "No partners with policies found.";
                    return RedirectToAction("Index");
                }
                var partnerVM = new PartnerViewModel
                {
                    partners = partnersWithPolicies,
                    postCreated = postCreated
                };

                return View(partnerVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while loading partners with policies. Please try again later.";
                Log.Error($"Unexpected error loading partners with policies: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View("Create");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while loading the create policy page. Please try again later.";
                Log.Error($"Unexpected error loading the create policy page: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Create(PartnerSaveDto partner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Failed to create partner. Please correct the errors and try again.";
                    return View("Create", partner);
                }

                var insert = _patnerData.Insert(partner);

                if (insert.Exception != null)
                {
                    TempData["error"] = insert.Exception.InnerException.Message;
                    Log.Error($"Error creating partner: {insert.Exception.InnerException.Message}");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = "Partner created successfully";
                    return RedirectToAction("Index", new { postCreated = true });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while creating the partner. Please try again later.";
                Log.Error($"Unexpected error creating partner: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(PartnerUpdateDto partner)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Failed to update partner. Please correct the errors and try again.";
                    return View("Edit", partner);
                }

                var update = _patnerData.Update(partner);

                if (update.Exception != null)
                {
                    TempData["error"] = update.Exception.InnerException.Message;
                    Log.Error($"Error updating partner: {update.Exception.InnerException.Message}");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["success"] = "Partner updated successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while updating the partner. Please try again later.";
                Log.Error($"Unexpected error updating partner: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    Log.Warning("Attempted to delete partner with invalid id (0).");
                    TempData["error"] = "Invalid partner ID. Please try again.";
                    return RedirectToAction("Index");
                }

                var partner = _patnerData.Get(id).Result;

                if (partner == null)
                {
                    Log.Warning($"Attempted to delete non-existing partner with id: {id}.");
                    TempData["error"] = "Partner not found.";
                    return RedirectToAction("Index");
                }

                _patnerData.SoftDelete(id);
                TempData["success"] = "Partner deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while deleting the partner. Please try again later.";
                Log.Error($"Unexpected error deleting partner: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetPartnerDetails(int id)
        {
            try
            {
                if (id == 0)
                {
                    Log.Warning("Attempted to get partner details with an invalid partner ID (0).");
                    TempData["error"] = "Invalid partner ID. Please try again.";
                    return RedirectToAction("Index");
                }

                var partner = _patnerData.Get(id).Result;

                if (partner == null)
                {
                    Log.Warning($"Attempted to get partner details with an invalid partner ID: {id}");
                    TempData["error"] = "Partner not found.";
                    return RedirectToAction("Index");
                }

                return View("_PartnerDetails", partner);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while retrieving partner details. Please try again later.";
                Log.Error($"Unexpected error getting partner details: {ex.Message}");
                return RedirectToAction("Index");
            }
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
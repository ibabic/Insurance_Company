using Partners.DataAccess.Data;
using Partners.DataAccess.Models.DTOs;
using Serilog;
using System.Net;
using System;
using System.Web.Mvc;
using System.Linq;

namespace Partners.Controllers
{
    public class PolicyController : Controller
    {
        private readonly IPolicyRepository _policyData;

        public PolicyController(IPolicyRepository policyData)
        {
            _policyData = policyData;
        }

        public ActionResult Create(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["error"] = "Invalid partner id. Please provide a valid partner id.";
                    return RedirectToAction("Index", "Partner");
                }

                var policy = new PolicySaveDto()
                {
                    PartnerId = id
                };

                return View("Create", policy);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while loading the create policy page. Please try again later.";
                Log.Error($"Unexpected error loading create policy page: {ex.Message}");
                return RedirectToAction("Index", "Partner");
            }
        }

        [HttpPost]
        public ActionResult Create(PolicySaveDto policy)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Failed to create policy. Please correct the errors and try again.";
                    return View("Create");
                }

                var insert = _policyData.Insert(policy);
                if (insert.Exception != null)
                {
                    TempData["error"] = "An error occurred while creating the policy. Please try again later.";
                    Log.Error($"Error creating policy: {insert.Exception.InnerException.Message}");
                    return View("Create");
                }

                TempData["success"] = "Policy created successfully";
                return RedirectToAction("Index", "Partner");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while creating the policy. Please try again later.";
                Log.Error($"Unexpected error creating policy: {ex.Message}");
                return View("Create");
            }
        }

        public ActionResult ShowPolicies(int id)
        {
            try
            {
                if (id <= 0)
                {
                    TempData["error"] = "Invalid partner id. Please provide a valid partner id.";
                    return RedirectToAction("Index", "Partner");
                }

                var policies = _policyData.GetByPartnerId(id).Result;
                if (policies.Count() == 0)
                {
                    TempData["error"] = "No policies found for the specified partner.";
                    return RedirectToAction("Index", "Partner");
                }

                return View("ShowPolicies", policies);
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while loading the policies for the specified partner. Please try again later.";
                Log.Error($"Unexpected error loading policies for partner id {id}: {ex.Message}");
                return RedirectToAction("Index", "Partner");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                Log.Warning("Attempted to delete policy with invalid id (0 or negative).");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var policy = _policyData.Get(id).Result;
            if (policy == null)
            {
                Log.Warning($"Attempted to delete non-existing policy with id: {id}.");
                return HttpNotFound();
            }
            try
            {
                _policyData.SoftDelete(id);
                TempData["success"] = "Policy deleted successfully";
                return RedirectToAction("Index", "Partner");
            }
            catch (Exception ex)
            {
                Log.Error($"Error deleting policy with id: {id}. Error: {ex.Message}");
                TempData["error"] = "An error occurred while deleting the policy.";
                return RedirectToAction("Index", "Partner");
            }
        }
    }
}
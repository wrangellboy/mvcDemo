using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Data.Models;
using Logic.LeadLogic;
using Microsoft.AspNet.Identity;

namespace mvcDemo.Controllers
{
    [Authorize]
    public class LeadController : Controller
    {
        private readonly LeadLogic _leadLogic;

        public LeadController()
        {
            _leadLogic = new LeadLogic();
        }

        public LeadController(LeadLogic logic)
        {
            _leadLogic = logic;
        }

        // GET: Lead
        public ActionResult Index()
        {
            var leads = _leadLogic.GetLeads(User.Identity.GetUserName());
            return View(leads);
        }

        // GET: Lead/Details/5
        public ActionResult Details(int? id)
        {
            if (null == id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lead = _leadLogic.GetLeadById(id.Value);
            if (null == lead)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // GET: Lead/Create
        public ActionResult Create()
        {
            var lead = new Lead {LeadUser = User.Identity.GetUserName()};
            return View(lead);
        }

        // POST: Lead/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lead lead)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _leadLogic.AddLead(lead);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(lead);
        }

        // GET: Lead/Edit/5
        public ActionResult Edit(int? id)
        {
            if (null == id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lead = _leadLogic.GetLeadById(id.Value);
            if (null == lead)
            {
                return HttpNotFound();
            }

            return View(lead);
        }

        // POST: Lead/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lead lead)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(lead.LeadUser))
                    {
                        lead.LeadUser = User.Identity.GetUserName();
                    }

                    _leadLogic.UpdateLead(lead);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException cex)
                {
                    var entry = cex.Entries.Single();
                    var clientValues = (Lead) entry.Entity;
                    var dbEntry = entry.GetDatabaseValues();
                    if (null == dbEntry)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes.  The lead was deleted by another user.");
                    }
                    else
                    {
                        var dbValues = (Lead) dbEntry.ToObject();

                        if (dbValues.FirstName != clientValues.FirstName)
                        {
                            ModelState.AddModelError("FirstName", "Current Value: " + dbValues.FirstName);
                        }
                        if (dbValues.LastName != clientValues.LastName)
                        {
                            ModelState.AddModelError("LastName", "Current Value: " + dbValues.LastName);
                        }
                        if (dbValues.Email != clientValues.Email)
                        {
                            ModelState.AddModelError("Email", "Current Value: " + dbValues.Email);
                        }
                        if (dbValues.Address.AddressLine1 != clientValues.Address.AddressLine1)
                        {
                            ModelState.AddModelError("Address.AddressLine1",
                                "Current Value: " + dbValues.Address.AddressLine1);
                        }
                        if (dbValues.Address.AddressLine2 != clientValues.Address.AddressLine2)
                        {
                            ModelState.AddModelError("Address.AddressLine2",
                                "Current Value: " + dbValues.Address.AddressLine2);
                        }
                        if (dbValues.Address.City != clientValues.Address.City)
                        {
                            ModelState.AddModelError("Address.City", "Current Value: " + dbValues.Address.City);
                        }
                        if (dbValues.Address.State != clientValues.Address.State)
                        {
                            ModelState.AddModelError("Address.State", "Current Value: " + dbValues.Address.State);
                        }
                        if (dbValues.Address.ZipCode != clientValues.Address.ZipCode)
                        {
                            ModelState.AddModelError("Address.ZipCode", "Current Value: " + dbValues.Address.ZipCode);
                        }
                        if (dbValues.Address.Country != clientValues.Address.Country)
                        {
                            ModelState.AddModelError("Address.Country", "Current Value: " + dbValues.Address.Country);
                        }
                        if (dbValues.PhoneNumber.Number != clientValues.PhoneNumber.Number)
                        {
                            ModelState.AddModelError("PhoneNumber.Number",
                                "Current Value: " + dbValues.PhoneNumber.Number);
                        }
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                                               +
                                                               "was modified by another user after you got the original value. The "
                                                               +
                                                               "edit operation was canceled and the current values in the database "
                                                               +
                                                               "have been displayed. If you still want to edit this record, click "
                                                               +
                                                               "the Save button again. Otherwise click the Back to List hyperlink.");
                        lead.RowVersion = dbValues.RowVersion;
                    }
                    return View(lead);
                }
                catch
                {
                    ViewBag.Error = "Unable to update Lead.  Please try again.";
                    return View();
                }
            }
            return View(lead);
        }

        // GET: Lead/Delete/5
        public ActionResult Delete(int? id, bool? concurrencyError )
        {
            if (null == id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lead = _leadLogic.GetLeadById(id.Value);
            if (null == lead)
            {
                if (concurrencyError == true)
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                + "was modified by another user after you got the original values. "
                + "The delete operation was canceled and the current values in the "
                + "database have been displayed. If you still want to delete this "
                + "record, click the Delete button again. Otherwise "
                + "click the Back to List hyperlink.";
            }
            return View(lead);
        }

        // POST: Lead/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Lead lead)
        {
            try
            {
                _leadLogic.DeleteLead(lead);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new {concurrencyError = true, id = lead.LeadId});
            }
            catch
            {
                ViewBag.Error = "Unable to delete Lead.  Please try again.";
                return View();
            }
        }
    }
}

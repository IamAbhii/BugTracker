using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker_project.Models;

namespace Bug_Tracker_project.Controllers
{
    public class TicketNotificatioinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketNotificatioins
        public ActionResult Index()
        {
            var ticketNotificatioins = db.TicketNotificatioins.Include(t => t.Ticket);
            return View(ticketNotificatioins.ToList());
        }

        // GET: TicketNotificatioins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotificatioin ticketNotificatioin = db.TicketNotificatioins.Find(id);
            if (ticketNotificatioin == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotificatioin);
        }

        // GET: TicketNotificatioins/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            return View();
        }

        // POST: TicketNotificatioins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,UserId")] TicketNotificatioin ticketNotificatioin)
        {
            if (ModelState.IsValid)
            {
                db.TicketNotificatioins.Add(ticketNotificatioin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotificatioin.TicketId);
            return View(ticketNotificatioin);
        }

        // GET: TicketNotificatioins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotificatioin ticketNotificatioin = db.TicketNotificatioins.Find(id);
            if (ticketNotificatioin == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotificatioin.TicketId);
            return View(ticketNotificatioin);
        }

        // POST: TicketNotificatioins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,UserId")] TicketNotificatioin ticketNotificatioin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketNotificatioin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketNotificatioin.TicketId);
            return View(ticketNotificatioin);
        }

        // GET: TicketNotificatioins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketNotificatioin ticketNotificatioin = db.TicketNotificatioins.Find(id);
            if (ticketNotificatioin == null)
            {
                return HttpNotFound();
            }
            return View(ticketNotificatioin);
        }

        // POST: TicketNotificatioins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketNotificatioin ticketNotificatioin = db.TicketNotificatioins.Find(id);
            db.TicketNotificatioins.Remove(ticketNotificatioin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

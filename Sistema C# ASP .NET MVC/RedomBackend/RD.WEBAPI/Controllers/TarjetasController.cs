﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RD.Data.DAL;
using RD.Data.Models;

namespace RD.WEBAPI.Controllers
{
    public class TarjetasController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Tarjetas
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 1) == false)
                return RedirectToAction("Error", "Home");
            return View(await db.Tarjetas.OrderBy(x=>x.Nombre).ToListAsync());
        }

        // GET: Tarjetas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 1) == false)
                return RedirectToAction("Error", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarjeta tarjeta = await db.Tarjetas.FindAsync(id);
            if (tarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tarjeta);
        }

        // GET: Tarjetas/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 0) == false)
                return RedirectToAction("Error", "Home");
            return View();
        }

        // POST: Tarjetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TarjetaID,Tipo,Nombre")] Tarjeta tarjeta)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 0) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Tarjetas.Add(tarjeta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tarjeta);
        }

        // GET: Tarjetas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarjeta tarjeta = await db.Tarjetas.FindAsync(id);
            if (tarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tarjeta);
        }

        // POST: Tarjetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TarjetaID,Tipo,Nombre")] Tarjeta tarjeta)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(tarjeta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tarjeta);
        }

        // GET: Tarjetas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tarjeta tarjeta = await db.Tarjetas.FindAsync(id);
            if (tarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tarjeta);
        }

        // POST: Tarjetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 3) == false)
                return RedirectToAction("Error", "Home");

            Tarjeta tarjeta = await db.Tarjetas.FindAsync(id);
            db.Tarjetas.Remove(tarjeta);
            await db.SaveChangesAsync();
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

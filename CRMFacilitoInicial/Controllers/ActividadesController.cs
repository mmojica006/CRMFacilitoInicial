﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMFacilitoInicial.Models;

namespace CRMFacilitoInicial.Controllers
{
    public class ActividadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actividades
        public ActionResult Index()
        {
            return View(db.Actividades.ToList());
        }

      
       

        // GET: Actividades/Create
        public ActionResult Create()
        {
            var tipos = new SelectList(db.TipoActividades.ToList(), "TipoActividadId", "Descripcion");
            ViewData["tipos"] = tipos;
            return View();
        }

        // POST: Actividades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ActividadViewModel factividad)
        {

            Actividad actividad = new Actividad();



            if (ModelState.IsValid)
            {

                actividad.ActividadId = factividad.ActividadId;
                actividad.FechaInicial = factividad.FechaInicial;
                actividad.FechaFinal = factividad.FechaInicial;
                actividad.FechaInicialPlan = factividad.FechaInicial;
                actividad.FechaFinalPlan = factividad.FechaInicial;
                actividad.ClienteId = factividad.ClienteId;
                actividad.TipoActividadId = factividad.TipoActividadId;
                actividad.Descripcion = factividad.Descripcion;
                actividad.Estado = 0;



                db.Actividades.Add(actividad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var tipos = new SelectList(db.TipoActividades.ToList(), "TipoActividadId", "Descripcion");
            ViewData["tipos"] = tipos;


            return View(actividad);
        }

        // GET: Actividades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = db.Actividades.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            db.Entry(actividad).Reference("ClienteActividad").Load();
            ActividadViewModel factividad = new ActividadViewModel();
            factividad.ActividadId = actividad.ActividadId;
            factividad.Descripcion = actividad.Descripcion;
            factividad.FechaInicial = actividad.FechaInicial;
            factividad.ClienteId = actividad.ClienteId;
            factividad.TipoActividadId = actividad.TipoActividadId;
            factividad.nombre = actividad.ClienteActividad.Nombre; //Actualizar el nombre en el formulario
            var tipos = new SelectList(db.TipoActividades.ToList(), "TipoActividadId", "Descripcion");
            ViewData["tipos"] = tipos;
            factividad.ObtenTelefonosYEmailsDeCliente();
            return PartialView(factividad);
        }

        // POST: Actividades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActividadViewModel factividad)
        {
            if (ModelState.IsValid)
            {
                Actividad actividad = db.Actividades.Find(factividad.ActividadId);
                actividad.ActividadId = factividad.ActividadId;
                actividad.FechaInicial = factividad.FechaInicial;
                actividad.FechaFinal = factividad.FechaInicial;
                actividad.FechaInicialPlan = factividad.FechaInicial;
                actividad.FechaFinalPlan = factividad.FechaInicial;
                actividad.ClienteId = factividad.ClienteId;
                actividad.TipoActividadId = factividad.TipoActividadId;
                actividad.Descripcion = factividad.Descripcion;
                actividad.Estado = 0;
                db.Entry(actividad).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            var tipos = new SelectList(db.TipoActividades.ToList(), "TipoActividadId", "Descripcion");
            ViewData["tipos"] = tipos;
            return PartialView(factividad);
        }

        // GET: Actividades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = db.Actividades.Find(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // POST: Actividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actividad actividad = db.Actividades.Find(id);
            db.Actividades.Remove(actividad);
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

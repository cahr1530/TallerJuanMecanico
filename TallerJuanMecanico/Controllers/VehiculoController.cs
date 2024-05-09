using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerJuanMecanico.Models;
using TallerJuanMecanico.Models.TableViewModels;
using TallerJuanMecanico.Models.ViewModels;

namespace TallerJuanMecanico.Controllers
{
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        public ActionResult Index()
        {
            List<VehiculoTableViewModel> lista = null;
            using (TallerJuanMecanicoEntities db = new TallerJuanMecanicoEntities())
            {
                lista = (from d in db.Vehiculo join p in db.Usuarios on d.vehiculo_id equals p.usuario_Id
                         where d.activo == true
                                                 select new VehiculoTableViewModel
                                                 {
                             vehiculo_id = d.vehiculo_id,
                             marca = d.marca,
                             modelo = d.modelo,
                             ano = d.ano,
                             placa = d.placa,
                             nombre = d.Usuarios.nombre,
                             telefono = d.Usuarios.telefono,
                             email = d.Usuarios.Email
                         }).ToList();
            }
            return View(lista);
        }

        [HttpGet]
        public ActionResult Add()        {
            ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Usuarios.Where(u => u.rol_id == 2 && u.activo).ToList(), "usuario_Id", "nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Add(VehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Usuarios.Where(u=>u.rol_id==2 && u.activo).ToList(), "usuario_Id", "nombre");
                return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                Vehiculo oVehiculo = new Vehiculo();
                oVehiculo.marca = model.marca;
                oVehiculo.modelo = model.modelo;
                oVehiculo.ano = model.ano;
                oVehiculo.placa = model.placa;
                oVehiculo.usuario_Id = model.usuario_Id;
                db.Vehiculo.Add(oVehiculo);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Vehiculo/"));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditVehiculoViewModel model = new EditVehiculoViewModel();
            using (var db = new TallerJuanMecanicoEntities())
            {
                var oVehiculo = db.Vehiculo.Find(id);
                model.marca = oVehiculo.marca;
                model.modelo = oVehiculo.modelo;
                model.ano = oVehiculo.ano;
                model.placa = oVehiculo.placa;    
                model.vehiculo_Id = oVehiculo.vehiculo_id;
            }
           return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EditVehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                  return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                Vehiculo oVehiculo = db.Vehiculo.Find(model.vehiculo_Id);
                oVehiculo.marca = model.marca;
                oVehiculo.modelo = model.modelo;
                oVehiculo.ano = model.ano;
                oVehiculo.placa = model.placa;              
                db.Entry(oVehiculo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Vehiculo/"));
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new TallerJuanMecanicoEntities())
            {
                var oVehiculo = db.Vehiculo.Find(Id);
                oVehiculo.activo = false;

                db.Entry(oVehiculo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }

    }
}
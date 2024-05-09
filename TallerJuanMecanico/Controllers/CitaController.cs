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
    public class CitaController : Controller
    {
        // GET: Cita
        public ActionResult Index()
        {


            List<CitaTableViewModel> lista = null;
            using (TallerJuanMecanicoEntities db=new TallerJuanMecanicoEntities())
            {
                lista = (from d in db.Cita
                         join v in db.Vehiculo on d.vehiculo_id equals v.vehiculo_id
                         join p in db.Usuarios on v.usuario_Id equals p.usuario_Id
                         where p.activo == true 
                         &&    v.activo == true
                         orderby d.fecha_inicio
                         select new CitaTableViewModel
                         {
                             cita_id = d.cita_id,
                             fecha_inicio = d.fecha_inicio.ToString(),
                             fecha_fin = d.fecha_fin.ToString(),
                             estado = d.estado,
                             Cliente = p.nombre,
                             vehiculo = v.placa,
                             Modelo = v.modelo,
                             marca = v.marca
                         }).ToList();
            }
                return View(lista);
        }
        [HttpGet]
        public ActionResult Add()
        {

            ViewBag.Estado = GetEstadoItems();
            ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Vehiculo.Where(v=>v.activo). ToList(), "vehiculo_id", "placa");
            return View();
        }
        [HttpPost]
        public ActionResult Add(CitaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Estado = GetEstadoItems();
                ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Vehiculo.Where(v => v.activo).ToList(), "vehiculo_id", "placa");
                return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                Cita oCita = new Cita();
                Usuarios oUser = (Usuarios)Session["User"];
                oCita.fecha_inicio = model.fecha_inicio;
                oCita.fecha_fin = oCita.fecha_inicio.AddHours(2);
                oCita.estado = model.estado;
                oCita.vehiculo_id = model.vehiculo_id;
                oCita.usuario_Id =oUser.usuario_Id ;
                oCita.comentario_administrador = model.comentario_administrador;
                oCita.comentario_cliente = model.comentario_cliente;
                db.Cita.Add(oCita);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Cita/"));
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCitaViewModel model = new EditCitaViewModel();
            using (var db = new TallerJuanMecanicoEntities())
            {
                Cita oCita = db.Cita.Find(Id);
                model.cita_id = oCita.cita_id;
                model.fecha_inicio = oCita.fecha_inicio;
                model.estado = oCita.estado;
                model.vehiculo_id = oCita.vehiculo_id;
                model.comentario_administrador = oCita.comentario_administrador;
                model.comentario_cliente = oCita.comentario_cliente;
            }
            ViewBag.Estado = GetEstadoItems();
            ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Vehiculo.Where(v => v.activo).ToList(), "vehiculo_id", "placa");
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCitaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Estado = GetEstadoItems();
                ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Vehiculo.Where(v => v.activo).ToList(), "vehiculo_id", "placa");
                return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                Cita oCita = db.Cita.Find(model.cita_id);
                oCita.fecha_inicio = model.fecha_inicio;
                oCita.fecha_fin = oCita.fecha_inicio.AddHours(2);
                oCita.estado = model.estado;
                oCita.vehiculo_id = model.vehiculo_id;
                oCita.comentario_administrador = model.comentario_administrador;
                oCita.comentario_cliente = model.comentario_cliente;
                db.Entry(oCita).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Cita/"));
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new TallerJuanMecanicoEntities())
            {
                Cita oCita = db.Cita.Find(Id);
                db.Entry(oCita).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Content("1");
        }

        private List<SelectListItem> GetEstadoItems()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Text = "Aprobada", Value = "aprobada" },
            new SelectListItem { Text = "Rechazada", Value = "rechazada" },
            new SelectListItem { Text = "Pendiente", Value = "pendiente" },
        };
        }

    }
}
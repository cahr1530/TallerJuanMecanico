using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerJuanMecanico.Models;
using TallerJuanMecanico.Models.TableViewModels;
using TallerJuanMecanico.Models.ViewModels;

namespace TallerJuanMecanico.Controllers.Usuario
{
    public class UserCitaController : Controller
    {
        // GET: UserCita
        public ActionResult Index()
        {
            List<CitaCardViewModel> lista = null;
            using (TallerJuanMecanicoEntities db = new TallerJuanMecanicoEntities())
            {
                Usuarios oUser = (Usuarios)Session["User"];
                lista = (from d in db.Cita
                         join v in db.Vehiculo on d.vehiculo_id equals v.vehiculo_id
                         join p in db.Usuarios on v.usuario_Id equals p.usuario_Id
                         where p.activo == true
                         && v.activo == true
                         && v.usuario_Id == oUser.usuario_Id
                         orderby d.fecha_inicio
                         select new CitaCardViewModel
                         {
                             cita_id = d.cita_id,
                             fecha_inicio = d.fecha_inicio.ToString(),
                             fecha_fin = d.fecha_fin.ToString(),
                             estado = d.estado,
                             Cliente = p.nombre,
                             vehiculo = v.placa,
                             Modelo = v.modelo,
                             marca = v.marca,
                             ano =v.ano,
                             comentario_cliente = d.comentario_cliente,
                             comentario_administrador = d.comentario_administrador
                         }).ToList(); ;
            }


            return View(lista);
        }
        [HttpGet]
        public ActionResult Add()
        {
        Usuarios oUser = (Usuarios)Session["User"];

            ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Vehiculo.Where(v => v.activo && v.usuario_Id==oUser.usuario_Id ).ToList(), "vehiculo_id", "placa");
            return View();
        }

        [HttpPost]
        public ActionResult Add(CitaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Vehiculo.Where(v => v.activo).ToList(), "vehiculo_id", "placa");
                return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                Cita oCita = new Cita();
                Usuarios oUser = (Usuarios)Session["User"];
                oCita.fecha_inicio = model.fecha_inicio;
                oCita.fecha_fin =model.fecha_inicio.AddHours(2);
                oCita.estado = "Pendiente";
                oCita.vehiculo_id = model.vehiculo_id;
                oCita.comentario_cliente = model.comentario_cliente;
                oCita.comentario_administrador = "";
                db.Cita.Add(oCita);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Usuario/UserCita/Index"));
        }

    }
}
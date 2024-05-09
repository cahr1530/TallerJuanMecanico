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
    public class UserVehiculoController : Controller
    {
        // GET: UserVehiculo
        public ActionResult Index()
        {
            List<VehiculoCardViewModel> lista = null;
            using (TallerJuanMecanicoEntities db = new Models.TallerJuanMecanicoEntities())
            {
                Usuarios oUser = (Usuarios)Session["User"];
                lista = (from d in db.Vehiculo
                       where d.activo == true
                       && d.usuario_Id == oUser.usuario_Id
                                             select new TallerJuanMecanico.Models.TableViewModels.VehiculoCardViewModel
                                             {
                           placa = d.placa,
                           modelo = d.modelo,
                           marca = d.marca,
                           ano = d.ano
                         
                       }).ToList();
            }

            return View(lista);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(VehiculoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                Vehiculo oVehiculo = new Vehiculo();
                oVehiculo.marca = model.marca;
                oVehiculo.modelo = model.modelo;
                oVehiculo.ano = model.ano;
                oVehiculo.placa = model.placa;
                oVehiculo.activo = true;
                Usuarios oUser = (Usuarios)Session["User"];
                oVehiculo.usuario_Id = oUser.usuario_Id;
                db.Vehiculo.Add(oVehiculo);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
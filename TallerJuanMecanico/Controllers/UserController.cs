using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerJuanMecanico.Filters;
using TallerJuanMecanico.Helpers;
using TallerJuanMecanico.Models;
using TallerJuanMecanico.Models.TableViewModels;
using TallerJuanMecanico.Models.ViewModels;

namespace TallerJuanMecanico.Controllers
{
   
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            List<UsuariosTableViewModel> lista = null;
            using (TallerJuanMecanicoEntities db = new TallerJuanMecanicoEntities())
            {
                lista=(from d in db.Usuarios join p in db.Roles on d.rol_id equals p.rol_Id
                                   where d.activo == true
                                   orderby d.Email
                                   select new UsuariosTableViewModel
                                   {
                                        usuario_id = d.usuario_Id,
                                        Nombre = d.nombre,
                                        Telefono = d.telefono,
                                        Direccion = d.direccion,
                                        Email = d.Email,
                                        Password = d.Password,
                                        Rol = p.rol
                                    }).ToList();
            }
                return View(lista);
        }

        [HttpGet]
        public ActionResult Add()
        {
          ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Roles.ToList(), "rol_Id", "rol");
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Items = new SelectList(new TallerJuanMecanicoEntities().Roles.ToList(), "rol_Id", "rol");
                return View(model);
            }
         
            using (var db = new TallerJuanMecanicoEntities())
            {
                
                Usuarios oUser = new Usuarios();
                oUser.nombre = model.Nombre;
                oUser.telefono = model.Telefono;
                oUser.direccion = model.Direccion;
                oUser.Email = model.Email;

                oUser.Password = MD5Helper.CalculateMD5Hash(model.Password);
                
                oUser.rol_id = model.rol_id;
                oUser.activo = true;
                db.Usuarios.Add(oUser);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User/"));
        }

        public ActionResult Edit(int Id)
        {
            EditUserViewModel model = new EditUserViewModel();
           using( var db = new TallerJuanMecanicoEntities())
            {
                var oUser = db.Usuarios.Find(Id);
                model.Nombre = oUser.nombre;
                model.Telefono = oUser.telefono;
                model.Direccion = oUser.direccion;
                model.Email = oUser.Email;
                model.rol_id = oUser.rol_id;
                model.usuario_id = oUser.usuario_Id;
          
          
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new TallerJuanMecanicoEntities())
            {
                var oUser = db.Usuarios.Find(model.usuario_id);
                oUser.nombre = model.Nombre;
                oUser.telefono = model.Telefono;
                oUser.direccion = model.Direccion;
                oUser.Email = model.Email;

                if(model.Password != null && model.Password.Trim() != "")
                {
                    oUser.Password = MD5Helper.CalculateMD5Hash(model.Password);
                }
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User/"));
        }



        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new TallerJuanMecanicoEntities())
            {
                var oUser = db.Usuarios.Find(Id);
                oUser.activo = false;
             
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }


     
        }
}
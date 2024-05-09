using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerJuanMecanico.Models.TableViewModels
{
    public class UsuariosTableViewModel
    {
        public int usuario_id { get; set; } 
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Rol { get; set; }


    }
}
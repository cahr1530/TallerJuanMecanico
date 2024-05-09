using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TallerJuanMecanico.Models.TableViewModels
{
    public class CitaCardViewModel
    {
        public int cita_id { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public string estado { get; set; }
        //   public string Administrador { get; set; }
        public string Cliente { get; set; }
        public string vehiculo { get; set; }
        public string Modelo { get; set; }
        public string marca { get; set; }
        public int ano { get; set; }
        public string comentario_administrador { get; set; }
        public string comentario_cliente { get; set; }


    }
}
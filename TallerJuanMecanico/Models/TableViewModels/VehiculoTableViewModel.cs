using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TallerJuanMecanico.Models.TableViewModels
{
    public class VehiculoTableViewModel
    {
        public int vehiculo_id { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int ano { get; set; }
        public string placa { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
     
    }
}
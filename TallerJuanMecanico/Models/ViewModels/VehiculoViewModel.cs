using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerJuanMecanico.Helpers;

namespace TallerJuanMecanico.Models.ViewModels
{
    public class VehiculoViewModel
    {
            
      
        [Required]
        [Display(Name = "Marca")]
        public string marca { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string modelo { get; set; }
        [Required]
        [Display(Name = "Año")]
        public int ano { get; set; }
        [Required]
        [PlacaExist]
        [Display(Name = "Placa")]
        public string placa { get; set; }
        [Required]
        [Display(Name = "Dueño")]
        public int usuario_Id { get; set; }
        [Required]
        public bool activo { get; set; }
    }


    public class EditVehiculoViewModel
    {

        public int vehiculo_Id { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public string marca { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string modelo { get; set; }
        [Required]
        [Display(Name = "Año")]
        public int ano { get; set; }
        [Required]
        [Display(Name = "Placa")]
        public string placa { get; set; }
     
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerJuanMecanico.Helpers;

namespace TallerJuanMecanico.Models.ViewModels
{
    public class CitaViewModel
    {
        [Required]
        [CitaExist(ErrorMessage = "Ya hay una cita programada en este horario.")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime fecha_inicio { get; set; }
       
        [Required]
        [Display(Name = "Estado")]
        public string estado { get; set; }
      
        [Required]
        [Display(Name = "Vehículo")]
        public int vehiculo_id { get; set; }

        [Required]
        [Display(Name = "Comentario Administrador")]
        public string comentario_administrador { get; set; }

        [Required]
        [Display(Name = "Comentario Cliente")]
        public string comentario_cliente { get; set; }

    }

    public class EditCitaViewModel
    {
        public int cita_id { get; set; }

        [Required]
        [Display(Name = "Fecha de Inicio")]
        public DateTime fecha_inicio { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Required]
        [Display(Name = "Vehículo")]
        public int vehiculo_id { get; set; }

        [Required]
        [Display(Name = "Comentario Administrador")]
        public string comentario_administrador { get; set; }

        [Required]
        [Display(Name = "Comentario Cliente")]
        public string comentario_cliente { get; set; }
    }


}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TallerJuanMecanico.Helpers
{
    public class PlacaExistAttribute: ValidationAttribute
    {

        public PlacaExistAttribute()
        {
            ErrorMessage = "La placa ya existe";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            var placa = value.ToString();
            using (var db = new TallerJuanMecanico.Models.TallerJuanMecanicoEntities())
            {
                var vehiculo = db.Vehiculo.FirstOrDefault(v => v.placa == placa);
                if (vehiculo == null)
                {
                    return true;
                }
                return false;
            }
        }   
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerJuanMecanico.Models;

namespace TallerJuanMecanico.Helpers
{
    public class CitaExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime fecha)
            {
              
                if (fecha>=DateTime.Today)
                {
                    DateTime horaFinCita = fecha.AddHours(2);

                    var db = new TallerJuanMecanicoEntities();
                    var citasExistentes = db.Cita.ToList();
                    foreach (var citaExistente in citasExistentes)
                    {
                        DateTime horaInicioExistente = citaExistente.fecha_inicio;
                        DateTime horaFinExistente = citaExistente.fecha_inicio.AddHours(2);

                        if (fecha < horaFinExistente && horaFinCita > horaInicioExistente)
                        {
                            return new ValidationResult(ErrorMessage);
                        }
                    }
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }


            }

            return ValidationResult.Success;
        }

    }
}
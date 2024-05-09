using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TallerJuanMecanico.Models;
using TallerJuanMecanico.Models.ViewModels;

namespace TallerJuanMecanico.Helpers
{
    public class UserExistsAttribute : ValidationAttribute
    {
        private readonly bool _isNewUser;

        public UserExistsAttribute(bool isNewUser)
        {
            _isNewUser = isNewUser;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                   return new ValidationResult("El correo es obligatorio");
            }
            var email = value.ToString();

            if (!_isNewUser)
            {
                using (var db = new TallerJuanMecanicoEntities())
                {
                   
                    var existingUser = db.Usuarios.FirstOrDefault(u => u.Email == email);
                    if (existingUser != null)
                    {
                      
                        if (_isNewUser || existingUser.usuario_Id != (validationContext.ObjectInstance as EditUserViewModel).usuario_id)
                        {
                            return new ValidationResult("El correo electrónico ya está registrado.");
                        }
                    }
                }
            }
            else
            {
                using (var db = new TallerJuanMecanicoEntities())
                {
                  
                    var existingUser = db.Usuarios.FirstOrDefault(u => u.Email == email);
                    if (existingUser != null)
                    {
                            return new ValidationResult("El correo electrónico ya está registrado.");
                    }
                }

            }


            return ValidationResult.Success;
        }
    }
}
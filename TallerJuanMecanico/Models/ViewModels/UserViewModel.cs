using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Web.Mvc;
using TallerJuanMecanico.Helpers;
namespace TallerJuanMecanico.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9+\-() ]+$", ErrorMessage = "El número de teléfono solo puede contener números, espacios y los caracteres '+', '-', '(', ')'")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Dirección")]

        public string Direccion { get; set; }

        [Required]
        [EmailAddress]
        [UserExists(isNewUser: true, ErrorMessage = "El correo electrónico ya está registrado.")]
        [StringLength(100,ErrorMessage ="El {0} debe tener al menos {1} caracteres",MinimumLength =1)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmpassword { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int rol_id { get; set; }


    }


    public class EditUserViewModel
    {
        public int usuario_id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9+\-() ]+$", ErrorMessage = "El número de teléfono solo puede contener números, espacios y los caracteres '+', '-', '(', ')'")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Correo Electrónico")]
        [UserExists(isNewUser: false, ErrorMessage = "El correo electrónico ya está registrado.")]
        public string Email { get; set; }

      
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmpassword { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int rol_id { get; set; }


    }

}
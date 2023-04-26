using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.PropiedadMejora;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Propiedades
{
    public class SavePropiedadViewModel
    {
        public int id { get; set; }
        public string? UnicDigitSequence { get; set; }
        public string? Tipo { get; set; }
        public string? AgenteNombre { get; set; }
        public string? TipoVenta { get; set; }

        [ValidarDoP(ErrorMessage = "El precio debe ser mayor a 0.")]
        [Required(ErrorMessage = "Escriba el precio...")]
        public int Precio { get; set; }


        [Required(ErrorMessage = "Escriba los metros...")]
        public int MtsTerrain { get; set; }

        [Required(ErrorMessage = "Escriba la cantidad de habitaciones...")]
        public int QuantityHabitaciones { get; set; }

     
        [Required(ErrorMessage = "Escriba la cantidad de baños...")]
        public int QuantityBaños { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de venta...")]
        public int VentaTypeId { get; set; }
        public string? UserClientId { get; set; }
        public DateTime? created { get; set; }

      
        [Required(ErrorMessage = "Seleccione un tipo de propiedad ...")]
        public int PropertyTypeId { get; set; }
        public string AgenteId { get; set; }
        public List<PropiedadMejoraViewModel>? PropiedadMejoras { get; set; }

        public List<PropertypeViewModel>? TiposPropiedad { get; set; }
        public List<VentaTypeViewModel>? TiposVenta { get; set; }
        public List<MejoraViewModel>? Mejoras { get; set; }

        [Required(ErrorMessage = "Seleccione al menos una mejora")]
        public List<int>? IdMejoras { get; set; }

        public List<int>? EditIdMejoras { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Escriba la descripción...")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Inserte al menos un archivo")]
        [DataType(DataType.Upload)]
        public IFormFile? file1 { get; set; }

       
        [DataType(DataType.Upload)]
        public IFormFile? file2 { get; set; }

       
        [DataType(DataType.Upload)]
        public IFormFile? file3 { get; set; }

       
        [DataType(DataType.Upload)]
        public IFormFile? file4 { get; set; }

        public string? ErrorDetails { get; set; }
        public bool HasError { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels.Addendas
{
    public class AgregarRegistro
    {
        public System.Guid IdAddenda { get; set; }
        public string NombreAddenda { get; set; }
        public string XML { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string Usuario { get; set; }
        public Nullable<bool> Estado { get; set; }

    }
}
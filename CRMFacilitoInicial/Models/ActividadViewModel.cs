using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMFacilitoInicial.Models
{
    public class ActividadViewModel
    {
        public int ActividadId { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicial { get; set; }

        public int TipoActividadId { get; set; }

        public int ClienteId { get; set; }

        public string nombre { get; set; }

        public string NombreTipo { get; set; }

        public string Telefonos { get; set; }

        public string Emails { get; set; }

    }
}
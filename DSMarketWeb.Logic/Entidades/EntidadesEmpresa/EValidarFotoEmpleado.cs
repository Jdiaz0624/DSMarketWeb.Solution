using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesEmpresa
{
    public class EValidarFotoEmpleado
    {
        public decimal? IdEmpleado { get; set; }

        public System.Nullable<decimal> NumeroRegistro { get; set; }

        public System.Data.Linq.Binary Foto { get; set; }
    }
}

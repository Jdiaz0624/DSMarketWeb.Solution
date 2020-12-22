using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Entidades.EntidadesInventario
{
    public class EEliminarFotoProducto
    {
        public System.Nullable<decimal> IdProducto { get; set; }

        public System.Nullable<decimal> NumeroConector { get; set; }

        public System.Data.Linq.Binary FotoProducto { get; set; }
    }
}

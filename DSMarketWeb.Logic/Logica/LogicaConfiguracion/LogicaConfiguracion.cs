using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.Logica.LogicaConfiguracion
{
    public class LogicaConfiguracion
    {
        DSMarketWeb.Data.ConexionLINQ.BDConexionConfiguracionDataContext ObjData = new Data.ConexionLINQ.BDConexionConfiguracionDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["DSMarketWEBConexion"].ConnectionString);

        #region MANTENIMIENTO DE INFORMACION DE EMPRESA
        
        #endregion
    }
}

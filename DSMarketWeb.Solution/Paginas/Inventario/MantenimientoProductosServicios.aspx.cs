using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSMarketWeb.Solution.Paginas.Inventario
{
    public partial class MantenimientoProductosServicios : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack) {
         
            }
        }
    }
}
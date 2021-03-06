﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMarketWeb.Logic.PrcesarMantenimientos.Inventario
{
    public class ProcesarInformacionModelos
    {
        readonly DSMarketWeb.Logic.Logica.LogicaInventario.LogicaInventario ObjData = new Logica.LogicaInventario.LogicaInventario();

        private decimal IdMarca = 0;
        private decimal IdModelo = 0;
        private string Descripcion = "";
        private bool Estatus = false;
        private string Accion = "";

        public ProcesarInformacionModelos(
            decimal IdMarcaCON,
            decimal IdModeloCON,
            string DescripcionCON,
            bool EstatusCON,
            string AccionCON)
        {
            IdMarca = IdMarcaCON;
            IdModelo = IdModeloCON;
            Descripcion = DescripcionCON;
            Estatus = EstatusCON;
            Accion = AccionCON;
        }

        public void ProcesarInformacion() {
            DSMarketWeb.Logic.Entidades.EntidadesInventario.EModelos Procesar = new Entidades.EntidadesInventario.EModelos();

            Procesar.IdMarca = IdMarca;
            Procesar.IdModelo = IdModelo;
            Procesar.Modelo = Descripcion;
            Procesar.Estatus0 = Estatus;

            var MAN = ObjData.ProcesarModelos(Procesar, Accion);
        }
    }
}
